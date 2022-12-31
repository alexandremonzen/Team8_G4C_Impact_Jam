using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class PlayerMovement : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private float _moveSpeed;

    private Rigidbody2D _rigidbody;
    private PlayerInputActions _playerInputActions;
    private InputAction _movementAction;

    private Vector2 _movementVector;

    public Animator _flip;

    public Rigidbody2D Rigidbody { get => _rigidbody; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerInputActions = new PlayerInputActions();
        _movementAction = _playerInputActions.PlayerMovement.Movement;
    }

    private void OnEnable()
    {
        _playerInputActions.PlayerMovement.Enable();
        _movementAction.Enable();
    }

    private void OnDisable()
    {
        _playerInputActions.PlayerMovement.Disable();
        _movementAction.Disable();
    }

    private void Update()
    {
        HandleInput();
        if (_movementVector.x > 0)
            _flip.SetBool("Active", false);
        if (_movementVector.x < 0)
            _flip.SetBool("Active", true);
    }

    private void FixedUpdate()
    {
        HandleMovementPhysics();
    }

    private void HandleInput()
    {
        _movementVector = _movementAction.ReadValue<Vector2>();
    }

    private void HandleMovementPhysics()
    {
        _rigidbody.velocity = new Vector3(_movementVector.x * _moveSpeed, _movementVector.y * _moveSpeed, 0);
    }

    public void RemoveAllMovement()
    {
        _playerInputActions.PlayerMovement.Disable();
        _movementAction.Disable();
    }

    public void ReturnAllMovement()
    {
        _playerInputActions.PlayerMovement.Enable();
        _movementAction.Enable();
    }
}
