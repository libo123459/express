using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            _collision = collision;
            Slot _slot = collision.GetComponent<Slot>();
            if (_slot.isSelected == true)
            {
                overlap = true;
            }
            //_slot = collision.GetComponent<Slot>();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "slot")
        {
            inSlot = true;
            Slot _slot = collision.GetComponent<Slot>();
            if (_slot.isSelected == true)
            {
                overlap = true;
            }
            //_slot = collision.GetComponent<Slot>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "slot")
        {
            _collision = null;
            overlap = false;
            //_slot = null;
            inSlot = false;
        }
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
