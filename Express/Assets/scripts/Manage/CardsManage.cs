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

    List<Card> normalList = new List<Card>();
    List<Card_event> eventList = new List<Card_event>();
    List<Card> dropList = new List<Card>();

    CardsData _cardData;
    EventData _eData;
    ItemData _itemData;
    Distribution dManage;
    OrderManage oManage;
    EventManage eManage;

    int RemainCard = 60;
    int RemainNormalCard = 50;
    int RemainEventCard = 10;
    int coe_die = 1;

	// Use this for initialization
	void Start ()
    {
        _cardData = this.GetComponent<CardsData>();
        _eData = this.GetComponent<EventData>();
        _itemData = this.GetComponent<ItemData>();
        dManage = this.GetComponent<Distribution>();
        oManage = this.GetComponent<OrderManage>();
        eManage = this.GetComponent<EventManage>();
        RefreshTask();
	}

    void InitiCardPool()
    {
        for (int i = 0; i < RemainNormalCard; i++)
        {
            Card mycard = Instantiate(_card,NormalPool);
            normalList.Add(mycard);
        }
        for (int i = 0; i < RemainEventCard; i++)
        {
            Card_event mycard = Instantiate(_eCard,EventPool);
            eventList.Add(mycard);
        }
    }

    void moveToDropPool(Card _card)
    {
        dropList.Add(_card);
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

    public void AddTheCard()   //抽卡
    {
        if (grid.childCount < 6) ////6为临时测试用，需要改成卡池上线的变量
        {
            
            SelectCardType();
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
        Card mycard = Instantiate(_card);
        mycard.transform.SetParent(grid.transform);

        _cardData.CardsList.Add(mycard);
        
        int random = Random.Range(1,_cardData.Array.Length);//从文件获取
        mycard.destination = mycard.transform.GetChild(0).GetComponent<Text>();        
        mycard.timeCast = Random.Range(1,4);//耗时
        mycard.destination.text = "Time. " + mycard.timeCast;
		      
        Item theitem = _itemData.ItemsList[Random.Range(0, _itemData.ItemsList.Count)];//临时 那个物件
        Item myitem = Instantiate(theitem);        
        myitem.transform.SetParent(mycard.transform);
        myitem.transform.localPosition = -myitem.CenterPos * 0.5f;
        myitem.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        mycard._item = myitem;
        
        mycard._item.consume = 0;// Random.Range(1, 5);//油耗
        mycard.profit = 4;///收益
        mycard.credit =1; //信誉 
        //mycard.destination.text = _cardData.destinations[Random.Range(0, _cardData.destinations.Count)];       
    }

    void GiveTheCardItem(Card _card)
    {
        int random = Random.Range(1, 100);//从文件获取
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
        if (random >= 75 && random < 100)
        {
            _card._item = _itemData.ItemsList[Random.Range(10, 23)];
        }
        
        int blockNum = _card._item.transform.childCount;
        _card._item.consume = Random.Range(1, 5);//油耗
        _card.profit = blockNum * _card.timeCast * 10;///收益
        _card.credit = 1;//信誉       
        //mycard.destination.text = _cardData.destinations[Random.Range(0, _cardData.destinations.Count)];

        Item myitem = Instantiate(_card._item);
        myitem.transform.SetParent(_card.transform);
        myitem.transform.localPosition = myitem.CenterPos;
        myitem.transform.localScale = new Vector3(1, 1, 1);
    }

    void Card_event()//事件卡
    {
        eventPanel.SetActive(true);
        //int index = Random.Range(0, _eData.namelist.Count);
        int myindex = TheIndex();

		eManage.influnce(myindex);

        string name = _eData.namelist[myindex];

        _event.text = "事件：" + name;
    }

    public void confirmEvent()
    {
        eventPanel.SetActive(false);       
    }

    public void cancelTheCard(Card _card)///退订卡片
    {
        RemainCard++;
        
        punish = 2;
        
       /* if (Distribution.totalCredit + punish - eManage.punish_inc - eManage.punish_dec < 0)
        {
            GameOver();
        }
        else */{
            Distribution.totalCredit = Distribution.totalCredit + punish + eManage.punish_inc;
            Distribution.totalProfit -= _card.timeCast + 1 + eManage.punish_dec;
            dManage.text_credit.text = "信誉" + Distribution.totalCredit.ToString();
            dManage.text_profit.text = "金币" + Distribution.totalProfit.ToString();
        }
        DestoryTheCard(_card);
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
		_card.Destroy();//删除该卡

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

	int TheIndex()
	{
		List<int> sameNum = new List<int>();
		List<int> diffNum = new List<int>();
		List<int> namelist_copy = new List<int> ();
		for(int i = 0;i< 8 /*_eData.namelist.Count*/;i++)
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
		//print("sameNum count" + sameNum.Count);
		//print("namelist count" + namelist_copy.Count);
		//print("diffNum count" + diffNum.Count);
		int n = Random.Range(0,diffNum.Count);
		return diffNum[n];
	}
    
	// Update is called once per frame
	void Update () {
        remainCardText.text = "剩余卡牌" + RemainCard.ToString();
	}
   
}
