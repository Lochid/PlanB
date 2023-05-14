using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float height = 1;
    public float highHeight = 1;
    public float rotationSpeed = 100;
    public float walkingSpeed = 1;
    public float runningSpeed = 2;
    public float jumpForce = 1;

    public Vector3 Gravity;

    private Vector3 moveDirection;
    private CharacterController _controller;

    private bool isRunning = false;
    private bool isJumping = false;

    private float horizontalTurning = 0;
    private float verticalTurning = 0;

    private float horizontalMoving = 0;
    private float verticalMoving = 0;

    bool IsGrounded => Physics.Raycast(transform.position, -Vector3.up, height);

    bool IsHighGrounded => Physics.Raycast(transform.position, -Vector3.up, highHeight);
    bool startJump = false;
    public void OnRunning()
    {
        isRunning = true;
    }
    public void OnUnrunning()
    {
        isRunning = false;
    }

    public void OnJumping()
    {
        isJumping = true;
    }
    public void OnUnjumping()
    {
        isJumping = false;
    }
    public void OnHorizontalTurning(float horizontalTurning)
    {
        this.horizontalTurning = horizontalTurning;
    }
    public void OnVerticalTurning(float verticalTurning)
    {
        this.verticalTurning = verticalTurning;
    }
    public void OnHorizontalMoving(float horizontalMoving)
    {
        this.horizontalMoving = horizontalMoving;
    }
    public void OnVerticalMoving(float verticalMoving)
    {
        this.verticalMoving = verticalMoving;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Debug.Log(IsGrounded);
        CalculateTurning();
        CalculateMovement();
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

    void CalculateMovement()
    {
        var horizontalVelocity = Vector3.zero;
        var verticalVelocity = Vector3.zero;
        var jumpVelocity = Vector3.zero;
            var speed = isRunning ? runningSpeed : walkingSpeed;
            horizontalVelocity = -new Vector3(
               transform.right.x * horizontalMoving,
               0,
               transform.right.z * horizontalMoving
            ) * speed;
            verticalVelocity = -new Vector3(
               transform.forward.x * verticalMoving,
               0,
               transform.forward.z * verticalMoving
            ) * speed;

        if (IsGrounded)
        {
            if (isJumping && !startJump)
            {
                startJump = true;
            }
        }

        if (startJump)
        {
            if (!IsHighGrounded)
            {
                startJump = false;
            }
            jumpVelocity = new Vector3(
              0,
              jumpForce,
              0
           );
        }
        _controller.Move((Gravity + horizontalVelocity + verticalVelocity + jumpVelocity) * Time.deltaTime);
    }
}
