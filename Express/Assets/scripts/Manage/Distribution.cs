using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Distribution : MonoBehaviour {
    public Transform distributionPanel;
    public Transform destPanel;
    public GameObject destination;
    public GameObject ProfitPanel;
    public GameObject notEnough;
    public BtnStation Station;
    public Button nextRound;

    public Text _profit;
    public Text text_credit;
    public Text text_profit;
    public Text finishedOrder;

    public List<GameObject> destPanelList = new List<GameObject>();

    public static float profit;
    public static float credit;
    public static float totalCredit;
    public static float totalProfit;

    public static int MaxCredit = 10;
    public int dice;
    public int dice_coe;
    public int diceMax;
    public int diceMin;
    public string diceState = "normal";

    CardsData cData;
	EventData eData;
    OrderManage oManage;
    DriverManage dManage;
	EventManage eManage;
	CardsManage cManage;
    
    int consume;
    int timeCast;
    public static int level = 1;
    int finished = 0;

    void levelUp(Truck _truck)
    {
        finished += _truck.orderNum;
        if (finished >= 10)
        {
            if (level == 1)
            {
                level += 1;
                TruckManage.trucksList[1].active = true;
                TruckManage.trucksList[1].gameObject.SetActive(true);
                destPanelList[1].SetActive(true);
            }
        }
        if (finished >= 30)
        {
            if (level == 2)
            {
                level += 1;
                TruckManage.trucksList[2].active = true;
                TruckManage.trucksList[2].gameObject.SetActive(true);
                destPanelList[2].SetActive(true);
            }
        }
        finishedOrder.text = "完成订单" + finished.ToString();
    }

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
            _truck.state = "start";
        }    
        
        displaySpot(_truck);
        cManage.CardSkill(_truck);
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
        destPanelList[1].SetActive(false);
        destPanelList[2].SetActive(false);
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

    void GainTheDice()
    {
        if (diceState == "normal")
        {
            if (level == 1)
            {
                diceMax = 5;
                diceMin = 3;
            }
            if (level == 2)
            {
                diceMax = 5;
                diceMin = 2;
            }
            if (level == 3)
            {
                diceMax = 5;
                diceMin = 1;
            }
        }
        if (diceState == "Max")
        {
            diceMax = 5;
            diceMin = diceMin - 1;
        }
        if (diceState == "Min")
        {
            diceMin = 4 - level;
            diceMax = diceMin + 1;
        }
    }

    public void NextRound()
    {
        if (cData.CardsList.Count < 6)
        {
            if (level <= 6 - cData.CardsList.Count)
            {
                
                for (int i = 0; i < TruckManage.trucksList.Count; i++)
                {
                    if (TruckManage.trucksList[i].state == "dist" || TruckManage.trucksList[i].state == "start")
                    {
                        Truck _truck = TruckManage.trucksList[i];
                        _truck.state = "dist";
                        TruckMove(_truck, dice);///下一回合数随机
                        if (_truck.remain == 0)
                        {
                            _truck.state = "finished";                           
                            ProfitAtLast(_truck);
                        }
                    }
                }
                CountDown();
                GainTheDice();                
                dice = Random.Range(diceMin, diceMax) + dice_coe;
                cManage.AddTheCard(level);
                cManage.CardPorpty();
            }
            else {
                int n = 6 - cData.CardsList.Count;
                notEnough.SetActive(true);
                notEnough.transform.GetChild(0).GetComponent<Text>().text = "卡池剩余位置不足"
                    + "\n" + "是否消耗 " + ((level - n) * 2).ToString() +" 点信誉来进行下一回合";
            }            
        }        
    }

    public void DeductCre()///剩余位置不够抽卡时,扣信誉
    {
        int n = 6 - cData.CardsList.Count;

        totalCredit -= (level - n)* 2;

        for (int i = 0; i < TruckManage.trucksList.Count; i++)
        {
            if (TruckManage.trucksList[i].state == "dist")
            {
                Truck _truck = TruckManage.trucksList[i];


                TruckMove(_truck, dice);///下一回合数随机
                if (_truck.remain == 0)
                {
                    _truck.state = "finished";                   
                    ProfitAtLast(_truck);
                }
            }
        }
        CountDown();
        GainTheDice();
        cManage.AddTheCard(n);
        cManage.CardPorpty();
        CloseNotEnough();
    }

    public void CloseNotEnough()
    {
        notEnough.SetActive(false);
    }

	public void ProfitAtLast(Truck _truck)
    {
        ProfitPanel.SetActive(true);

        int n = _truck.orderNum;
        profit = 2 * n - 1;
        credit = 1;
        cManage.CardSkill(_truck);
        if (credit + totalCredit > MaxCredit)
        {
            totalCredit = MaxCredit;
        }
        else
        {
            totalCredit = totalCredit + credit;
        }
        totalProfit = totalProfit + profit;

        _profit.text = "收益金额：" + profit.ToString()
            + "\n" + "信誉度：" + credit.ToString();
       // cManage.CardSkill(_truck);
        text_credit.text = "信誉" + totalCredit.ToString();
        text_profit.text = "金币" + totalProfit.ToString();

        levelUp(_truck);
    }

    void TruckMove(Truck _truck,int _dice)
    {
        float unitShift = 0;
        unitShift = 1300.0f / (float)_truck.TotalTimecast;
        
        if (_truck.stopTime >= _dice)
        {
            _truck.stopTime -= _dice;
        }
        else {
            int n = _dice - _truck.stopTime;
            if (_truck.remain >= n)
            {
                _truck.remain -= n;
                float xPos = _truck.transform.localPosition.x + unitShift * n;
                float yPos = _truck.transform.localPosition.y;
                _truck.transform.localPosition = new Vector3(xPos, yPos, 0);
            }
            else
            {                
                float xPos = _truck.transform.localPosition.x + unitShift * _truck.remain; ;
                float yPos = _truck.transform.localPosition.y;
                _truck.transform.localPosition = new Vector3(xPos, yPos, 0);
                _truck.remain = 0;
            }
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
                _truck.skillList.Clear();
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
        consume = _truck.consume * _truck.TotalTimecast;// + _truck.driver.salary;///工资加油耗
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
						eCard.TimeCast.text = eCard.name + "CountDown" + eCard.CountDown.ToString();
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
        diceState = "normal";
        if (dice == 0)
        {
           dice = Random.Range(3,5);
        }
        oManage = GameObject.Find("Manage").GetComponent<OrderManage>();
        cData = GameObject.Find("Manage").GetComponent<CardsData>();
       // dManage = GameObject.Find("Manage").GetComponent<DriverManage>();
		cManage = GameObject.Find("Manage").GetComponent<CardsManage>();
		eManage = GameObject.Find("Manage").GetComponent<EventManage>();

        displayDestPanel();

        totalCredit = 10;//MaxCredit;///初始信誉
        text_credit.text = "信誉" + totalCredit.ToString();
        text_profit.text = "金币" + totalProfit.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        nextRound.transform.GetChild(0).GetComponent<Text>().text = dice.ToString();
    }
}
