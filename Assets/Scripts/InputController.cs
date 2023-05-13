using UnityEngine;
using UnityEngine.Events;

namespace PlanB
{
    public class InputController : MonoBehaviour
    {
        public KeyCode RunningKey = KeyCode.LeftShift;

        public UnityEvent OnStartRunning;
        public UnityEvent OnStopRunning;

        private bool isPrevRunning = false;
        private bool isRunning => Input.GetKey(RunningKey);
        private bool shouldInvokeStartRunning => isRunning && !isPrevRunning;
        private bool shouldInvokeStopRunning => !isRunning && isPrevRunning;


        void Update()
        {
            HandleRunning();
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
    }
}