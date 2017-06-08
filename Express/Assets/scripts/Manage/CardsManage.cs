using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsManage : MonoBehaviour {
    public Card _card;
    
    public Transform grid;
    CardsData _cardData;
    ItemData _itemData;
    int RefreshTime;
    OrderManage oManage;
	// Use this for initialization
	void Start () {
        _cardData = this.GetComponent<CardsData>();
        _itemData = this.GetComponent<ItemData>();
        oManage = this.GetComponent<OrderManage>();
        RefreshTask();
	}
    void RefreshTask()
    {
        for (int i = 0; i < 4; i++)
        {
            Card mycard = Instantiate(_card);
            mycard.transform.SetParent(grid.transform);
            //mycard.transform.localPosition = new Vector3(0, 0, 0);
            //mycard.transform.localScale = new Vector3(1, 1, 1);

            _cardData.CardsList.Add(mycard);

            mycard.timeCast = Random.Range(2,6);//耗时
            mycard._item = _itemData.ItemsList[Random.Range(0, 2)];//临时
            mycard._item.consume = Random.Range(1, 5);//油耗
            mycard.destination = mycard.transform.GetChild(0).GetComponent<Text>();
            mycard.destination.text = _cardData.destinations[Random.Range(0, 10)];
            mycard.ID = i;

            Item myitem = Instantiate(mycard._item);
            myitem.transform.SetParent(mycard.transform);
            myitem.transform.localPosition = new Vector3(0,0,0);
            myitem.transform.localScale = new Vector3(1,1,1);
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

        print(grid.transform.childCount.ToString());
        
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
            Card mycard = Instantiate(_card);
            mycard.transform.SetParent(grid.transform);

            _cardData.CardsList.Add(mycard);

            mycard.timeCast = Random.Range(2, 6);//耗时
            mycard._item = _itemData.ItemsList[Random.Range(0, 2)];//临时
            mycard._item.consume = Random.Range(1, 5);//油耗
            mycard.destination = mycard.transform.GetChild(0).GetComponent<Text>();
            mycard.destination.text = _cardData.destinations[Random.Range(0, 10)];

            Item myitem = Instantiate(mycard._item);
            myitem.transform.SetParent(mycard.transform);
            myitem.transform.localPosition = new Vector3(0, 0, 0);
            myitem.transform.localScale = new Vector3(1, 1, 1);
        }
        for (int i = 0; i < grid.childCount; i++)         //刷新所有卡的序列号
        {
            grid.transform.GetChild(i).GetComponent<Card>().ID = i;
        }
    }


	// Update is called once per frame
	void Update () {
		
	}
}
