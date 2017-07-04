using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Truck : MonoBehaviour {
    
    public List<string> _destination = new List<string>();
    public List<int> timeCast = new List<int>();
   
    public List<int> profit = new List<int>();
    public List<int> credit = new List<int>();

    public int consume;
    public int row;
    public int column;
    public int ID;
    public int price;
    public int orderNum;
    public int remain;
    public int stopTime;
    public int TotalTimecast;
    public string state;

    public int driverID;

    public Vector3 StartPos;

    public Text text;
    public void ClearAll()
    {
        _destination.Clear();
        timeCast.Clear();
        
        remain = 0;
    }
	// Use this for initialization
	void Start () {
        StartPos = this.transform.position;
        text = this.transform.GetChild(0).GetComponent<Text>();
        
	}
	
	// Update is called once per frame
	void Update () {
        text.text = (remain + stopTime).ToString();
    }
}
