using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Card : MonoBehaviour {
    public int ID;
    public int timeCast;
    public int profit;
    public int credit;
    
    
    public Text destination;
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
		destination = this.transform.GetChild(0).GetComponent<Text>();
        _cancel.onClick.AddListener(() => cManage.cancelTheCard(this));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
