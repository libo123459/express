using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverData : MonoBehaviour {
    
    public List<Driver> driverList = new List<Driver>();
    public List<string> nameList = new List<string>();
    private string[] names = new string[] {"Tom","John","Smith","Peter","Bob","Alex","Obama","Lee" };
	// Use this for initialization
	void Awake () {
        for (int i = 0; i < names.Length; i++)
        {
            nameList.Add(names[i]);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
