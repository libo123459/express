using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Distribution : MonoBehaviour {
    public Transform distributionPanel;
    TruckManage tManage;
    OrderManage oManage;
    public Image truck;
    int consume;
    int timeCast;


    public void distribution(int truckNum)
    {
        Truck _truck = tManage.trucksList[truckNum];
        
        Image _tImage = Instantiate(truck);
        _tImage.transform.SetParent(distributionPanel);
        _tImage.transform.localPosition = new Vector3(-600,0,0);
        _tImage.transform.localScale = new Vector3(1,1,1);
        
        for (int i = 0; i < oManage.OrdersList.Count; i++)///////把已装配的订单数据传输到当前truck上
        {
            _truck._ordersList.Add(oManage.OrdersList[i]);
            _truck.timeCast = _truck.timeCast + oManage.OrdersList[i]._timecast;
            _truck.consume = _truck.consume + oManage.OrdersList[i]._consume;
        }
    }  
    

    // Use this for initialization
    void Start () {
        tManage = GameObject.Find("Manage").GetComponent<TruckManage>();
        oManage = GameObject.Find("Manage").GetComponent<OrderManage>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
