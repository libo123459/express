using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnStation : MonoBehaviour {
    public int truckNum;
    public Manage manage;
    private TruckManage tManage;
    // Use this for initialization

    public void onclick()
    {
        manage.Assemble(this.truckNum);
    }
	void Start () {
        manage = GameObject.Find("Manage").GetComponent<Manage>();
        tManage = manage.GetComponent<TruckManage>();
	}
	
	// Update is called once per frame
	void Update () {
        if (tManage.trucksList[truckNum].state == "dist")
        {
            this.GetComponent<Button>().interactable = false;
        }
        else {
            this.GetComponent<Button>().interactable = true;
        }
		
	}
    
}
