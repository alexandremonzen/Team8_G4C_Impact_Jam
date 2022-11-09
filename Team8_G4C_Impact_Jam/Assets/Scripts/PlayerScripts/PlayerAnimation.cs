using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerAnimation : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private Animator _animator;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        _animator.SetFloat("velocity", _playerMovement.Rigidbody.velocity.magnitude);
        _animator.SetFloat("velocityX", _playerMovement.Rigidbody.velocity.x);
    }
}
