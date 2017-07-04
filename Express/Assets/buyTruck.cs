using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buyTruck : MonoBehaviour {
    TruckManage tManage;
	// Use this for initialization
	void Start () {
        tManage = GameObject.Find("Manage").GetComponent<TruckManage>();
        
        Button btn = this.transform.GetChild(1).GetComponent<Button>();
        btn.onClick.AddListener(()=>tManage.buyTheTruck(this.gameObject));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
