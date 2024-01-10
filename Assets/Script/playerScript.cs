using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScipt : MonoBehaviour
{

    public float playerSpeed = 2f;
    public CharacterController characterController;

    private void Update()
    {
        playerMovement();
    }

    void playerMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(moveX, 0f, moveZ).normalized;

        if (direction.magnitude >= 0.1f)
        {
            characterController.Move(direction.normalized * playerSpeed * Time.deltaTime);
        }
    }
}