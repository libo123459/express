﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour {
    
    public List<string> _destination = new List<string>();
    public List<int> timeCast = new List<int>();
    public List<int> consume = new List<int>();
    public int row;
    public int column;
    public int ID;
    public int orderNum;


    public void ClearAll()
    {
        _destination.Clear();
        timeCast.Clear();
        consume.Clear();
    }
	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
