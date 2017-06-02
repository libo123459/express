using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manage : MonoBehaviour {
    public Draggable _draggabel;
	// Use this for initialization
	void Start () {
        _draggabel._slotData = this.GetComponent<SlotData>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
