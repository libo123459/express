using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour {
    public bool inSlot = false;
    public bool overlap = false;
    public Collider2D _collision;
    //public Slot _slot;
    // Use this for initialization
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "slot")
        {
            inSlot = true;
            
        }
        if (collision.gameObject.tag == "item")
        {
            overlap = true;
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "slot")
        {
            inSlot = true;
            
        }
        if (collision.gameObject.tag == "item")
        {
            overlap = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "slot")
        {
            inSlot = false;
            
        }
        if (collision.gameObject.tag == "item")
        {
            overlap = false;
        }
    }
    void Start () {
		
	}

    private void Awake()
    {
        //this.GetComponent<Image>().color = new Color(0,0,0,1);
        this.GetComponent<Image>().sprite = Resources.Load<Sprite>("block01");
    }

    // Update is called once per frame
    void Update () {
		
	}

}
