using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Slot : MonoBehaviour {
    public bool isSelected;
    public bool currentUsed;
    public Collider2D _collision;
   
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.isSelected != true)
        {
            if (collision.gameObject.tag == "item")
            {
                this.GetComponent<Image>().color = new Color(0, 0, 0, 1);
                currentUsed = true;
                _collision = collision;
            }
        }
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (this.isSelected != true)
        {
            if (collision.gameObject.tag == "item")
            {
                this.GetComponent<Image>().color = new Color(0, 0, 0, 1);
                currentUsed = true;
                _collision = collision;
            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (this.isSelected != true)
        {
            if (collision.gameObject.tag == "item")
            {
                this.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                currentUsed = false;
                _collision = null;
            }
        }
    }


}
