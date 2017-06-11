﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Card : MonoBehaviour {
    public int ID;
    public int timeCast;
    public int profit;
    
    public Text destination;
    public Item _item;
    public Button _cancel;

    CardsManage cManage;
    Distribution dManage;
    public void Destroy()
    {        
        DestroyImmediate(this.gameObject);
    }
    
    // Use this for initialization
    void Start () {
        _cancel = this.transform.Find("cancel").GetComponent<Button>();
        cManage = GameObject.Find("Manage").GetComponent<CardsManage>();
        dManage = GameObject.Find("Manage").GetComponent<Distribution>();

        _cancel.onClick.AddListener(() => cManage.cancelTheCard(this));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
