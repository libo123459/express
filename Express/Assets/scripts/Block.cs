using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
    public bool inSlot = false;
    public bool inItem = false;
    public Slot _slot;
    // Use this for initialization
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "slot")
        {
            _slot = collision.GetComponent<Slot>();
            inSlot = true;
        }
        if (collision.gameObject.tag == "item")
        {
            inItem = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "slot")
        {
            _slot = collision.GetComponent<Slot>();
            inSlot = true;
        }

        if (collision.gameObject.tag == "item")
        {
            inItem = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "slot")
        {
            _slot = null;
            inSlot = false;
        }

        if (collision.gameObject.tag == "item")
        {
            inItem = false;
        }
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
