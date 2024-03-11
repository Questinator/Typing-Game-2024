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
    public float jumpHight;
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpHight;
            }
            else
            {
                moveDirection.y = 0;
            }
        }
        Vector3 normalized = new Vector3(moveDirection.x, 0, moveDirection.z).normalized;
        characterController.Move(new Vector3(normalized.x, moveDirection.y, normalized.z) * Time.deltaTime * moveSpeed);
    }
}
