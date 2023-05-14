using UnityEngine;
using UnityEngine.Events;

namespace PlanB
{
    public class InputController : MonoBehaviour
    {
        private float prevHorizontalTurning = 0;
        private float horizontalTurning => Input.GetAxis("Mouse X");
        private bool shouldInvokeChangeHorizontalTurning => prevHorizontalTurning != horizontalTurning;
        public UnityEvent<float> OnChangeHorizontalTurning;
        // ============================================
        private float prevVerticalTurning = 0;
        private float verticalTurning => Input.GetAxis("Mouse Y");
        private bool shouldInvokeChangeVerticalTurning => prevVerticalTurning != verticalTurning;
        public UnityEvent<float> OnChangeVerticalTurning;
        // ============================================
        private float prevHorizontalMoving = 0;
        private float horizontalMoving => Input.GetAxis("Horizontal");
        private bool shouldInvokeChangeHorizontalMoving => prevHorizontalMoving != horizontalMoving;
        public UnityEvent<float> OnChangeHorizontalMoving;
        // ============================================
        private float prevVerticalMoving = 0;
        private float verticalMoving => Input.GetAxis("Vertical");
        private bool shouldInvokeChangeVerticalMoving => prevVerticalMoving != verticalMoving;
        public UnityEvent<float> OnChangeVerticalMoving;
        // ============================================

        private bool isPrevRunning = false;
        private bool isRunning => Input.GetKey(RunningKey);
        private bool shouldInvokeStartRunning => isRunning && !isPrevRunning;
        private bool shouldInvokeStopRunning => !isRunning && isPrevRunning;

        public KeyCode RunningKey = KeyCode.LeftShift;

        public UnityEvent OnStartRunning;
        public UnityEvent OnStopRunning;

        // ============================================

        private bool isPrevJumping = false;
        private bool isJumping => Input.GetKey(JumpingKey);
        private bool shouldInvokeStartJumping => isJumping && !isPrevJumping;
        private bool shouldInvokeStopJumping => !isJumping && isPrevJumping;

        public KeyCode JumpingKey = KeyCode.Space;

        public UnityEvent OnStartJumping;
        public UnityEvent OnStopJumping;


        void Update()
        {
            HandleTurning();
            HandleMoving();
            HandleRunning();
            HandleJumping();
        }

        void HandleTurning()
        {
            HandleHorizontalTurning();
            HandleVerticalTurning();
        }

        void HandleHorizontalTurning()
        {
            if (shouldInvokeChangeHorizontalTurning)
            {
                OnChangeHorizontalTurning?.Invoke(horizontalTurning);
            }
            prevHorizontalTurning = horizontalTurning;
        }

        void HandleVerticalTurning()
        {
            if (shouldInvokeChangeVerticalTurning)
            {
                OnChangeVerticalTurning?.Invoke(verticalTurning);
            }
            prevVerticalTurning = verticalTurning;
        }

        void HandleMoving()
        {
            HandleHorizontalMoving();
            HandleVerticalMoving();
        }

        void HandleHorizontalMoving()
        {
            if (shouldInvokeChangeHorizontalMoving)
            {
                OnChangeHorizontalMoving?.Invoke(horizontalMoving);
            }
            prevHorizontalMoving = horizontalMoving;
        }

        void HandleVerticalMoving()
        {
            if (shouldInvokeChangeVerticalMoving)
            {
                OnChangeVerticalMoving?.Invoke(verticalMoving);
            }
            prevVerticalMoving = verticalMoving;
        }

        void HandleRunning()
        {
            if (shouldInvokeStartRunning)
            {
                OnStartRunning?.Invoke();
            }
            else if (shouldInvokeStopRunning)
            {
                OnStopRunning?.Invoke();
            }
            isPrevRunning = isRunning;
        }

        void HandleJumping()
        {
            if (shouldInvokeStartJumping)
            {
                OnStartJumping?.Invoke();
            }
            else if (shouldInvokeStopJumping)
            {
                OnStopJumping?.Invoke();
            }
            isPrevJumping = isJumping;
        }
    }
}