using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Order : MonoBehaviour {
    public Text destination;
    public Text timeCast;
    public Text consume;

    public int _timecast;
    public int _consume;
    public int ID;
    public int profit;
    public int credit;
    public int blockNum;
    public int skillID;

	// Use this for initialization
	void Start ()
    {
        
    }

    public void InitOrder()
    {
        this.destination = transform.Find("destination").GetComponent<Text>();
        this.timeCast = transform.Find("time").GetComponent<Text>();
        this.consume = transform.Find("consume").GetComponent<Text>();       
    }

    

    // Update is called once per frame
    void Update () {
		
	}
}
