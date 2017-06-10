using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Distribution : MonoBehaviour {
    public Transform distributionPanel;
    public Transform destPanel;
    public GameObject destination;

    public List<GameObject> destList = new List<GameObject>();

    public int profit;

    TruckManage tManage;
    OrderManage oManage;
    public Image truck;
    int consume;
    int timeCast;
    
    public void distribution(int truckNum)//配送界面
    {
        Truck _truck = tManage.trucksList[truckNum];//获取车辆

        oManage.SendOrderToTruck(_truck);
        oManage.OrdersList.Clear();

        if (_truck.orderNum == 0) //检测车辆是否已经装配了物品
        {
            _truck.state = "empty";
        }
        else {
            _truck.state = "dist";
        }      
               
        displaySpot(_truck);
    }

    void displaySpot(Truck _truck)
    {
        for (int i = 0; i < _truck.orderNum; i++)
        {
            GameObject dest = Instantiate(destination, destPanel);

            if (i < 1)
            {
                float Posx = (float)_truck.timeCast[i] / (float)_truck.remain * 1600;
                dest.GetComponent<RectTransform>().anchoredPosition = new Vector2(Posx, 400 * _truck.ID -200);
            }
            else {
                float a = 0;
                for (int j = 0; j < destList.Count; j++)
                {
                    a = a + _truck.timeCast[j];
                }
                float Posx = ((float)_truck.timeCast[i] + (float)a) / (float)_truck.remain * 1600;

                dest.GetComponent<RectTransform>().anchoredPosition = new Vector2(Posx, 400 * _truck.ID - 200);
            }

            dest.transform.localScale = new Vector3(1,1,1);
            destList.Add(dest);
        }
    }

    public void ClearDest()
    {
        for (int i = 0; i < destList.Count; i++)
        {
            DestroyImmediate(destList[i].gameObject);
        }
        destList.Clear();
        
    }

    public void NextRound()
    {
        
        for (int i = 0; i < tManage.trucksList.Count; i++)
        {
            int finishedOrder = 0;///已完成订单数
            if (tManage.trucksList[i].state == "dist")
            {
                
                for (int j = 0; j < tManage.trucksList[i].timeCast.Count; j++) ////开始对第一个订单倒计时
                {
                    if (tManage.trucksList[i].timeCast[j] != 0) ///如果为0，则开始第二个
                    {
                        tManage.trucksList[i].timeCast[j]--;
                        break;
                    }
                    else
                    {
                        finishedOrder++;
                        ProfitEachDest(tManage.trucksList[i],j);///收益函数？
                        continue;
                    }
                }
            }
            if (finishedOrder == tManage.trucksList[i].orderNum)
            {
                ///执行回总站函数；
                //////收益函数？
                //花销函数？
                tManage.trucksList[i].state = "empty";
            }
            else {
                TruckMove(tManage.trucksList[i]);
            }
        }
    }

    void ProfitEachDest(Truck _truck,int index)
    {
        profit = profit + _truck.profit[index];
    }

    void TruckMove(Truck _truck)
    {
        if (_truck.state == "dist")
        {
            float unitShift = 0;
            unitShift = 1600.0f / (float)_truck.remain;
            float xPos = _truck.transform.localPosition.x + unitShift;
            float yPos = _truck.transform.localPosition.y;
            _truck.transform.localPosition = new Vector3(xPos, yPos, 0);
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
