using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Driver : MonoBehaviour {
    public int coe_consume;
    public int coe_timecast;
    public int coe_profit;
    public string coe_property;

    public int id;
    public Text text;
    public new string name;

    private DriverManage dManage;
    private Button confirm;
    // Use this for initialization
    void Start () {
        text = this.transform.GetChild(0).GetComponent<Text>();
        text.text = name;
        dManage = GameObject.Find("Manage").GetComponent<DriverManage>();
        confirm = this.GetComponent<Button>();
        confirm.onClick.AddListener(()=>dManage.confirmTheDriver(this));
    }
    public void Confirm()
    {

    }
	// Update is called once per frame
	void Update () {
		
	}
}
