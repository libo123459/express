using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable_order : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler {

    Transform parentReturnTo;
    GameObject placeholder;

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentReturnTo = this.transform.parent.transform;
        this.transform.SetParent(this.transform.parent.parent);
        
        placeholder = new GameObject();
        
        RectTransform rt = placeholder.AddComponent<RectTransform>();
        //im.color = new Color(0.5f,0.5f,0.5f,1);
        rt.sizeDelta = this.GetComponent<RectTransform>().sizeDelta;
        placeholder.transform.SetParent(parentReturnTo);
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
       
        for (int i = 0; i < parentReturnTo.childCount; i++)
        {
            if (this.transform.localPosition.y > parentReturnTo.GetChild(i).transform.localPosition.y)
            {
                placeholder.transform.SetSiblingIndex(i);
                break;
            }
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(parentReturnTo);
        
        this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        DestroyImmediate(placeholder);

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
