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
	// Use this for initialization
	void Start () {
        _cardData = this.GetComponent<CardsData>();
        _itemData = this.GetComponent<ItemData>();
        RefreshTask();
	}
    void RefreshTask()
    {
        for (int i = 0; i < 4; i++)
        {
            Card mycard = Instantiate(_card);
            mycard.transform.SetParent(grid.transform);
            mycard.transform.localPosition = new Vector3(0, 0, 0);
            mycard.transform.localScale = new Vector3(1, 1, 1);

            _cardData.CardsList.Add(mycard);

            mycard.timeCast = Random.Range(2,6);//耗时
            mycard._item = _itemData.ItemsList[Random.Range(0, 2)];//临时
            mycard._item.timeCast = Random.Range(1,3);
            mycard.destination = mycard.transform.GetChild(0).GetComponent<Text>();
            mycard.destination.text = mycard._item.gameObject.name;
            mycard.ID = i;

            Item myitem = Instantiate(mycard._item);
            myitem.transform.SetParent(mycard.transform);
            myitem.transform.localPosition = new Vector3(0,0,0);
            myitem.transform.localScale = new Vector3(1,1,1);
        }
        
    }
	// Update is called once per frame
	void Update () {
		
	}
}
