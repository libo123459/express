using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Card : MonoBehaviour {
    public int ID;
    public int timeCast;
    public int profit;
    public int credit;
    public int skillID;
    public int count = 0;

    public string state;

    public Text TimeCast;
    public Text Description;
    public Item _item;
    public Button _cancel;

    protected CardsManage cManage;
    
    public void Destroy()
    {        
        DestroyImmediate(this.gameObject);
    }
    
    // Use this for initialization
    void Awake () {
        _cancel = this.transform.Find("cancel").GetComponent<Button>();
        cManage = GameObject.Find("Manage").GetComponent<CardsManage>();
		
        _cancel.onClick.AddListener(() => cManage.cancelTheCard(this));
    }
    // Update is called once per frame
    void Update () {
		
	}
}
