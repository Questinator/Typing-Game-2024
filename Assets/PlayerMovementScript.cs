using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovementScript : MonoBehaviour
{
    public Vector3 moveDirection = new Vector3();

    public float moveSpeed;
    public float velocity;
    public float terminalVelocity;
    CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        if (characterController == null)
        {
            characterController = GetComponent<CharacterController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), moveDirection.y, Input.GetAxis("Vertical"));
        //moveDirection = moveDirection.normalized;
        if (!characterController.isGrounded)
        {
            if (moveDirection.y > -terminalVelocity)
            {
                moveDirection.y -= Time.deltaTime * velocity;
            }
        } else
        {
            moveDirection.y = -2;
        }
        characterController.Move(moveDirection * Time.deltaTime * moveSpeed);
    }
}
