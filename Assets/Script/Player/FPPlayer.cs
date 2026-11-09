using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(FPControler))]
    [RequireComponent(typeof(FPInteraction))]
    [RequireComponent (typeof(FPTeleport))]
    public class FPPlayer : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] FPControler FPControler;
        [SerializeField] FPInteraction FPInteraction;
        [SerializeField] FPTeleport FPTeleport;
        [SerializeField] WorldTransitionCheck worldTransCheck;
        //[Header("Weapon")]

        //[SerializeField] public IWeapon currentWeapon;
        [Header("Events")]
        public UnityEvent JumpInput, PauseInput, AttackInput ,blinkFail,blinkSuccess;

        private void Awake()
        {
            
        }
      

        #region Input
        void OnMove(InputValue value)
        {

            FPControler.moveInput = value.Get<Vector2>();
        }
        void OnLook(InputValue value)
        {
            FPControler.lookInput = value.Get<Vector2>();
        }
        void OnSprint(InputValue value)
        {
            FPControler.isSprinting = value.isPressed;
        }
        void OnJump(InputValue value)
        {
            FPControler.isJumping = value.isPressed;
            if (value.isPressed)
            {
                FPControler.TryJump();
            }
        }
        void OnInteract(InputValue value)
        {
            FPInteraction.isInteracting = value.isPressed;
        }
        void OnPause(InputValue value)
        {

        }
        void OnAim(InputValue value)
        {
            FPTeleport.isAiming = value.isPressed;
        }
        void OnAttack(InputValue value) //Checks if dash is being aimed if not grapple instead
        {
            if (FPTeleport.Dash())
            {

            }
            else
            {
                //currentWeapon.Attack();
            }
            
        }
        
        void OnSwap(InputValue value)
        {
            if (!worldTransCheck.canSwap())
            {
                blinkFail.Invoke();
            }
            else
            {
                blinkSuccess.Invoke();
                Blinking.instance.StartBlink();
            }
            
        }

        #endregion

        #region Unity Methods
        private void OnValidate()
        {
            if (FPControler == null) FPControler = GetComponent<FPControler>();
            if (FPInteraction == null) FPInteraction = GetComponent<FPInteraction>();
            if(FPTeleport == null) FPTeleport = GetComponent<FPTeleport>();
            
        }
        private void Start()
        {
            CursorHide();

        }
        #endregion

        private void CursorHide()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }


}
