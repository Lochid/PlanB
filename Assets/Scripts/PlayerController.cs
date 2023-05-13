using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 100;
    public float walkingSpeed = 1;
    public float runningSpeed = 2;

    private Vector3 moveDirection;
    private CharacterController _controller;

    private bool isRunning = false;

    public void OnRunning()
    {
        isRunning = true;
    }
    public void OnUnrunning()
    {
        isRunning = false;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        CalculateTurning();
        CalculateMovement();
    }

    void CalculateTurning()
    {
        float yawMouse = Input.GetAxis("Mouse X");
        float pitchMouse = -Input.GetAxis("Mouse Y");


        if (Mathf.Abs(yawMouse) > 0.1f || Mathf.Abs(pitchMouse) > 0.1f)
        {
            var targetFlyRotation = yawMouse * transform.right + pitchMouse * transform.up;
            targetFlyRotation.Normalize();
            targetFlyRotation *= Time.deltaTime * 3.0f;

            moveDirection += targetFlyRotation * rotationSpeed;
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }

    void CalculateMovement()
    {
        var vert = Input.GetAxis("Vertical");
        var hori = Input.GetAxis("Horizontal");
        var speed = isRunning ? runningSpeed : walkingSpeed;

        var _velocity = new Vector3(
            transform.forward.x * vert + transform.right.x * hori,
            0,
            transform.forward.z * vert + transform.right.z * hori
            );
        _controller.Move(-_velocity * Time.deltaTime * speed);
    }
}
