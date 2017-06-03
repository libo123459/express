using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Order : MonoBehaviour {
    public Text _destination;
    public Text _timeCast;
    public Text _consume;

	// Use this for initialization
	void Start ()
    {
        this._destination = transform.Find("destination").GetComponent<Text>();
        this._timeCast = transform.Find("time").GetComponent<Text>();
        this._consume = transform.Find("consume").GetComponent<Text>();
    }

    public void ShowDestination(string address)
    {
        _destination.text = address;
    }

    public void ShowTimeCast(int time)
    {
        _timeCast.text = "time:" + time.ToString();
    }

    public void ShowConsume(int consume)
    {
        _consume.text = "consume:" + consume.ToString();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
