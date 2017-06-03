﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{
    
    Item _item;
    Vector3 startPos;
    Card _card;

    CardsManage _cardManage;
    OrderManage _orderManage;

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
        if (IfAllIn == true && CheckOverlap() == false)
        {
            this.transform.position = _item.SlotPos;
            _orderManage.AddTheOrder(_card);
        }
        else {
            this.transform.position = startPos;
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
