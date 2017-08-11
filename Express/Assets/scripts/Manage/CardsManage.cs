using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsManage : MonoBehaviour {
    public Card _card;
    public Card_event _eCard;
    
    public Transform grid;

    public GameObject eventPanel;
    public GameObject gameoverPanel;
    public Text _event;
    public Text _useMoney;
    public Text remainCardText;
    public Transform NormalPool;
    public Transform EventPool;

	public int punish = 2;

    public List<Card> normalList = new List<Card>();

    public List<Card> dropList = new List<Card>();

    CardsData _cardData;
    EventData _eData;
    
    Distribution dManage;
    OrderManage oManage;

    int RemainCard = 60;
    int coe_die = 1;

	// Use this for initialization
	void Start ()
    {        
        _cardData = this.GetComponent<CardsData>();  
        dManage = this.GetComponent<Distribution>();
        oManage = this.GetComponent<OrderManage>();
        InitiCardPool();
        RefreshTask();
        RemainCard = normalList.Count;
	}
    void InitiCardPool()
    {
        int stageNum = _cardData.GetStageID(_cardData.column.Count - 1);///列表最后一个card的阶段数，总阶段数
        List<List<Card>> _totalList = new List<List<Card>>();
        for (int i = 1; i <= stageNum; i++)///每个阶段一个LIST，放到一个总LIST中
        {
            List<Card> _cards = new List<Card>();
            _totalList.Add(_cards);
        }
        for (int j = 1; j < _cardData.column.Count; j++)
        {
            Card mycard = Instantiate(_card, NormalPool);
            mycard.TimeCast = mycard.transform.GetChild(0).GetComponent<Text>();
            mycard.Description = mycard.transform.Find("description").GetComponent<Text>();
            mycard.ID = j;
            mycard.skillID = _cardData.GetSkillID(j);
            
            mycard.timeCast = _cardData.GetTimeCast(j);
            mycard._item = _cardData.GetItem(j);
            mycard.stageID = _cardData.GetStageID(j);
            if (mycard.skillID == 13)
            {
                mycard.timeCast = 10;
            }
            if (mycard.skillID == 14)
            {
                mycard.timeCast = 60;
            }
            mycard.TimeCast.text = "Time. " + mycard.timeCast + "ID" + mycard.ID;
            mycard.Description.text = _cardData.GetSkillDes(mycard.skillID);

            int blockNum = mycard._item.transform.childCount;

            Item myitem = Instantiate(mycard._item);
            myitem.transform.SetParent(mycard.transform);
            myitem.transform.localPosition = -myitem.CenterPos * 0.5f;
            myitem.transform.localScale = new Vector3(1, 1, 1) * 0.5f;

            _totalList[mycard.stageID - 1].Add(mycard);
        }
        
        for (int i = 0; i < _totalList.Count; i++)
        {
            int count = _totalList[i].Count;
            for (int j = 0; j < count; j++)
            {                
                int n = Random.Range(0, _totalList[i].Count);
                normalList.Add(_totalList[i][n]);
                _totalList[i].RemoveAt(n);
            }
        }
    }

    void moveToNormalPool(Card _card)
    {
        _card.transform.SetParent(NormalPool);
        normalList.Insert(Random.Range(0,normalList.Count),_card);
        _cardData.CardsList.Remove(_card);
    }

    void RefreshTask()
    {
        for (int i = 0; i < 6; i++)
        {
            Card_normal();
        }       

        RefreshID();
    }

    void RefreshID()
    {
        for (int i = 0; i < grid.transform.childCount; i++)         //刷新所有卡的序列号
        {
            grid.transform.GetChild(i).GetComponent<Card>().ID = i;
        }
    }

    public void CancelTheSendedCard()///将装配上的物件从卡池中消除
    {   
        for (int i = 0; i < oManage.OrdersList.Count; i++) //清除卡池中已装配的卡
        {
            int index = oManage.OrdersList[i].ID;
            _cardData.CardsList[index].Destroy();     
        }

        _cardData.CardsList.Clear();
        
        for (int i = 0; i < grid.transform.childCount; i++)//重新排列剩余卡
        {
            Card thecard = grid.transform.GetChild(i).GetComponent<Card>();
            thecard.ID = i;
            _cardData.CardsList.Add(thecard);
        }
    }

    public void AddTheCard(int level)   //抽卡
    {
        for (int i = 0; i < level; i++)
            {
                if (grid.childCount < 6) ////6为临时测试用，需要改成卡池上线的变量
                {

                    Card_normal();
                }
            }              

        RefreshID();
    }

    public void Card_normal()///普通卡
    {
        if (normalList.Count > 0)
        {
            
            Card _card = normalList[0];
            _card.transform.SetParent(grid);
            _cardData.CardsList.Add(_card);
            normalList.RemoveAt(0);            
        }        
    }

    /*void Card_event()//事件卡
    {
        eventPanel.SetActive(true);
        //int index = Random.Range(0, _eData.namelist.Count);
        int myindex = TheIndex();

		eManage.influnce(myindex);

        string name = _eData.namelist[myindex];

        _event.text = "事件：" + name;
    }*/

    public void confirmEvent()
    {
        eventPanel.SetActive(false);       
    }

    public void cancelTheCard(Card _card)///退订卡片
    {
        RemainCard++;

        if (Distribution.totalCredit + punish < 0)
        {
            GameOver();
        }
        else {
            Distribution.totalCredit = Distribution.totalCredit - punish;
            // Distribution.totalProfit -= _card.timeCast + 1 ;
            dManage.text_credit.text = "信誉" + Distribution.totalCredit.ToString();
            // dManage.text_profit.text = "金币" + Distribution.totalProfit.ToString();
        }
        moveToNormalPool(_card);        
    }    

    void GameOver()
    {        
        
    }

	public void DestoryTheCard(Card _card)
	{
        _card.Destroy();
        
		List<Card> _tmp = new List<Card>();
		for (int i = 0; i < _cardData.CardsList.Count; i++)
		{
			if (_cardData.CardsList[i] != null)
			{
				_tmp.Add(_cardData.CardsList[i]);
			}
		}
		_cardData.CardsList.Clear();
		for (int i = 0; i < _tmp.Count; i++)
		{
			_cardData.CardsList.Add(_tmp[i]);
			_cardData.CardsList[i].ID = i;
		}
	}

    /*int TheIndex()
	{
		List<int> sameNum = new List<int>();
		List<int> diffNum = new List<int>();
		List<int> namelist_copy = new List<int> ();
		for(int i = 0;i< 8 /*_eData.namelist.Count;i++)
		{
			namelist_copy.Add(i);
		}

		for(int i = 0;i<_cardData.CardsList.Count;i++)
		{
			if(_cardData.CardsList[i].GetComponent<Card_event>() != null)
			{
				sameNum.Add(_cardData.CardsList[i].GetComponent<Card_event>().eventID);
			}
		}

		for(int i = 0;i<sameNum.Count;i++)
		{
			for(int j = 0;j<namelist_copy.Count;j++)
			{
				if(namelist_copy[j] == sameNum[i])
				{
					namelist_copy.RemoveAt(j);
				}
			}
		}
		for(int i = 0;i<namelist_copy.Count;i++)
		{
			if(namelist_copy[i] != null)
			{
				diffNum.Add(namelist_copy[i]);
			}
		}
		int n = Random.Range(0,diffNum.Count);
		return diffNum[n];
	}*/
    public void CardSkill(Truck _truck)
    {
        for (int i = 0; i < _truck.skillList.Count; i++)
        {
            switch (_truck.skillList[i])
            {
                case 1:
                    skill_1(_truck);
                    break;
                case 2:
                    skill_2(_truck);
                    break;
                case 3:
                    skill_3(_truck);
                    break;
                case 4:
                    skill_4(_truck);
                    break;
                case 5:
                    skill_5(_truck);
                    break;
                case 6:
                    skill_6(_truck);
                    break;
                case 7:
                    skill_7(_truck);
                    break;
                case 8:
                    skill_8(_truck);
                    break;
                case 9:
                    skill_9(_truck);
                    break;
                case 10:
                    skill_10(_truck);
                    break;
                case 11:
                    skill_11(_truck);
                    break;

                case 15:
                    skill_15(_truck);
                    break;
                case 16:
                    skill_16(_truck);
                    break;

            }
        }        
    }
    void skill_1(Truck _truck)
    {
        if (_truck.state == "finished")
        {
            Distribution.credit += 1;
        }        
    }//额外增加1点信用
    void skill_2(Truck _truck)
    {
        if (_truck.state == "finished")
        {
            Distribution.credit = 0;
        }        
    }//不增加
    void skill_3(Truck _truck)
    {
        if (_truck.state == "start")
        {
            if (_truck.orderNum > 1)
            {
                _truck.stopTime += 1;
            }
        }
    }//与其他货物一起增加1回合
    void skill_4(Truck _truck)
    {
        if (_truck.state == "start")
        {
            if (_truck.orderNum > 1)
            {
                _truck.TotalTimecast -= 1;
                _truck.remain -= 1;
            }
        }
    }//减少1
    void skill_5(Truck _truck)
    {
        if (_truck.state == "start")
        {
            if (_truck.orderNum == 1)
            {
                _truck.stopTime += 1;
            }
        }
    }//单独增加1回合
    void skill_6(Truck _truck)
    {
        if (_truck.state == "start")
        {
            if (_truck.orderNum == 1)
            {
                _truck.TotalTimecast -= 1;
                _truck.remain -= 1;
            }
        }
    }//减少1
    void skill_7(Truck _truck)
    {
        if (_truck.state == "finished")
        {
            Distribution.credit -= 5;
            Distribution.profit += 10;
        }
    }//增加10点业务，减少5信用
    void skill_8(Truck _truck)
    {
        if (_truck.state == "dist" || _truck.state == "start" || _truck.state == "assemble")
        {
            dManage.diceState = "Max";            
        }
    }//运输期间DICE最大
    void skill_9(Truck _truck)
    {
        if (_truck.state == "dist" || _truck.state == "start" || _truck.state == "assemble")
        {
            dManage.diceState = "Min";
        }
    }//运输期间DICE最小
    void skill_10(Truck _truck)
    {
        if (_truck.state == "finished")
        {
            if (_truck.TotalTimecast > 5)
            {
                Distribution.credit -= 5;
            }
        }
    }//超过5回合，减少5信用
    void skill_11(Truck _truck)
    {
        if (_truck.state == "finished")
        {
            Distribution.profit += Distribution.profit;
        }
    }//业务翻倍
    
    void skill_15(Truck _truck)
    {
        if (_truck.state == "finished")
        {
            Distribution.credit = Distribution.MaxCredit - Distribution.totalCredit;
        }
    }//信用最大值
    void skill_16(Truck _truck)
    {
        if (_truck.state == "finished")
        {
            Distribution.credit = -Distribution.totalCredit + 1;
        }
    }//信用值1点

    public void CardPorpty()
    {
        for (int i = 0; i < _cardData.CardsList.Count; i++)
        {
            if(_cardData.CardsList[i].skillID >= 12 && _cardData.CardsList[i].skillID <= 14)
            {
                if (_cardData.CardsList[i].skillID == 12)
                {
                    if (_cardData.CardsList[i].count < 3)
                    {
                        _cardData.CardsList[i].timeCast += 1;
                        _cardData.CardsList[i].count += 1;
                    }                    
                }
                if (_cardData.CardsList[i].skillID == 13)
                {
                    if (_cardData.CardsList[i].timeCast > 1)
                    {
                        _cardData.CardsList[i].timeCast -= 1;
                    }                    
                }                
            }
            _cardData.CardsList[i].TimeCast.text = "Time." + _cardData.CardsList[i].timeCast.ToString();
        }
    }
    public void Card14(Truck _truck)
    {
        for (int i = 0; i < _cardData.CardsList.Count; i++)
        {
            if (_cardData.CardsList[i].skillID == 14)
            {
                if (_truck.state == "finished")
                {
                    _cardData.CardsList[i].timeCast -= _truck.orderNum;
                    
                }                
            }
        }        
    }
    
    // Update is called once per frame
    void Update () {
        remainCardText.text = "剩余卡牌" + normalList.Count.ToString();
	}

}
