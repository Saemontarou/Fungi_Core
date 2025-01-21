using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayInventory : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryCellPrefab;
    [SerializeField] private InventoryObject _inventorySO;
    public int X_START;
    public int Y_START;
    public int NUMBER_OF_COLUMN;
    public int X_SPACE_ITEM;
    public int Y_SPACE_ITEM;

    private Dictionary<GameObject, InventorySlot> _itemDisplayed;
    private Canvas _parentCanvas;
    private MouseItem _mouseItem;


    private void Start()
    {
        _mouseItem = new MouseItem();
        _parentCanvas = transform.parent.GetComponent<Canvas>();
    }

    private void OnEnable()
    {
        CreateSlot();
    }

    private void CreateSlot()
    {
        _itemDisplayed = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < _inventorySO.Container.Items.Length; i++)
        {
            var cellObj = Instantiate(_inventoryCellPrefab, Vector3.zero, transform.rotation, transform);
            cellObj.GetComponent<RectTransform>().localPosition = Vector3.zero;
            cellObj.GetComponent<RectTransform>().anchoredPosition = GetPosition(i);
            _itemDisplayed.Add(cellObj, _inventorySO.Container.Items[i]);
        }
    }


    private Vector3 GetPosition(int index)
    {

        float x = X_START + X_SPACE_ITEM * (index % NUMBER_OF_COLUMN);
        float y = -(Y_START + Y_SPACE_ITEM * (index / NUMBER_OF_COLUMN));
        return new Vector3(x, y, 0);
        
    }
}

public class MouseItem
{
    public GameObject hoverObj;
    public GameObject draggetObj;
    public InventorySlot draggetSlot;
}


