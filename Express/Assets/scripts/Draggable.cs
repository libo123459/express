using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{
    public SlotData _slotData;
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
        
        startPos = this.transform.position;
        
    }
        
    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if (IfAllIn == true)
        {
            
            if (Overlap() == false)
            {
                this.transform.position = _item.SlotPos;
                
            }
            else {
                this.transform.position = startPos;
                
            }
        }
        
    }
    // Use this for initialization
    void MakeSlotSelected()
    {
        for (int i = 0; i < _slotData.SlotsList.Count; i++)
        {
            if (_slotData.SlotsList[i].currentUsed == true)
            {
                _slotData.SlotsList[i].isSelected = true;
            }
        }
    }

    bool Overlap()
    {
        int num = 0;
        for (int i = 0; i < _item.BlockList.Count; i++)
        {
            if (_item.BlockList[i]._collision != null)
            {
                num++;
            }
        }

        if (num == 0)
        {
            return false;
        }
        else {
            return true;
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
