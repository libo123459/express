using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Distribution : MonoBehaviour {
    public Transform distributionPanel;
    public Transform destPanel;
    public GameObject destination;
    public GameObject ProfitPanel;
    public BtnStation Station;

    public Text _profit;
    public Text text_credit;
    public Text text_profit;

    public List<GameObject> destPanelList = new List<GameObject>();

    public static float profit;
    public static float credit;
    public static float totalCredit;
    public static float totalProfit;

    public int MaxCredit = 100;

    CardsData cData;
	EventData eData;
    OrderManage oManage;
    DriverManage dManage;
	EventManage eManage;
	CardsManage cManage;
    
    int consume;
    int timeCast;    
    
    public void distribution(int truckNum)//配送界面
    {
        Truck _truck = TruckManage.trucksList[truckNum];//获取车辆
        oManage.SendOrderToTruck(_truck);
        oManage.OrdersList.Clear();

        if (_truck.orderNum == 0) //检测车辆是否已经装配了物品
        {
            _truck.state = "empty";
        }
        else {
            _truck.state = "dist";
        }
        if (_truck.driver != null)
        {
            DriverManage.DriverSkill(_truck.driver);
        }
        
        displaySpot(_truck);
    }

    void displayDestPanel() //目的地panel的实例化
    {
        for (int i = 0; i < TruckManage.trucksList.Count; i++)
        {
            GameObject dPanel = Instantiate(destPanel.gameObject, distributionPanel);
            BtnStation _station = Instantiate(Station, dPanel.transform);
            dPanel.transform.localPosition = new Vector3(-130, 200 * i, 0);
            _station.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,0,0);
            _station.truckNum = i;
            destPanelList.Add(dPanel);
        }
    }

    void displaySpot(Truck _truck)
    {
        for (int i = 0; i < _truck.orderNum; i++)
        {
            GameObject dest = Instantiate(destination, destPanelList[_truck.transform.GetSiblingIndex()].transform);

            if (i < 1)
            {
                float Posx = ((float)_truck.timeCast[i] / (float)_truck.TotalTimecast) * 1300;
                dest.GetComponent<RectTransform>().anchoredPosition = new Vector2(Posx,0);
                
            }
            else {
                float a = 0;
                for (int j = 0; j <= i; j++)
                {
                    a = a + _truck.timeCast[j];
                }
                float Posx = (a / (float)_truck.TotalTimecast) * 1300;

                dest.GetComponent<RectTransform>().anchoredPosition = new Vector2(Posx,0);
            }
            dest.transform.localScale = new Vector3(1,1,1);           
        }
    }

    public void ClearDest(int truckNum)
    {
        int num = destPanelList[truckNum].transform.childCount;
        for (int i = 1; i < num; i++)
        {
            
            DestroyImmediate(destPanelList[truckNum].transform.GetChild(1).gameObject);
        }       
    }

    public void NextRound()
    {
        if (cData.CardsList.Count < 6)///6为临时
        {
			
			for (int i = 0; i < TruckManage.trucksList.Count; i++)
            {
                if (TruckManage.trucksList[i].state == "dist")
                {
                    Truck _truck = TruckManage.trucksList[i];
                    if (_truck.ID == 10)
                    {
                        TruckManage.TruckSkill(_truck);
                    }
                    else {
                        TruckMove(_truck);
                    }
                    
                    for (int j = 0; j < _truck.timeCast.Count; j++) ////开始对第一个订单倒计时
                    {
                        if (_truck.remain == 0)
                        {
                            _truck.state = "finished";
                            
                            CreditLast(_truck);
                            ProfitAtLast(_truck);
                            TruckConsume(_truck);
                            break;
                        }
                        else {
                            if (_truck.timeCast[j] != 0) ///如果为0，则开始第二个
                            {
                                _truck.timeCast[j]--;

                                break;
                            }
                        }                                              
                    }                    
                }                
            }
			CountDown();
        }        
    }

	public void ProfitAtLast(Truck _truck)
    {
        ProfitPanel.SetActive(true);

        TruckConsume(_truck);

        for (int i = 0;i<_truck.profit.Count;i++)
		{
			profit = profit + _truck.profit[i];
		}
		
        _profit.text = "收益金额：" + profit.ToString() 
            +"\n" + "汽油消耗" + consume.ToString()
            + "\n" + "总计收益" + (profit - consume).ToString()
            + "\n" + "信誉度：" + credit.ToString();

        totalProfit = totalProfit + profit - consume;

        text_profit.text = "金币" + totalProfit.ToString();
    }

    public void CreditLast(Truck _truck)
    {
        for (int i = 0; i < _truck.orderNum; i++)
        {
            credit += _truck.credit[i];
        }
        if (_truck.orderNum >= 3)
        {
            credit += 1;
        }

        if (credit + totalCredit > MaxCredit)
        {
            totalCredit = MaxCredit;
        }
        else {
            totalCredit = totalCredit + credit;
        }
        text_credit.text = "信誉" + totalCredit.ToString();
        
       // print("加成" + n.ToString());
        
    }

    void TruckMove(Truck _truck)
    {
        if (_truck.stopTime == 0)
        {
			_truck.remain--;
			float unitShift = 0;
            unitShift = 1300.0f / (float)_truck.TotalTimecast;
            float xPos = _truck.transform.localPosition.x + unitShift;
            float yPos = _truck.transform.localPosition.y;
            _truck.transform.localPosition = new Vector3(xPos, yPos, 0);
        }
        else {
            _truck.stopTime--;
        }
    }

    public void TruckMoveToStation()
    {
        for (int i = 0; i < TruckManage.trucksList.Count; i++)
        {
            Truck _truck = TruckManage.trucksList[i];
            if (_truck.state == "finished")
            {
                ClearDest(_truck.transform.GetSiblingIndex());
                
                _truck.transform.position = _truck.StartPos;
                _truck.state = "empty";
                _truck.profit.Clear();
                _truck.credit.Clear();
                _truck.stopTime = 0;
            }
        }

        _profit.text = null;
        profit = 0;
        credit = 0;
        ProfitPanel.SetActive(false);
    }
    
    void TruckConsume (Truck _truck)
    {
        consume = _truck.TotalTimecast * _truck.consume;        
    }

	void CountDown()
	{
		for(int i = 0;i < cData.CardsList.Count;i++)
		{
			if(cData.CardsList[i].GetComponent<Card_event>()!=null)
			{
				Card_event eCard = cData.CardsList[i].GetComponent<Card_event>();
				if(eCard.type == "delayed1" || eCard.type == "delayed2")
				{
					if(eCard.CountDown >0)
					{
						eCard.CountDown--;
						eCard.destination.text = eCard.name + "CountDown" + eCard.CountDown.ToString();
					} else {
						if(eCard.type == "delayed1")
						{
							eManage.undoEvent01(eCard.eventID);
						} else {
                            
                            eManage.CheckEvent03(eCard.eventID);
						}

						cManage.DestoryTheCard(eCard);
					}
				}
			}
		}
	}


    // Use this for initialization
    void Start () {
        
        oManage = GameObject.Find("Manage").GetComponent<OrderManage>();
        cData = GameObject.Find("Manage").GetComponent<CardsData>();
        dManage = GameObject.Find("Manage").GetComponent<DriverManage>();
		cManage = GameObject.Find("Manage").GetComponent<CardsManage>();
		eManage = GameObject.Find("Manage").GetComponent<EventManage>();

        displayDestPanel();

        totalCredit = 100;//MaxCredit;///初始信誉
        text_credit.text = "信誉" + totalCredit.ToString();
        text_profit.text = "金币" + totalProfit.ToString();
    }
	
	// Update is called once per frame
	void Update () {
    }
}
