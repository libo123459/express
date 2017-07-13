using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{
    
    public Item _item;
    
    public Card _card;

    public CardsManage _cardManage;
    public OrderManage _orderManage;

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
        this.transform.localScale = new Vector3(1,1,1);
    }
        
    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if (IfAllIn == true && CheckOverlap() == false)
        {
            this.transform.position = _item.SlotPos;
            _orderManage.AddTheOrder(_card);            
        }
        else {
            this.transform.localPosition = -_card._item.CenterPos * 0.5f;
            this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            _orderManage.CancelOrder(_card.ID);
        }
    }
    // Use this for initialization

    bool CheckOverlap()
    {
        int OverNum = 0;
        for (int i = 0; i < _item.BlockList.Count; i++)
        {
            if (_item.BlockList[i].overlap == true)
            {
                OverNum++;
            }
            else {
                continue;
            }
        }

        if (OverNum == 0)
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
        _cardManage = GameObject.Find("Manage").GetComponent<CardsManage>();
        _orderManage = GameObject.Find("Manage").GetComponent<OrderManage>();
        _card = this.transform.parent.GetComponent<Card>();
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
    
}
