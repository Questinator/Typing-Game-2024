using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovementScript : MonoBehaviour
{
    // Creates the Vector3 the player will be useing to move
    public Vector3 moveDirection = new Vector3();

    // Changeable variables for varous movement
    public float moveSpeed;
    public float velocity;
    public float terminalVelocity;
    public float jumpHight;
    public float rotationSpeed;

    // creates the characterController variable so the player can move
    CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
       // if characterController isnt assigned to anything it gets assigned
        if (characterController == null)
        {
            characterController = GetComponent<CharacterController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Recives input from the player and stores it in a vector
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), moveDirection.y, Input.GetAxis("Vertical"));
        
        // Asks if the characater is not grounded
        if (!characterController.isGrounded)
        {
            // increases downward movement if terminal velocity has not been reached
            if (moveDirection.y > -terminalVelocity)
            {
                moveDirection.y -= Time.deltaTime * velocity;
            }
        } else
        {
            // if the character is grounded then check if the user pressed space 
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // increases upward movement by jumphight
                moveDirection.y = jumpHight;
            }
            else
            {
                //sets up and downward movement to -1 so the player is and stays grounded when on the ground
                moveDirection.y = 0;
            }
        }

        // Some really goofy rotation
        Quaternion toRotation = Quaternion.LookRotation(moveDirection, transform.position);
        // transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        if(characterController.isGrounded)
        {
            Mathf.Clamp(transform.rotation.eulerAngles.x, 0f, 0f);
            Mathf.Clamp(0f, 0f, transform.rotation.eulerAngles.z);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        // Normilises x and z input so diagonal movement isnt really fast
        Vector3 normalized = new Vector3(moveDirection.x, 0, moveDirection.z).normalized;

        // Moves the player
        characterController.Move(new Vector3(normalized.x, moveDirection.y, normalized.z) * Time.deltaTime * moveSpeed);
    }

}
