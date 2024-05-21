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
        player = GetComponent<Player>();
    }


    void Start()
    {
        if (Persistence.Instance.NextPlayerLocation != Persistence.UseSceneDefault)
        {
            transform.position = Persistence.Instance.NextPlayerLocation;
        }
        if (_characterController == null)
        {
            _characterController = GetComponent<CharacterController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        ApplyRotation();

        if (player.CutsceneState) return;

        _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        _direction = new Vector3(_input.x, 0f, _input.y);

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
    private void ApplyRotation()
    {
        if (_input.sqrMagnitude != 0)
        {
            var targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }

    public void SetCutsceneState(bool state)
    {
        _characterController.enabled = state;
    }
}
