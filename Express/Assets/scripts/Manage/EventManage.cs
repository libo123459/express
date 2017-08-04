using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManage : MonoBehaviour {
    public Card_event eCard;
    public Transform grid;
	public BTN_truckChoose _truckChoose;
    public GameObject _truckChoosePanel;
	public int punish_inc = 0;
    public int punish_dec = 0;

    CardsData _cardData;
    EventData _eData;

    CardsManage cManage;
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
        if (n != 0)
        {
            for (int i = 0; i < n; i++)
            {
                cManage.Card_normal();
            }
        }
		
    }

    void doEvent00_1()//大雪
    {
        for (int i = 0; i < TruckManage.trucksList.Count; i++)
        {
            if (TruckManage.trucksList[i].state == "dist")
            {
                TruckManage.trucksList[i].stopTime = 2;
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
        mycard.use.gameObject.SetActive(false);
        mycard._cancel.gameObject.SetActive(false);
        switch (index)
        {
            case 2:
                mycard.CountDown = 6;
                mycard.TimeCast.text = mycard.name + "CountDown" 
                    + (mycard.CountDown + 1).ToString() + "\n" + "7回合内外送信用加1";
                doEvent01_0();
				
                break;
            case 3:
                mycard.CountDown = 4;
                mycard.TimeCast.text = mycard.name + "CountDown"
                    + (mycard.CountDown + 1).ToString() + "\n" + "5回合内外包费用加1";
                doEvent01_1();
				
                break;
        }
		
		_cardData.CardsList.Add(mycard);
    }    

    void doEvent01_0()///信用严打
    {
		punish_inc = 1;
    }

	public void undoEvent01(int index)///恢复
	{
        switch (index)
        {
            case 2:
                punish_inc = 0;
                break;
            case 3:
                punish_dec = 0;
                break;
        }
	}

    void doEvent01_1()///这货不能送
    {
        punish_dec = 1; ;
    }
		
    public void Event_02(int index)///时间类型——使用卡
    {
		Card_event mycard = Instantiate(eCard);
		mycard.transform.SetParent(grid.transform);
		mycard.eventID = index;
		mycard.name = _eData.namelist[index];
        mycard.TimeCast.text = mycard.name;
        mycard._cancel.gameObject.SetActive(false);
        switch (index)
        {
            case 4:
                mycard.TimeCast.text = mycard.name + "\n" + "恢复5点信用值";
                break;
            case 5:
                mycard.TimeCast.text = mycard.name + "\n" + "选择一名司机立即返回并结算配送奖励";
                break;
        }

        _cardData.CardsList.Add(mycard);
    }

	public void CheckEvent02(int index)////分支器
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

	public void doEvent02_0()///信用锦旗
	{
		float n = Distribution.totalCredit + 5;
		if(n >= Distribution.MaxCredit)
		{
            Distribution.totalCredit = 10;
		} else {
            Distribution.totalCredit = n;
		}
		dManage.text_credit.text = Distribution.totalCredit.ToString();
	}

	public void doEvent02_1()///光速驾驶
	{
        int distTruck = 0;        
        
        for (int i = TruckManage.trucksList.Count - 1; i>=0;i--)
		{
            if (TruckManage.trucksList[i].state == "dist")
            {
                BTN_truckChoose Btn = Instantiate(_truckChoose, _truckChoosePanel.transform);
                Btn.eManage = this;
                Btn.id = i;
                Text text = Btn.transform.GetChild(0).GetComponent<Text>();
                text.text = "truck" + i;
                distTruck++;
            }            
		}
        if (distTruck != 0)
        {
            _truckChoosePanel.SetActive(true);
        }
	}

	public void ChooseTruck_Event02_1(int truckNum) ///选择回归车辆
	{
		Truck _truck = TruckManage.trucksList[truckNum];
		
		//_truck.state = "finished";
		
        dManage.ProfitAtLast(_truck);
        dManage.ClearDest(_truck.ID);
        _truck.transform.position = _truck.StartPos;
        _truck.state = "empty";
        _truck.profit.Clear();
        _truck.credit.Clear();
         for (int i = 0; i < _truckChoosePanel.transform.childCount; i++)
        {
             Destroy(_truckChoosePanel.transform.GetChild(i).gameObject);
        }
        _truckChoosePanel.SetActive(false);
    }

    public void Event_03(int index) ///事件类型——延迟结算
    {
        Card_event mycard = Instantiate(eCard);
        mycard.transform.SetParent(grid.transform);
		mycard.eventID = index;
		mycard.type = "delayed2";
		mycard.name = _eData.namelist[index];
		
        mycard.use.gameObject.SetActive(false);
        mycard._cancel.gameObject.SetActive(false);

        switch (index)
		{
		case 6:
			mycard.CountDown = 4;
                mycard.TimeCast.text = mycard.name 
                    + "CountDown" + (mycard.CountDown + 1).ToString() + "\n" + "5回合后所有配送中车辆增加一回合";
                break;
		case 7:
			mycard.CountDown = 6;
                mycard.TimeCast.text = mycard.name 
                    + "CountDown" + (mycard.CountDown + 1).ToString() + "\n"+ "7回合后执行“购物狂欢节";
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

    void doEvent03_0() //交通堵塞
    {
		for (int i = 0; i < TruckManage.trucksList.Count; i++)
		{
			if (TruckManage.trucksList[i].state == "dist")
			{
                TruckManage.trucksList[i].stopTime = TruckManage.trucksList[i].stopTime + 1;
			}
		}
    }

	void doEvent03_1() //下周大狂欢
	{
        int n = 6 - _cardData.CardsList.Count;
        if (n != 0)
        {
            for (int i = 0; i < n; i++)
            {
                cManage.Card_normal();
            }
        }
        
	}

    public void Event_04(int index)
    {
        Card_event mycard = Instantiate(eCard);
        mycard.transform.SetParent(grid.transform);
        mycard.eventID = index;
        mycard.name = _eData.namelist[index];
        mycard.TimeCast.text = mycard.name;
        mycard._cancel.gameObject.SetActive(false);
        switch (index)
        {
            case 8:
                doEvent04_0();
                break;
            case 9:
                doEvent04_1();
                break;
            case 10:
                doEvent04_2();
                break;
        }
        _cardData.CardsList.Add(mycard);
    }

    void doEvent04_0()
    {

    }

    void doEvent04_1()
    {

    }

    void doEvent04_2()
    {

    }
    // Use this for initialization
    void Start () {
        cManage = this.GetComponent<CardsManage>();
		dManage = this.GetComponent<Distribution>();
        _eData = this.GetComponent<EventData>();
        _cardData = this.GetComponent<CardsData>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
