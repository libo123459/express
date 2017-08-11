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
    public int diceNext;
    public int diceMaxNext;
    public int diceMinNext;
    public int diceMax;
    public int diceMin;
    public string diceState = "normal";
    
    CardsData cData;	
    OrderManage oManage;
    TruckManage tManage;    
	CardsManage cManage;
    
    int timeCast;
    public static int coe_timeCast_Team1;
    public static int level = 1;
    public static int finished = 0;
    public static int stage = 1;
    
    
    public void distribution(int truckNum)//配送界面
    {
        Truck _truck = TruckManage.trucksList[truckNum];//获取车辆
        oManage.SendOrderToTruck(_truck);
        oManage.OrdersList.Clear();
        _truck.TotalTimecast += coe_timeCast_Team1;
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

    string GetDiceState()
    {
        int n = 0;
        for (int i = 0; i < TruckManage.trucksList.Count; i++)
        {
            Truck _truck = TruckManage.trucksList[i];
            for (int j = 0; j < _truck.skillList.Count; j++)
            {
                if (_truck.skillList[j] != 8 && _truck.skillList[j] != 9)
                {
                    continue;
                }
                else {
                    n++;
                }
            }
        }
        if (n == 0)
        {
            return "normal";
        }
        else {
            return "unnormal";
        }
    }

    void GainTheDice()
    {
        diceState = GetDiceState();
       
        switch (diceState)
        {
            case "normal":
                if (diceNext == 0)
                {
                    diceMax = TruckData.GetDiceMax(1, stage) + 1;
                    diceMin = TruckData.GetDiceMin(1, stage);
                    dice = Random.Range(diceMin, diceMax);
                }
                else
                {
                    dice = diceNext;
                }

                if (TruckManage.teamID == 1)
                {
                    diceMaxNext = TruckData.GetDiceMax(1, stage) + 1;
                    diceMinNext = TruckData.GetDiceMin(1, stage);
                    diceNext = Random.Range(diceMinNext, diceMaxNext);
                }
                break;
            case "Max":
                if (diceNext == 0)
                {
                    dice = diceMax - 1;
                }
                else
                {
                    dice = diceNext;
                }
                if (TruckManage.teamID == 1)
                {
                    diceNext = diceMax - 1;
                }
                break;
            case "Min":
                if (diceNext == 0)
                {
                    dice = diceMin;
                }
                else
                {
                    dice = diceNext;
                }
                if (TruckManage.teamID == 1)
                {
                    diceNext = diceMin;
                }
                break;
        }      

        nextRound.transform.GetChild(0).GetComponent<Text>().text = dice.ToString() + "(" + diceNext.ToString() + ")";
    }

    public void NextRound()
    {
        if (cData.CardsList.Count < 6)
        {
            if (stage <= 6 - cData.CardsList.Count)
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
                            cManage.Card14(_truck);
                            ProfitAtLast(_truck);
                        }
                    }
                }
                GainTheDice();                
                cManage.AddTheCard(stage);
                cManage.CardPorpty();
            }
            else {
                int n = 6 - cData.CardsList.Count;
                notEnough.SetActive(true);
                notEnough.transform.GetChild(0).GetComponent<Text>().text = "卡池剩余位置不足"
                    + "\n" + "是否消耗 " + ((stage - n) * 2).ToString() +" 点信誉来进行下一回合";
            }            
        }        
    }

    public void DeductCre()///剩余位置不够抽卡时,扣信誉
    {
        int n = 6 - cData.CardsList.Count;

        totalCredit -= (stage - n)* 2;

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
        text_credit.text = "信誉" + totalCredit.ToString();
        text_profit.text = "金币" + totalProfit.ToString();
        tManage.TeamSkill(1);
        //finishedOrder.text = "完成订单" + finished.ToString();
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

	/*void CountDown()
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
	}*/


    // Use this for initialization
    void Start () {
        diceState = "normal";
        GainTheDice();
        oManage = GameObject.Find("Manage").GetComponent<OrderManage>();
        cData = GameObject.Find("Manage").GetComponent<CardsData>();
		cManage = GameObject.Find("Manage").GetComponent<CardsManage>();
        tManage = GameObject.Find("Manage").GetComponent<TruckManage>();
        displayDestPanel();

        totalCredit = MaxCredit;///初始信誉
        text_credit.text = "信誉" + totalCredit.ToString();
        text_profit.text = "金币" + totalProfit.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
