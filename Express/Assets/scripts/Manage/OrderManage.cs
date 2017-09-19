﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManage : MonoBehaviour {
    public Transform orderPanel;
    public Order _order;
    public List<Order> OrdersList = new List<Order>();    
    
    public void AddTheOrder(Card _card)
    {
        if (CheckNoThis(_card) == true)
        {
            Order myorder = Instantiate(_order);
            myorder.transform.SetParent(orderPanel.transform);
            myorder.ID = _card.ID;
            myorder.IDinPanel = _card.IDinPanel;
            myorder.profit = _card.profit;
            myorder.credit = _card.credit;
            myorder.skillID = _card.skillID;
            myorder.blockNum = _card._item.BlockNum;
            myorder.InitOrder();
            ShowDestination(myorder, _card);
            OrdersList.Add(myorder);
        }
        if (_card.skillID == 8)
        {
            Distribution.diceMax = TruckData.GetDiceMax(1, Distribution.stage);
            Distribution.dice = Distribution.diceMax;
            print(Distribution.dice.ToString());
        }
        if (_card.skillID == 9)
        {
            Distribution.diceMin = TruckData.GetDiceMin(1, Distribution.stage);
            Distribution.dice = Distribution.diceMin;
            print(Distribution.dice.ToString());
        }
    }

    public void SendOrderToTruck(Truck _truck)
    {
        _truck.orderNum = OrdersList.Count;
        for (int i = 0; i < OrdersList.Count; i++)///////把已装配的订单数据传输到当前truck上
        {
            _truck.timeCast.Add(OrdersList[i]._timecast);
            _truck.IDs.Add(OrdersList[i].ID);
            _truck.profit.Add(OrdersList[i].profit);
            _truck.credit.Add(OrdersList[i].credit);
            _truck.skillList.Add(OrdersList[i].skillID);
            _truck.remain = _truck.remain + _truck.timeCast[i];///车辆剩余回合数
            _truck.TotalTimecast = _truck.remain;
            _truck.blockNum += OrdersList[i].blockNum;
            DestroyImmediate(OrdersList[i].gameObject);// 清除任务栏上的任务
        }
    }

    bool CheckNoThis(Card thecard)
    {
        int sameNum = 0;
        for (int i = 0; i < OrdersList.Count; i++)
        {
            if (thecard.IDinPanel == OrdersList[i].IDinPanel)
            {                
                sameNum++;
            }
        }

        if (sameNum == 0)
        {
            return true;
        }
        else {
            return false;
        }
    }

    public void ShowDestination(Order theorder, Card _card)
    {
        theorder.destination.text = _card.TimeCast.text;

        theorder.timeCast.text = "time:" + _card.timeCast.ToString();
        theorder._timecast = _card.timeCast;

        theorder.consume.text = "consume:" + _card._item.consume.ToString();
        theorder._consume = _card._item.consume;
    }

    public void CancelOrder(int id)
    {
        for (int i = 0; i < OrdersList.Count; i++)
        {
            if (OrdersList[i].IDinPanel == id)
            {
                DestroyImmediate(OrdersList[i].gameObject);
            }
        }
        RefreshOrder();
        Distribution.dice = Distribution.dicePre;

    }

    void RefreshOrder()
    {
        List<Order> tmp = new List<Order>();
        for (int i = 0; i < OrdersList.Count; i++)
        {
            if (OrdersList[i] != null)
            {
                tmp.Add(OrdersList[i]);
            }
        }
        OrdersList.Clear();
        for (int i = 0; i < tmp.Count; i++)
        {
            OrdersList.Add(tmp[i]);
        }

    }

    // Use this for initialization
    void Start () {
		
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
