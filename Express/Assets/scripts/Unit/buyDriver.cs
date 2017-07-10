using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buyDriver : MonoBehaviour {
    DriverManage dManage;
    public int id;
	// Use this for initialization
	void Start () {
        dManage = GameObject.Find("Manage").GetComponent<DriverManage>();

        Button btn = this.transform.GetChild(1).GetComponent<Button>();
        btn.onClick.AddListener(() => dManage.buyTheDriver(this.gameObject));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
