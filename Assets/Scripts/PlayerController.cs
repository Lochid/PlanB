using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
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
    // ============================================


    public float rotationSpeed = 100;
    private Vector3 moveDirection;





    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _controller = GetComponent<CharacterController>();
        originalHeight = _controller.height;
    }

    void Update()
    {
        CalculateTurning();
        UpdateMovement();
        UpdateSitting();
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

    void CalculateTurning()
    {
        if (Mathf.Abs(horizontalTurning) > 0.1f || Mathf.Abs(verticalTurning) > 0.1f)
        {
            var targetFlyRotation = horizontalTurning * transform.right - verticalTurning * transform.up;
            targetFlyRotation.Normalize();
            targetFlyRotation *= Time.deltaTime * 3.0f;

            moveDirection += targetFlyRotation * rotationSpeed;
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }
}
