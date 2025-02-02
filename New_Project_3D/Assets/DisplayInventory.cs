using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    private void OnDisable()
    {
        DestroySlot();
    }

    private void DestroySlot()
    {
        
    }
    
    private void Update()
    {
        UpdateSlot();
    }

    private void CreateSlot()
    {
        _itemDisplayed = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < _inventorySO.Container.Items.Length; i++)
        {
            var cellObj = Instantiate(_inventoryCellPrefab, Vector3.zero, transform.rotation, transform);
            cellObj.GetComponent<RectTransform>().localPosition = Vector3.zero;
            cellObj.GetComponent<RectTransform>().anchoredPosition = GetPosition(i);
            
            AddEvent(cellObj, EventTriggerType.PointerEnter, delegate { OnEnter(cellObj); });
            AddEvent(cellObj, EventTriggerType.PointerExit, delegate { OnExit(cellObj); });
            AddEvent(cellObj, EventTriggerType.BeginDrag, delegate { OnBeginDrag(cellObj); });
            AddEvent(cellObj, EventTriggerType.Drag, delegate { OnDrag(cellObj); });
            AddEvent(cellObj, EventTriggerType.EndDrag, delegate { OnDragEnd(cellObj); });
            
            // SetSlotData(cellObj, _inventorySO.Container.Items[i]);
            
            
            
            _itemDisplayed.Add(cellObj, _inventorySO.Container.Items[i]);
        }
    }

    private void UpdateSlot()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in _itemDisplayed)
        {
            if (_slot.Value.SlotID >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite =
                    _inventorySO.Database.GetItem[_slot.Value.item.Id].Icon;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.transform.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.transform.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }

    private void SetSlotData(GameObject cellObj, InventorySlot slot)
    {
        var imageSprite = cellObj.transform.GetChild(0).GetComponentInChildren<Image>();
        imageSprite.enabled = !slot.IsEmpty;
        imageSprite.sprite = slot.IsEmpty ? null : _inventorySO.GetSpriteByItemId(slot.item.Id);
        cellObj.transform.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount == 1 ? "" : slot.amount.ToString("n0");
    }

    private void OnEnter(GameObject cellObj)
    {
        cellObj.transform.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
        _mouseItem.hoverObj = cellObj;
    }

    private void OnExit(GameObject cellObj)
    {
        cellObj.transform.GetComponent<Image>().color = new Color(0.25f, 0.25f, 0.25f, 1);
        _mouseItem.hoverObj = null;
    }

    private void OnBeginDrag(GameObject cellObj)
    {
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(50, 50);
        mouseObject.transform.SetParent(transform.parent);
        if (_itemDisplayed[cellObj].SlotID >= 0)
        {
            var img = mouseObject.GetComponent<Image>();
            img.sprite = _inventorySO.Database.GetItem[_itemDisplayed[cellObj].item.Id].Icon; // ЗДЕСЬ ЕСТЬ ОШИБКА!!!
            img.raycastTarget = false;
        }

        _mouseItem.hoverObj = mouseObject;
        _mouseItem.draggetSlot = _itemDisplayed[cellObj];
    }

    private void OnDrag(GameObject cellObj)
    {
        if (_mouseItem.hoverObj != null)
        {
            // RectTransformUtility.ScreenPointToWorldPointInRectangle(_parentCanvas.transform as RectTransform,
            //     Input.mousePosition,
            //     Camera.main,
            //     out Vector3 position
            //     );
            // // _mouseItem.draggetObj.transform.position = _parentCanvas.transform.TransformPoint(position);
            // _mouseItem.draggetObj.GetComponent<RectTransform>().position = Input.mousePosition;

            RectTransform rt = _mouseItem.hoverObj.GetComponent<RectTransform>();
            rt.position = Input.mousePosition;
        }
    }

    private void OnDragEnd(GameObject cellObj)
    {
        if (_mouseItem.hoverObj)
        {
            _inventorySO.MoveItem(_itemDisplayed[cellObj], _itemDisplayed[_mouseItem.hoverObj]);
        }
        else
        {
            _inventorySO.RemoveItem(_itemDisplayed[cellObj].item);
        }
        Destroy(_mouseItem.draggetObj);
        _mouseItem.hoverObj = null;
        _mouseItem.draggetSlot = null;
    }

    private void AddEvent(GameObject target, EventTriggerType triggerType, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = target.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = triggerType;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
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


