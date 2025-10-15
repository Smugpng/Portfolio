using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(FPControler))]
    [RequireComponent(typeof(FPInteraction))]
    public class FPPlayer : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] FPControler FPControler;
        [SerializeField] FPInteraction FPInteraction;

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
        #endregion

        #region Unity Methods
        private void OnValidate()
        {
            if (FPControler == null) FPControler = GetComponent<FPControler>();
            if (FPInteraction == null) FPInteraction = GetComponent<FPInteraction>();
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
