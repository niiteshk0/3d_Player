using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScipt : MonoBehaviour
{

    public float playerSpeed = 2f;
    public CharacterController characterController;

    [Header("Ground Check")]
    public float gravity = -9.81f;
    public float jumpForce = 1f;
    Vector3 velocity;
    public Transform groundCheck;
    bool isGround;
    public float groundDistance = 0.4f;
    public LayerMask groundLayerMask;

    [Header("For cinemachine Variables")]
    public Transform playerCamera;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelcity;

    private void Update()
    {
        isGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayerMask);      // ground Check code 

        if(isGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);              // till here code of Ground Check


        playerMovement();
        Jump();
    }

    void playerMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(moveX, 0f, moveZ).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelcity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);
        }
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2 * gravity);
        }
    }
}