using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabaseObject", menuName = "Inventory System/Database/ItemDatabaseObject")]

public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{

    public ItemsObject[] Items;

    public Dictionary<int, ItemsObject> GetItem;
    
    
    public void OnBeforeSerialize()
    {
        // GetItem = new Dictionary<int, ItemsObject>();
    }

    public void OnAfterDeserialize()
    {

        GetItem = new Dictionary<int, ItemsObject>();
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].ID = i;
            GetItem.TryAdd(i, Items[i]);
        }

    }

}
