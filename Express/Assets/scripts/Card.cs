using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Card : MonoBehaviour {
    public int ID;
    public int timeCast;
    public int profit;
    
    public Text destination;
    public Item _item;

    public void Destroy()
    {
        
        DestroyImmediate(this.gameObject);
    }
    // Use this for initialization
    void Start () {        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
