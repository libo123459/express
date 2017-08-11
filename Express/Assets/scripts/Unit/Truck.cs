using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Truck : MonoBehaviour {
    
    public List<string> _destination = new List<string>();
    public List<int> timeCast = new List<int>();
    public List<int> skillList = new List<int>();
    public List<float> profit = new List<float>();
    public List<float> credit = new List<float>();

    public int consume;
    public int row;
    public int column;
    public int ID;///truck种类序列号
    public int price;
    public int skillID;

    public int orderNum;
    public int blockNum;
    public int remain;
    public int stopTime;
    public int TotalTimecast;
    public string state;
    public bool active = false;

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
