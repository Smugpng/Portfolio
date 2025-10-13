using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(FPControler))]
    public class FPPlayer : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] FPControler FPControler;

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
        void OnZoom(InputValue value)
        {

        }
        #endregion

        #region Unity Methods
        private void OnValidate()
        {
            if (FPControler == null) FPControler = GetComponent<FPControler>();
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
