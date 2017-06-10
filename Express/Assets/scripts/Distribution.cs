using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Distribution : MonoBehaviour {
    public Transform distributionPanel;
    public Transform destPanel;
    public GameObject destination;
    public GameObject ProfitPanel;

    public Text _profit;

    public List<GameObject> destList = new List<GameObject>();

    public int profit;

    CardsData cData;
    TruckManage tManage;
    OrderManage oManage;
    
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
        if (cData.CardsList.Count < 6)///6为临时
        {
            for (int i = 0; i < tManage.trucksList.Count; i++)
            {
                if (tManage.trucksList[i].state == "dist")
                {
                    Truck _truck = tManage.trucksList[i];
                    TruckMove(_truck);
                    
                    for (int j = 0; j < _truck.timeCast.Count; j++) ////开始对第一个订单倒计时
                    {
                        _truck.remain--;
                        if (_truck.timeCast[j] != 0) ///如果为0，则开始第二个
                        {
                            _truck.timeCast[j]--;                            
                            break;
                        }
                        else
                        {
                            
                            ProfitEachDest(_truck, j);///收益函数？
                            continue;
                        }                        
                    }
                    if (_truck.remain == 0)
                    {
                        _truck.state = "finished";
                        ProfitAtLast();
                    }
                }                
            }
        }
        
    }

    void ProfitEachDest(Truck _truck,int index)
    {
        profit = profit + _truck.profit[index];
    }

    void ProfitAtLast()
    {
        ProfitPanel.SetActive(true);
        _profit.text = "总共收益金额：" + profit.ToString();
    }

    void TruckMove(Truck _truck)
    {
        float unitShift = 0;
        unitShift = 1600.0f / (float)_truck.remain;
        float xPos = _truck.transform.localPosition.x + unitShift;
        float yPos = _truck.transform.localPosition.y;
        _truck.transform.localPosition = new Vector3(xPos, yPos, 0);
                
    }

    public void TruckMoveToStation()
    {
        for (int i = 0; i < tManage.trucksList.Count; i++)
        {
            Truck _truck = tManage.trucksList[i];
            if (tManage.trucksList[i].state == "finished")
            {
                float xPos = _truck.transform.localPosition.x;
                float yPos = _truck.transform.localPosition.y;
                _truck.transform.localPosition = new Vector3(xPos - 1600, yPos, 0);
                _truck.state = "empty";
            }
        }
        _profit.text = null;
        ProfitPanel.SetActive(false);
    }

    // Use this for initialization
    void Start () {
        tManage = GameObject.Find("Manage").GetComponent<TruckManage>();
        oManage = GameObject.Find("Manage").GetComponent<OrderManage>();
        cData = GameObject.Find("Manage").GetComponent<CardsData>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
