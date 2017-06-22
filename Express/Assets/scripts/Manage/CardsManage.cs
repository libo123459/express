using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsManage : MonoBehaviour {
    public Card _card;
    
    public Transform grid;

    public GameObject eventPanel;
    public Text _event;

    CardsData _cardData;
    EventData _eData;
    ItemData _itemData;
    Distribution dManage;
    OrderManage oManage;
    EventManage eManage;

	// Use this for initialization
	void Start () {
        _cardData = this.GetComponent<CardsData>();
        _eData = this.GetComponent<EventData>();
        _itemData = this.GetComponent<ItemData>();
        dManage = this.GetComponent<Distribution>();
        oManage = this.GetComponent<OrderManage>();
        eManage = this.GetComponent<EventManage>();
        RefreshTask();
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
        int chance = Random.Range(0,100);
        if (chance >= 50)
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
        mycard.timeCast = Random.Range(4,7);//耗时
        mycard.destination.text = _cardData.GetDestination(random) + "耗时" + mycard.timeCast;

        
        mycard._item = _itemData.ItemsList[Random.Range(0, _itemData.ItemsList.Count)];//临时 那个物件
        int blockNum = mycard._item.transform.childCount;
        mycard._item.consume = Random.Range(1, 5);//油耗
        mycard.profit = blockNum * mycard.timeCast;///收益
        mycard.credit = 1;//信誉
        mycard.punish = 2;//惩罚
        
        //mycard.destination.text = _cardData.destinations[Random.Range(0, _cardData.destinations.Count)];

        Item myitem = Instantiate(mycard._item);
        myitem.transform.SetParent(mycard.transform);
        myitem.transform.localPosition = new Vector3(-50, 100, 0);
        myitem.transform.localScale = new Vector3(1, 1, 1);
    }

    void Card_event()//事件卡
    {
        eventPanel.SetActive(true);
        int index = Random.Range(0, _eData.namelist.Count);
        eManage.influnce(index);

        string name = _eData.namelist[index];

        _event.text = "事件：" + name;        
    }

    public void confirmEvent()
    {
        eventPanel.SetActive(false);       
    }

    public void cancelTheCard(Card _card)///退订卡片
    {
        dManage.totalCredit = dManage.totalCredit - _card.punish;
        dManage.text_credit.text = dManage.totalCredit.ToString();
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

    
	// Update is called once per frame
	void Update () {
		
	}
   
}
