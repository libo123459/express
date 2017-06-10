using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckManage : MonoBehaviour {
    public Truck _truck;
    public List<Truck> trucksList = new List<Truck>();
    public Transform truckPanel;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < 2; i++)/////////临时
        {
            Truck mytruck = Instantiate(_truck);
            mytruck.transform.SetParent(truckPanel);
            mytruck.transform.localPosition = new Vector3(0,0,0);
            mytruck.transform.localScale = new Vector3(1,1,1);

            mytruck.row = 4;
            mytruck.column = 5;
            mytruck.ID = i;
            trucksList.Add(mytruck);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
