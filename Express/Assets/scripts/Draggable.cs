using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{
    public Item _item;
    
    public bool OutOfSlot = true;

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
            print(hit.collider.gameObject.name);
            if (hit.collider.gameObject.tag == "slot" && IfAllIn == true)
            {
                this.transform.position = hit.collider.transform.position;
            }         
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
