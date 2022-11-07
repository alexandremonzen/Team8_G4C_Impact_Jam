using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class PlayerJump : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private float _jumpForce;

    private Rigidbody _rigidbody;
    private PlayerInputActions _playerInputActions;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _playerInputActions.PlayerJump.Enable();
        _playerInputActions.PlayerJump.Jump.performed += PerformJump;
    }

    private void OnDisable()
    {
        _playerInputActions.PlayerJump.Jump.performed -= PerformJump;
        _playerInputActions.PlayerJump.Disable();
    }

    private void PerformJump(InputAction.CallbackContext obj)
    {
        _rigidbody.AddRelativeForce(Vector3.back * _jumpForce, ForceMode.Impulse);
    }
}
