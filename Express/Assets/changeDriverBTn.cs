using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeDriverBTn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (TruckManage.trucksList[AssembleManage.currentTruck].ID == 8)
        {
            this.GetComponent<Button>().interactable = false;
        }
        else {
            this.GetComponent<Button>().interactable = true;
        }
	}
}
