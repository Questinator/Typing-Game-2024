    using System;
using UnityEngine;


[RequireComponent(typeof(CharacterController)), RequireComponent(typeof(Player))]

public class PlayerMovementScript : MonoBehaviour
{
    // Movement variables
    private Vector2 _input;
    private CharacterController _characterController;
    private Vector3 _direction;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float sprintIncrease;
    [SerializeField] private float rotSpeed;
    [SerializeField] private float sprintRotIncrease;


    // Gravity variables
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float _velocity;
    private float _terminalVelocity = -30;

    // Rotation variables
    [SerializeField] private float smoothTime = 0.05f;
    private float _currentVelocity;

    private Player player;

    void Awake()
    {
        if (Persistence.Instance.NextPlayerLocation != Persistence.UseSceneDefault)
        {
            transform.position = Persistence.Instance.NextPlayerLocation;
        }
        player = GetComponent<Player>();
    }


    void Start()
    {

        if (_characterController == null)
        {
            _characterController = GetComponent<CharacterController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.CutsceneState) return;
        float forward = Input.GetAxisRaw("Vertical") * _moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? sprintIncrease : 1);
        float rotation = Input.GetAxisRaw("Horizontal") * rotSpeed * (Input.GetKey(KeyCode.LeftShift) ? sprintRotIncrease : 1) * Time.deltaTime;
        
        
        _direction = transform.forward * forward;
        transform.rotation = Quaternion.Euler(0,transform.rotation.eulerAngles.y + rotation,0); 
        
        ApplyGravity();
        
        _characterController.Move(_direction * _moveSpeed * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if (_characterController.isGrounded && _velocity < 0.0f)
        {
            _velocity = -1.0f;
        }
        else
        {
            if (_velocity > _terminalVelocity)
            {
                _velocity += _gravity * gravityMultiplier * Time.deltaTime;
            }
        }
        _direction.y = _velocity;
    }
}
