using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class truckInGarage : MonoBehaviour {
    public int id;
    TruckManage tManage;
	// Use this for initialization
	void Start () {
        tManage = GameObject.Find("Manage").GetComponent<TruckManage>();
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(()=>tManage.changeTheTruck(this));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
