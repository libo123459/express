using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManage : MonoBehaviour {
    public Card_event eCard;
    public Transform grid;

    CardsData _cardData;

    public void Event_01()
    {
        Card_event mycard = Instantiate(eCard);
        mycard.transform.SetParent(grid.transform);
        mycard.CountDown = 5;////临时倒计时
        _cardData.CardsList.Add(mycard);
    }    

    void doEvent01()
    {

    }

    public void CheckEvent01(Card_event thecard)
    {
        if (thecard.CountDown > 0)
        {
            doEvent01();
        }
    }

    public void Event_02()
    {
        Card_event mycard = Instantiate(eCard);
        mycard.transform.SetParent(grid.transform);
        mycard.CountDown = 5;////临时倒计时
        _cardData.CardsList.Add(mycard);
    }

    void doEvent02()
    {

    }

    public void CheckEvent02(Card_event thecard)
    {
        if (thecard.CountDown == 0)
        {
            doEvent02();
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
