using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Distribution : MonoBehaviour {
    public Transform distributionPanel;
    public Transform destPanel;
    public GameObject destination;

    public List<GameObject> destList = new List<GameObject>();
    TruckManage tManage;
    OrderManage oManage;
    public Image truck;
    int consume;
    int timeCast;
    
    public void distribution(int truckNum)//装配界面
    {
        Truck _truck = tManage.trucksList[truckNum];//获取车辆
        _truck.orderNum = oManage.OrdersList.Count;

        Image _tImage = Instantiate(truck);//显示车的图片
        _tImage.transform.SetParent(distributionPanel);
        _tImage.transform.localPosition = new Vector3(-600, 200, 0);
        _tImage.transform.localScale = new Vector3(1, 1, 1);

        for (int i = 0; i < oManage.OrdersList.Count; i++)///////把已装配的订单数据传输到当前truck上
        {
            _truck.timeCast.Add(oManage.OrdersList[i]._timecast);
            _truck.consume.Add(oManage.OrdersList[i]._consume);
            DestroyImmediate(oManage.OrdersList[i].gameObject);// 清除任务栏上的任务
        }
        
        displaySpot(truckNum);
        oManage.OrdersList.Clear();        
    }
    void displaySpot(int truckNum)
    {
        Truck _truck = tManage.trucksList[truckNum];//获取车辆
        for (int i = 0; i < _truck.orderNum; i++)
        {
            GameObject dest = Instantiate(destination, destPanel);
            if (i < 1)
            {
                dest.GetComponent<RectTransform>().anchoredPosition = new Vector2(100 + 100 * _truck.timeCast[i], 0);
                //transform.localPosition = new Vector3(100 + 150 * _truck._ordersList[i]._timecast, 0, 0);
                
            }
            else {
                float Posx = destList[i - 1].GetComponent<RectTransform>().anchoredPosition.x;
                dest.GetComponent<RectTransform>().anchoredPosition = new Vector2(Posx + 150 * _truck.timeCast[i], 0);
            }
            
            dest.transform.localScale = new Vector3(1,1,1);
            destList.Add(dest);
            print("111");
        }
    }

    public void ClearDist()
    {
        for (int i = 0; i < destList.Count; i++)
        {
            DestroyImmediate(destList[i].gameObject);
        }
        destList.Clear();
        DestroyImmediate(distributionPanel.transform.GetChild(0).gameObject);
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
