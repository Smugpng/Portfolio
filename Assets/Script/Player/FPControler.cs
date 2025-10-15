using System.ComponentModel;
using System.Linq;
using Unity.Cinemachine;
using Unity.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class FPControler : MonoBehaviour
    {
        public static FPControler instance;
        public bool isStunned = false;

        [Header("Movement Param")]

        public float maxSpeed => isSprinting ? sprintSpeed : walkSpeed;
        public float acceleration = 15f;


        [SerializeField] float walkSpeed = 5f, sprintSpeed = 10f;

        [Space(15)]
        [SerializeField] float jumpHeight = 2f;

        private int timesJumped = 0;

        [SerializeField] bool canDoubleJump = true;

        public bool Sprinting
        {
            get
            {
                return isSprinting && currentSpeed > 0.1f;
            }
        }


        [Header("Look Param")]
        public Vector2 lookSens = new Vector2(0.1f, 0.1f);
        public float pitchLimit = 90f;
        [SerializeField] float currentPitch = 0f;
        public float CurrentPitch
        {
            
            get => currentPitch;

            set
            {
                currentPitch = Mathf.Clamp(value, -pitchLimit, pitchLimit);
            }
        }

        [Header("Input")]
        public Vector2 moveInput, lookInput;
        public bool isSprinting;
        public bool isJumping;

        [Header("Components")]
        [SerializeField] CinemachineCamera fpCamera;
        [SerializeField] CharacterController controller;

        [Header("Events")]
        public UnityEvent Landed;

        [Header("Camera Param")]
        [Range(60,120)] public float baseFov = 90f, maxFov = 110f;
        public float cameraFovSmoothing = 1f;
        [SerializeField] private CameraStuff camShake;
         float currentFov
        {
            get
            {
                return Sprinting ? maxFov : baseFov;
            }
        }

        [Header("Physics")]
        public Vector3 currentVelcity { get; private set; }
        public float currentSpeed { get; private set; }

        public float verticalVelocity = 0f;

        [SerializeField] float gravityScale = 3f;

        public bool isGrounded => controller.isGrounded;
        private bool wasGrounded = false;

        [Header("Climbing")]
        public LayerMask wallMask;
        [SerializeField] bool facingWall,isClimbing;
        public float climbTimer, climbMaxTimer;

        [Header("Detection")]
        [SerializeField] private float rayLength;

        #region Unity Methods
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        private void OnValidate()
        {
            if(controller == null)
            {
                controller = GetComponent<CharacterController>();
            }
        }
        private void Update()
        {
            if(isStunned) { return; }
            MoveUpdate();
            LookUpdate();
            CameraUpdate();
            CheckWall();
            StateMachine();
            if(isClimbing && climbTimer > 0) Climbing();
            if(!wasGrounded && isGrounded)
            {
                if(verticalVelocity < -20)
                {
                    camShake.ScreeShake();
                }
                timesJumped = 0;
                ClimbReset();
                Landed?.Invoke();
            }
            wasGrounded = isGrounded;
        }

        #endregion
        #region Controller Methods
        public void TryJump() //On button press check if I can jump. If I can add to my vert vel
        {
            if (!isGrounded)
            {
                if(!canDoubleJump || timesJumped >= 2)
                {
                    return;
                }
            }
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y * gravityScale);
            timesJumped++;
        }
        #region Wall Climbing
        private void CheckWall() //Constant Check if facing wall
        {
            Vector3 localPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            facingWall = Physics.Raycast(localPos, transform.forward, rayLength, wallMask);
        }
        
        private void StartClimb()
        {
            isClimbing = true;
        }
        private void Climbing()
        {
            verticalVelocity = 9;
        }

        private void StopClimb() //On climb stop give a little boost
        {
            isClimbing = false;
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y * gravityScale);
        }
        private void ClimbReset()
        {
            climbTimer = climbMaxTimer;
        }
        private void StateMachine()
        {
            if(facingWall && isJumping)
            {
                if (!isClimbing && climbTimer > 0) 
                { 
                    StartClimb(); 
                }
                if (climbTimer > 0 && isClimbing) climbTimer -= Time.deltaTime;
                if (climbTimer < 0 && isClimbing) StopClimb();
            }
            else
            {
                if(isClimbing) StopClimb();
            }
        }
        #endregion

        private void MoveUpdate()
        {
            Vector3 motion = transform.forward * moveInput.y + transform.right * moveInput.x;
            motion.y = 0f;
            motion.Normalize();

            if(motion.sqrMagnitude >= 0.01f )
            {
                currentVelcity = Vector3.MoveTowards(currentVelcity, motion * maxSpeed, acceleration * Time.deltaTime);
            }
            else
            {
                currentVelcity = Vector3.MoveTowards(currentVelcity, Vector3.zero, acceleration * Time.deltaTime);
            }


            if (isGrounded && verticalVelocity <= .01f && !facingWall)
            {
                verticalVelocity = -3f;
            }
            else if(!facingWall)
            {
                verticalVelocity += Physics.gravity.y * gravityScale * Time.deltaTime;
            }
            else if(facingWall && !isClimbing)
            {
                verticalVelocity += Physics.gravity.y * (gravityScale/2) * Time.deltaTime;
            }
 
            Vector3 fullVelocity = new Vector3(currentVelcity.x,(verticalVelocity),currentVelcity.z);



            CollisionFlags flags = controller.Move(fullVelocity * Time.deltaTime);
            if ((flags & CollisionFlags.Above) != 0 && verticalVelocity > .01f)
            {
                verticalVelocity = 0f;
            }

            currentSpeed = currentVelcity.magnitude;
        }
        private void LookUpdate()
        {
            Vector2 input = new Vector2(lookInput.x * lookSens.x, lookInput.y * lookSens.y);

            // handles up and down
            CurrentPitch -= input.y;
            fpCamera.transform.localRotation = Quaternion.Euler(currentPitch, 0f, 0f);

            //handles side to side
            transform.Rotate(Vector3.up * input.x);
        }
        #endregion
        #region Camera
        private void CameraUpdate()
        {
            float targetFov = baseFov;
            if (isSprinting)
            {
                float speedRatio = currentSpeed / sprintSpeed;
                targetFov = Mathf.Lerp(baseFov, maxFov, speedRatio);
            }
            fpCamera.Lens.FieldOfView = Mathf.Lerp(fpCamera.Lens.FieldOfView, targetFov, cameraFovSmoothing * Time.deltaTime);
        }
        #endregion
    }

}
