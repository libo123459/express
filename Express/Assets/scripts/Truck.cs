using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour {
    public List<Order> _ordersList = new List<Order>();
    public List<string> _destination = new List<string>();
    public int timeCast;
    public int consume;
    public int row;
    public int column;
    public int ID;

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
