using UnityEngine;

public class GroundItem : MonoBehaviour
{
    [SerializeField] private ItemsObject _Item;


    public Item CreateItem()
    {
        return _Item.CreateItem();
    }
}
