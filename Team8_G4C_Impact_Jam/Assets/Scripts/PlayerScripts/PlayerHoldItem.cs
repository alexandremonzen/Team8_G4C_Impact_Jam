using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class PlayerHoldItem : MonoBehaviour
{
    [SerializeField] private Transform _holdItemOffset;
    [SerializeField] private Transform _dropItemOffset;
    private Item _actualHoldingItem;

    private PlayerInputActions _playerInputActions;

    public Item ActualHoldingItem { get => _actualHoldingItem; }

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _playerInputActions.PlayerHoldItem.Enable();
        _playerInputActions.PlayerHoldItem.DropItem.performed += DropItem;
    }

    private void OnDisable()
    {
        _playerInputActions.PlayerHoldItem.DropItem.performed += DropItem;
        _playerInputActions.PlayerHoldItem.Disable();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!_actualHoldingItem)
        {
            Item item = col.GetComponent<Item>();
            if (item)
            {
                item.transform.parent = _holdItemOffset.transform;
                item.transform.position = _holdItemOffset.position;
                _actualHoldingItem = item;
            }
        }
    }

    private void DropItem(InputAction.CallbackContext obj)
    {
        if (_actualHoldingItem)
        {
            _actualHoldingItem.transform.parent = null;
            _actualHoldingItem.transform.position = _dropItemOffset.position;
            _actualHoldingItem = null;
        }
    }

    public void DisappearActualItem()
    {
        if (_actualHoldingItem)
        {
            _actualHoldingItem.gameObject.SetActive(false);
            _actualHoldingItem.transform.parent = null;
            _actualHoldingItem = null;
        }
    }
}
