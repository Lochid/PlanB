using UnityEngine;
using UnityEngine.UI;

namespace PlanB
{
    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour
    {
        private float horizontalTurning => Input.GetAxis("Mouse X");
        private float verticalTurning => Input.GetAxis("Mouse Y");

        private float horizontalMoving => Input.GetAxis("Horizontal");
        private float verticalMoving => Input.GetAxis("Vertical");

        public KeyCode RunningKey = KeyCode.LeftShift;
        private bool isRunningPressed => Input.GetKey(RunningKey);

        public KeyCode JumpingKey = KeyCode.Space;
        private bool isJumpingPressed => Input.GetKey(JumpingKey);

        public KeyCode SittingKey = KeyCode.LeftControl;
        private bool isSittingPressed => Input.GetKey(SittingKey);

        public KeyCode UsingKey = KeyCode.E;
        private bool isUsingPressed => Input.GetKey(UsingKey);

        private CharacterController _controller;

        public Vector3 Gravity;
        public float Height = 1;
        public float JumpSpeed = 1;

        private bool IsGrounded => Physics.Raycast(transform.position, -Vector3.up, Height);
        private Vector3 _speed;
        private Vector3 gravityAcceleration => IsGrounded ? Vector3.zero : Gravity;
        private Vector3 acceleration => gravityAcceleration;

        public float crowdSpeed = 0.5f;
        public float walkingSpeed = 1;
        public float runningSpeed = 2;
        private float speedForUse => isSitting ? crowdSpeed : isRunningPressed ? runningSpeed : walkingSpeed;
        private Vector3 forwardSpeed => -new Vector3(
               transform.forward.x * verticalMoving,
               0,
               transform.forward.z * verticalMoving
            ) * speedForUse;
        private Vector3 sideSpeed => -new Vector3(
               transform.right.x * horizontalMoving,
               0,
               transform.right.z * horizontalMoving
            ) * speedForUse;
        private Vector3 movementSpeed => forwardSpeed + sideSpeed;
        private bool isSitting = false;

        private float originalHeight = 0;

        public float rotationSpeed = 1;

        private bool shouldTurn => Mathf.Abs(horizontalTurning) > 0.1f || Mathf.Abs(verticalTurning) > 0.1f;
        private Vector3 targetFlyDirection => (horizontalTurning * transform.right - verticalTurning * transform.up) * rotationSpeed;
        private Vector3 moveDirection;
        public Transform rayAnchor;
        public float rayDistance = 20;
        private Transform realRayAnchor => rayAnchor == null ? transform : rayAnchor;
        private Usable objectForUse = null;
        public Text uiText;
        private bool isUsing = false;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _controller = GetComponent<CharacterController>();
            originalHeight = _controller.height;
        }

        void Update()
        {
            UpdateTurning();
        }

        void FixedUpdate()
        {
            UpdateMovement();
            UpdateSitting();
            UpdateUsable();
        }

        void UpdateTurning()
        {
            if (shouldTurn)
            {
                moveDirection += targetFlyDirection;
                transform.rotation = Quaternion.LookRotation(moveDirection * Time.deltaTime);
            }
        }
        void UpdateMovement()
        {
            _speed += acceleration * Time.deltaTime;
            if (IsGrounded)
            {
                if (!isSitting)
                {
                    if (_speed.y < 0)
                    {
                        _speed.y = 0;
                    }
                    if (isJumpingPressed)
                    {
                        _speed.y = JumpSpeed;
                    }
                }

                var moveSpeed = movementSpeed;
                _speed.x = moveSpeed.x;
                _speed.z = moveSpeed.z;
            }

            _controller.Move(_speed * Time.deltaTime);
        }
        void UpdateSitting()
        {
            if (IsGrounded && isSittingPressed && !isSitting)
            {
                isSitting = true;
                _controller.height = originalHeight / 2;
                _controller.Move(Vector3.down * originalHeight / 4);
            }
            else if (!isSittingPressed && isSitting)
            {
                isSitting = false;
                _controller.height = originalHeight;
                _controller.Move(Vector3.up * (originalHeight / 2 - 0.5f));
            }
        }
        void UpdateUsable()
        {
            RaycastHit hit;
            if (Physics.Raycast(realRayAnchor.position, realRayAnchor.TransformDirection(Vector3.forward), out hit, rayDistance))
            {
                objectForUse = hit.collider.GetComponent<Usable>();
            }
            else
            {
                objectForUse = null;
            }

            if (objectForUse != null && objectForUse.Active)
            {
                uiText.text = $"Press \"{UsingKey}\"\n For {objectForUse.ActionName}";
            }
            else
            {
                uiText.text = "";
            }

            if (isUsingPressed)
            {
                uiText.text = "";
            }

            if (isUsingPressed && !isUsing)
            {
                isUsing = true;

                if (objectForUse != null && objectForUse.Active)
                {
                    objectForUse.Use();
                }
            }
            else if (!isUsingPressed && isUsing)
            {
                isUsing = false;
            }
        }
    }
}