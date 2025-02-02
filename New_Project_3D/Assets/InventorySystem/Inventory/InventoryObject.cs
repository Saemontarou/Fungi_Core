using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory Object")]
public class InventoryObject : ScriptableObject
{
    public ItemDatabaseObject Database;
    public Inventory Container;


    public Sprite GetSpriteByItemId(int id)
    {
        if (Database.GetItem.TryGetValue(id, out var slot))
        {
            return slot.Icon;
        }

        return null;
    }

    public void AddItem(Item item, int amount)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].SlotID == item.Id)
            {
                Container.Items[i].AddAmount(amount);
                return;
            }
        }

        SetEmptySlot(item, amount);
    }

    public void RemoveItem(Item item)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].item == item)
            {
                Container.Items[i].UpdateSlot(-1, null, 0);
            }
        }
    }

    public void MoveItem(InventorySlot fromSlot, InventorySlot toSlot)
    {
        InventorySlot temp = new InventorySlot(toSlot.SlotID, toSlot.item, toSlot.amount);
        toSlot.UpdateSlot(fromSlot.SlotID, fromSlot.item, fromSlot.amount);
        fromSlot.UpdateSlot(temp.SlotID, temp.item, temp.amount);
    }

    private InventorySlot SetEmptySlot(Item item, int amount)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].SlotID <= -1)
            {
                Container.Items[i].UpdateSlot(item.Id, item, amount);
                return Container.Items[i];
            }
        }
        return null;
    }

}

[System.Serializable]
public class Inventory
{
    public InventorySlot[] Items = new InventorySlot[20];
    
}

[System.Serializable]
public class InventorySlot
{
    public int SlotID = -1;
    public Item item;
    public int amount;

    internal bool IsEmpty => item.Id < 0;

    public InventorySlot()
    {
        SlotID = -1;
        item = null;
        amount = 0;
    }

    public InventorySlot(int slotID, Item item, int amount)
    {
        SlotID = slotID;
        this.item = item;
        this.amount = amount;
    }

    public void UpdateSlot(int slotID, Item item, int amount)
    {
        SlotID = slotID;
        this.item = item;
        this.amount = amount;
        
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}
