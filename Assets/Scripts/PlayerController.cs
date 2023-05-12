using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 100;

    private Vector3 moveDirection;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float yawMouse = Input.GetAxis("Mouse X");
        float pitchMouse = Input.GetAxis("Mouse Y");


        if (Mathf.Abs(yawMouse) > 0.1f || Mathf.Abs(pitchMouse) > 0.1f)
        {
            var targetFlyRotation = yawMouse * transform.right + pitchMouse * transform.up;
            targetFlyRotation.Normalize();
            targetFlyRotation *= Time.deltaTime * 3.0f;

            float limitX = Quaternion.LookRotation(moveDirection + targetFlyRotation).eulerAngles.x;

            if (!(limitX < 90 && limitX > 70 || limitX > 270 && limitX < 290))
            {
                moveDirection += targetFlyRotation;
                transform.rotation = Quaternion.LookRotation(moveDirection);
            }

        }
    }
}
