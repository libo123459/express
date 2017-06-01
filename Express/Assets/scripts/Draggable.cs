using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{
    Item _item;
    Vector3 startPos;
    bool IfAllIn
    {
        get
        {
            int inNum = 0;
            for (int i = 0; i < _item.BlockList.Count; i++)
            {
                if (_item.BlockList[i].inSlot == true)
                {
                    inNum++;
                }
                else
                {
                    continue;
                }
            }
            if (inNum == _item.BlockList.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        MakeTheSlotSelected(false);
        startPos = this.transform.position;
    }
        
    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.position = _item.SlotPos;
        MakeTheSlotSelected(true);
    }
    // Use this for initialization
    void MakeTheSlotSelected(bool _selected)
    {
        for (int i = 0; i < _item.BlockList.Count; i++)
        {
            if (_item.BlockList[i]._slot != null)
            {
                _item.BlockList[i]._slot.isSelected = _selected;
            }
        }
    }

    void Start()
    {
        _item = this.GetComponent<Item>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
