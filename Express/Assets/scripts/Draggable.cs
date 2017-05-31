using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{
    Item _item;
    public Slot _slot;
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
        
    }
        
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        LayerMask mask = 1 << LayerMask.NameToLayer("slot");
        RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector3.forward,100,mask.value);
        if (hit.collider != null)
        {
            
            if (IfAllIn == true)
            {
                this.transform.position = hit.collider.transform.position;
               
            }
        }
        bool a = CheckOverlay();
        print(a.ToString());
    }

    bool CheckOverlay()
    {
        int num = 0;
        for (int i = 0; i < _item.BlockList.Count; i++)
        {
            if (_item.BlockList[i].inItem == true)
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

    // Use this for initialization
    void Start()
    {
        _item = this.GetComponent<Item>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
