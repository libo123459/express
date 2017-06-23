using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManage : MonoBehaviour {
    public Card_event eCard;
    public Transform grid;
	public Button _truckChoose;
	public int punish_event = 0;

    CardsData _cardData;
    EventData _eData;

    CardsManage cManage;
    TruckManage tManage;
	Distribution dManage;


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
		int n = 6 - _cardData.CardsList.Count;

		for (int i = 0; i < n; i++)
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
		mycard.name = _eData.namelist[index];
		mycard.type = "delayed1";

        switch (index)
        {
            case 2:
                mycard.CountDown = 7;
                doEvent01_0();
				
                break;
            case 3:
                mycard.CountDown = 5;
                doEvent01_1();
				
                break;
        }
		mycard.destination.text = mycard.name + "CountDown" + mycard.CountDown.ToString();
		_cardData.CardsList.Add(mycard);
    }    

    void doEvent01_0()
    {
		cManage.punish = 4;
    }

	public void undoEvent01()
	{
		cManage.punish = 2;
	}

    void doEvent01_1()
    {
		cManage.punish = 0;
    }
		
    public void Event_02(int index)
    {
		Card_event mycard = Instantiate(eCard);
		mycard.transform.SetParent(grid.transform);
		mycard.eventID = index;
		mycard.name = _eData.namelist[index];

		mycard.destination.text = mycard.name;

		_cardData.CardsList.Add(mycard);
    }

	public void CheckEvent02(int index)
	{
		switch(index)
		{
			case 4:
			doEvent02_0();
			break;
			case 5:
			doEvent02_1();
			break;
		}
	}

	public void doEvent02_0()
	{
		int n = dManage.totalCredit + 5;
		if(n >= dManage.MaxCredit)
		{
			dManage.totalCredit = 10;
		} else {
			dManage.totalCredit = n;
		}
		dManage.text_credit.text = dManage.totalCredit.ToString();
	}

	public void doEvent02_1()
	{
		for(int i = 0;i<tManage.trucksList.Count;i++)
		{
			Button Btn = tManage.trucksList[i].GetComponent<Button>();
			if(tManage.trucksList[i].state == "dist")
			{
				Btn.enabled = true;
				Btn.onClick.AddListener(()=>ChooseTruck_Event02_1(i));
			}else{
				Btn.enabled = false;
			}
		}
	}

	public void ChooseTruck_Event02_1(int truckNum)
	{
		print(truckNum.ToString());
		/*Truck _truck = tManage.trucksList[truckNum];
		print(truckNum);
		_truck.state = "finished";
		dManage.CreditLast(_truck);
		dManage.ProfitAtLast(_truck);
		dManage.TruckMoveToStation();
		for(int i = 0;i<tManage.trucksList.Count;i++)
		{
			Button Btn = tManage.trucksList[i].GetComponent<Button>();
			Btn.enabled = false;
		}*/
	}

    public void Event_03(int index)
    {
        Card_event mycard = Instantiate(eCard);
        mycard.transform.SetParent(grid.transform);
		mycard.eventID = index;
		mycard.type = "delayed2";
		mycard.name = _eData.namelist[index];
		mycard.destination.text = mycard.name + "CountDown" + mycard.CountDown.ToString();
		switch (index)
		{
		case 6:
			mycard.CountDown = 5;

			break;
		case 7:
			mycard.CountDown = 7;

			break;
		}
        _cardData.CardsList.Add(mycard);
    }

	public void CheckEvent03(int index)
	{
		switch (index)
		{
		case 6:
			doEvent03_0();

			break;
		case 7:
			doEvent03_1();

			break;
		}
	}

    void doEvent03_0()
    {
		for (int i = 0; i < tManage.trucksList.Count; i++)
		{
			if (tManage.trucksList[i].state == "dist")
			{
				tManage.trucksList[i].stopTime = 1;
			}
		}
    }

	void doEvent03_1()
	{
		for (int i = 0; i < 6 - _cardData.CardsList.Count; i++)
		{
			cManage.Card_normal();
		}
	}

    public void Event_04(int index)
    {

    }
    // Use this for initialization
    void Start () {
        cManage = this.GetComponent<CardsManage>();
        tManage = this.GetComponent<TruckManage>();
		dManage = this.GetComponent<Distribution>();
        _eData = this.GetComponent<EventData>();
        _cardData = this.GetComponent<CardsData>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
