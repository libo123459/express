using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManage : MonoBehaviour {
    public Transform orderPanel;
    public Order _order;
    public List<Order> OrdersList = new List<Order>();

    public void AddTheOrder(Card _card)
    {
        if (CheckNoThis(_order) == true) ///error!!!!!!!!!!!
        {
            Order myorder = Instantiate(_order);
            myorder.transform.SetParent(orderPanel.transform);
            myorder.transform.localPosition = new Vector3(0, 0, 0);
            myorder.transform.localScale = new Vector3(1, 1, 1);

            myorder.ShowDestination(_card.destination.text);
            myorder.ShowTimeCast(_card.timeCast);
            myorder.ShowConsume(_card._item.consume);

            OrdersList.Add(myorder);
        }
        
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
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
