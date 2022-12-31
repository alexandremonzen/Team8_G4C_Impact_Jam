using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemType _itemType;

    public ItemType ItemType { get => _itemType; }
}
