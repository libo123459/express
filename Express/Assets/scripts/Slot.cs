using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour {
    public bool isSelected;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "item")
        {
            this.GetComponent<Image>().color = new Color(0, 0, 0, 1);
            
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "item")
        {
            this.GetComponent<Image>().color = new Color(0, 0, 0, 1);
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "item")
        {
            this.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            
        }
            
    }


}
