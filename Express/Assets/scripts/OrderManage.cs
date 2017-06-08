using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManage : MonoBehaviour {
    public Transform orderPanel;
    public Order _order;
    public List<Order> OrdersList = new List<Order>();
    TruckManage _tManage;
    
    public void AddTheOrder(Card _card)
    {
        if (CheckNoThis(_card) == true)
        {
            Order myorder = Instantiate(_order);
            myorder.transform.SetParent(orderPanel.transform);
            myorder.transform.localPosition = new Vector3(0, 0, 0);
            myorder.transform.localScale = new Vector3(1, 1, 1);
            myorder.ID = _card.ID;
            myorder.InitOrder();
            ShowDestination(myorder, _card);
            OrdersList.Add(myorder);
        }
    }

    public void CancelTheOrder(Card _card)
    {
        
    }

    bool CheckNoThis(Card thecard)
    {
        int sameNum = 0;
        for (int i = 0; i < OrdersList.Count; i++)
        {
            if (thecard.ID == OrdersList[i].ID)
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
        theorder.destination.text = _card.destination.text;

        theorder.timeCast.text = "time:" + _card.timeCast.ToString();
        theorder._timecast = _card.timeCast;

        theorder.consume.text = "consume:" + _card._item.consume.ToString();
        theorder._consume = _card._item.consume;
    }

    void RrefreshTheList()
    {
        List<Order> _temp = new List<Order>();
        for (int i = 0; i < OrdersList.Count; i++)
        {
            if (OrdersList[i] != null)
            {
                _temp.Add(OrdersList[i]); 
            }
        }
        OrdersList.Clear();
        for (int i = 0; i < _temp.Count; i++)
        {
            OrdersList.Add(_temp[i]);
        }
    }

    
    // Use this for initialization
    void Start () {
		_tManage = GameObject.Find("Manage").GetComponent<TruckManage>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
