using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{
    Vector3 startPos;
    Vector3 targetPos;
    bool OutOfSlot = true;
    public void OnBeginDrag(PointerEventData eventData)
    {
        startPos = this.transform.position;
    }
        
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (OutOfSlot == true)
        {
            this.transform.position = eventData.position;
        }
        else {
            this.transform.position = targetPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "slot")
        {
            OutOfSlot = false;
            targetPos = collision.transform.position;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "slot")
        {
            OutOfSlot = true;
        }
    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
