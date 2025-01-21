using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Default", menuName = "Inventory System/Items/DefaultObject")]
public class DefaultObject : ItemsObject
{
    private void Awake()
    {
        type = ItemType.Trash;
    }
}
