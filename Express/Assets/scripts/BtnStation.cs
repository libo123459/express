using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnStation : MonoBehaviour {
    public int truckNum;
    public Manage manage;
    // Use this for initialization

    public void onclick()
    {
        manage.Assemble(this.truckNum);
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
}
