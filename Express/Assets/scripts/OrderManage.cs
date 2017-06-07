using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManage : MonoBehaviour {
    public Transform orderPanel;
    public Order _order;
    public List<Order> OrdersList = new List<Order>();

    public void AddTheOrder(Card _card)
    {
        Order myorder = Instantiate(_order);
        myorder.transform.SetParent(orderPanel.transform);
        myorder.transform.localPosition = new Vector3(0, 0, 0);
        myorder.transform.localScale = new Vector3(1, 1, 1);
        myorder.ID = _card.ID;
        myorder.InitOrder();
        ShowDestination(myorder,_card);
        OrdersList.Add(myorder);
        
    }

    public void CancelTheOrder(Card _card)
    {
        
    }

    bool CheckNoThis(Order theOrder)
    {
        int num = 0;
        for (int i = 0; i < orderPanel.transform.childCount; i++)
        {
            if (orderPanel.transform.GetChild(i).GetComponent<Order>() != theOrder)
            {
                num++;
            }
        }

        if (num == orderPanel.transform.childCount)
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

        theorder.consume.text = "consume:" + _card._item.consume.ToString();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
