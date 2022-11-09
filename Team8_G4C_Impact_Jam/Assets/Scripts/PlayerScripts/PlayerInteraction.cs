using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class PlayerInteraction : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;
    private bool _canInteract;

    private IInteractable _actualInteractable;

    [Header("Player Scripts")]
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerHoldItem _playerHoldItem;

    #region Getters & Setters
    public bool canInteract { get => _canInteract; set => _canInteract = value; }
    public PlayerMovement PlayerMovement { get => _playerMovement; }
    public PlayerHoldItem PlayerHoldItem { get => _playerHoldItem; }
    #endregion

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _canInteract = true;
    }

    private void OnEnable()
    {
        _playerInputActions.PlayerInteraction.Interact.Enable();
        _playerInputActions.PlayerInteraction.Interact.performed += PerformedInteraction;
    }

    private void OnDisable()
    {
        _playerInputActions.PlayerInteraction.Interact.performed -= PerformedInteraction;
        _playerInputActions.PlayerInteraction.Interact.Disable();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (_canInteract)
        {
            IInteractable _interactable = col.GetComponent<IInteractable>();
            if (_interactable != null)
            {
                _actualInteractable = _interactable;
            }
        }
        else
        {
            //Debug.Log("Ja esta carregando algo!!!");
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (_canInteract)
        {
            IInteractable _interactable = col.GetComponent<IInteractable>();
            if (_interactable != null)
            {
                _actualInteractable = null;
            }
        }
    }

    private void PerformedInteraction(InputAction.CallbackContext obj)
    {
        if (_actualInteractable != null)
        {
            _actualInteractable.Interact(this);
        }
        else
        {
            //Debug.Log("Nao existe item para interagir");
        }
    }

    public void SetActualInteractableNull()
    {
        _actualInteractable = null;
    }
}
