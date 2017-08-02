﻿using System.Collections;
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
    ItemData _itemData;
    Distribution dManage;
    OrderManage oManage;

    int RemainCard = 60;
    int RemainNormalCard = 60;
    int RemainEventCard = 10;
    int coe_die = 1;

	// Use this for initialization
	void Start ()
    {        
        _cardData = this.GetComponent<CardsData>();        
        _itemData = this.GetComponent<ItemData>();
        dManage = this.GetComponent<Distribution>();
        oManage = this.GetComponent<OrderManage>();
        InitiCardPool();
        RefreshTask();
	}

    void InitiCardPool()
    {
        for (int i = 0; i < RemainNormalCard; i++)
        {
            Card mycard = Instantiate(_card,NormalPool);            

            int random = Random.Range(1, _cardData.Array.Length);
            mycard.destination = mycard.transform.GetChild(0).GetComponent<Text>();
            
            if (i < 14)
            {
                mycard.timeCast = 2;
            }
            if (i >= 14 && i < 29)
            {
                mycard.timeCast = 3;
            }
            if (i >= 29 && i < 44)
            {
                mycard.timeCast = 4;
            }
            if (i >= 44 && i < RemainNormalCard)
            {
                mycard.timeCast = 5;
            }
            mycard.ID = random;
            mycard.skillID = _cardData.GetSkillID(mycard.ID);
            mycard.profit = 5;///收益
            mycard.credit = 1; //信誉
            mycard.destination.text = "Time. " + mycard.timeCast;
            mycard.state = "inPool";
            GiveTheCardItem(mycard);
            normalList.Add(mycard);
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

                    SelectCardType();
                }
            }              

        RefreshID();
    }

    void SelectCardType()
    {
        int chance = Random.Range(0,101);
        if (chance < 51)
        {
			Card_normal();
        }
        else {
            //Card_event();///
            Card_normal();
        }
    }
    
    public void Card_normal()///普通卡
    {
        if (normalList.Count > 0)
        {
            int index = Random.Range(0, normalList.Count);
            Card _card = normalList[index];
            _card.transform.SetParent(grid);
            _cardData.CardsList.Add(_card);
            normalList.RemoveAt(index);            
        }        
    }

    void GiveTheCardItem(Card _card)
    {
        int random = Random.Range(1, 101);//从文件获取
        if (random < 25)
        {
            _card._item = _itemData.ItemsList[0];
        }
        if (random >= 25 && random < 50)
        {
            _card._item = _itemData.ItemsList[Random.Range(1,3)];
        }
        if (random >= 50 && random < 75)
        {
            _card._item = _itemData.ItemsList[Random.Range(3, 10)];
        }
        if (random >= 75 && random < 101)
        {
            _card._item = _itemData.ItemsList[Random.Range(10, _itemData.ItemsList.Count)];
        }
        
        int blockNum = _card._item.transform.childCount;
        _card._item.consume = 1;//油耗         
        //mycard.destination.text = _cardData.destinations[Random.Range(0, _cardData.destinations.Count)];

        Item myitem = Instantiate(_card._item);
        myitem.transform.SetParent(_card.transform);
        myitem.transform.localPosition = -myitem.CenterPos * 0.5f;
        myitem.transform.localScale = new Vector3(1, 1, 1) * 0.5f;
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

    void useMoney()
    {
        gameoverPanel.SetActive(false);
       
        float n = Distribution.totalProfit - (coe_die * 100);
        Distribution.totalProfit = n;
        
        dManage.text_profit.text = "金币" + Distribution.totalProfit.ToString();
        Button btn = gameoverPanel.transform.GetChild(0).GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        coe_die = coe_die + 1;
    }

    void GameOver()
    {
        gameoverPanel.SetActive(true);
        _useMoney.text = "使用" + (coe_die * 100).ToString() + "金币避免死亡";
        Button btn = gameoverPanel.transform.GetChild(0).GetComponent<Button>();
        btn.onClick.AddListener(useMoney);
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
                case 12:
                    skill_12(_truck);
                    break;
                case 13:
                    skill_13(_truck);
                    break;
                case 14:
                    skill_14(_truck);
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
        if (_truck.state == "dist")
        {
            if (_truck.orderNum > 1)
            {
                _truck.stopTime += 1;
            }
        }
    }//与其他货物一起增加1回合
    void skill_4(Truck _truck)
    {
        if (_truck.state == "dist")
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
        if (_truck.state == "dist")
        {
            if (_truck.orderNum == 1)
            {
                _truck.stopTime += 1;
            }
        }
    }//单独增加1回合
    void skill_6(Truck _truck)
    {
        if (_truck.state == "dist")
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

    }//运输期间DICE最大/最小（还没做）
    void skill_9(Truck _truck)
    {
        if (_truck.state == "finished")
        {
            if (_truck.orderNum > 5)
            {
                Distribution.credit -= 5;
            }
        }
    }//超过5回合，减少5信用
    void skill_10(Truck _truck)
    {
        if (_truck.state == "finished")
        {
            Distribution.profit *= 2;
        }
    }//业务翻倍
    void skill_11(Truck _truck)
    {

    }//
    void skill_12(Truck _truck)
    {

    }//
    void skill_13(Truck _truck)
    {

    }//
    void skill_14(Truck _truck)
    {
        if (_truck.state == "finished")
        {
            Distribution.totalCredit = Distribution.MaxCredit;
        }
    }//信用最大值
    void skill_15(Truck _truck)
    {
        if (_truck.state == "finished")
        {
            Distribution.totalCredit = 1;
        }
    }//信用值1点

    public void CardPorpty()
    {
        for (int i = 0; i < _cardData.CardsList.Count; i++)
        {
            if(_cardData.CardsList[i].skillID >= 11 && _cardData.CardsList[i].skillID <= 13)
            {
                if (_cardData.CardsList[i].skillID == 11)
                {
                    if (_cardData.CardsList[i].count < 3)
                    {
                        _cardData.CardsList[i].timeCast += 1;
                        _cardData.CardsList[i].count += 1;
                    }                    
                }
                if (_cardData.CardsList[i].skillID == 12)
                {
                    if (_cardData.CardsList[i].timeCast > 1)
                    {
                        _cardData.CardsList[i].timeCast -= 1;
                    }                    
                }
                if (_cardData.CardsList[i].skillID == 13)
                {
                    for (int j = 0; j < TruckManage.trucksList.Count; j++)
                    {
                        if (TruckManage.trucksList[i].state == "finished")
                        {
                            _cardData.CardsList[i].timeCast -= TruckManage.trucksList[i].orderNum;
                        }
                    }
                }
            }
        }
    }
    
    // Update is called once per frame
    void Update () {
        remainCardText.text = "剩余卡牌" + normalList.Count.ToString();
	}

}
