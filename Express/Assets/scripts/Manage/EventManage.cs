using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManage : MonoBehaviour {
    public Card_event eCard;
    public Transform grid;

    CardsData _cardData;
    EventData _eData;

    CardsManage cManage;
    TruckManage tManage;

    public void influnce(int index)
    {
        if (index >= 0 && index <= 1)
        {
            Event_00(index);///立即生效
        }
        if (index >= 2 && index <= 3)
        {
            Event_01(index);//延时状态
        }
        if (index >= 4 && index <= 5)
        {
            Event_02(index);//使用
        }
        if (index >= 6 && index <= 7)
        {
            Event_03(index);//延时结算
        }
        if (index >= 8 && index <= 10)
        {
            Event_04(index);//任务
        }
    }

    public void Event_00(int index)//立即生效
    {
        switch (index)
        {
            case 0:
                doEvent00_0();
                break;
            case 1:
                doEvent00_1();
                break;
        }
    }

    void doEvent00_0()//购物狂欢节
    {
        for (int i = 0; i < 6 - _cardData.CardsList.Count; i++)
        {
            cManage.Card_normal();
        }
    }

    void doEvent00_1()//大雪
    {
        for (int i = 0; i < tManage.trucksList.Count; i++)
        {
            if (tManage.trucksList[i].state == "dist")
            {
                tManage.trucksList[i].stopTime = 2;
            }
        }
    }

    public void Event_01(int index)//倒计时状态
    {
        Card_event mycard = Instantiate(eCard);
        mycard.transform.SetParent(grid.transform);
        mycard.eventID = index;
        switch (index)
        {
            case 0:
                mycard.CountDown = 7;
                doEvent01_0();
                break;
            case 1:
                mycard.CountDown = 5;
                doEvent01_1();
                break;
        }
        
        _cardData.CardsList.Add(mycard);
    }    

    void doEvent01_0()
    {
        for (int i = 0; i < _cardData.CardsList.Count; i++)
        {
            _cardData.CardsList[i].punish = 4;
        }
    }

    void doEvent01_1()
    {
        for (int i = 0; i < _cardData.CardsList.Count; i++)
        {
            _cardData.CardsList[i].punish = 0;
        }
    }

    public void CheckEvent01(Card_event thecard)
    {
        if (thecard.CountDown == 0)
        {
            
        }
    }

    public void Event_02(int index)
    {
        
    }    

    public void Event_03(int index)
    {
        Card_event mycard = Instantiate(eCard);
        mycard.transform.SetParent(grid.transform);
        mycard.CountDown = 5;////临时倒计时
        _cardData.CardsList.Add(mycard);
    }

    void doEvent03()
    {

    }

    public void CheckEvent03(Card_event thecard)
    {
        if (thecard.CountDown == 0)
        {
            doEvent03();
        }
    }

    public void Event_04(int index)
    {

    }
    // Use this for initialization
    void Start () {
        cManage = this.GetComponent<CardsManage>();
        tManage = this.GetComponent<TruckManage>();
        _eData = this.GetComponent<EventData>();
        _cardData = this.GetComponent<CardsData>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
