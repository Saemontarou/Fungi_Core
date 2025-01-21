using System;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private InventoryObject _inventoryObject;

    private void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            var inventoryItem = item.CreateItem();
            _inventoryObject.AddItem(inventoryItem, 1);
        }
    }

    // public void OnTriggerEnter2D(Collider2D other)
    // {
    //     var item = other.GetComponent<GroundItemWithoutMesh>();
    //     if (item)
    //     {
    //         Item _item = new Item(item.)
    //     }
    // }
}
