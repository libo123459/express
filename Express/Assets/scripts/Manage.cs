using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manage : MonoBehaviour {
    public GameObject truckpanel;
    public GameObject cardpanel;
    public GameObject assemblepanel;
    public GameObject distributionpanel;

    CardsManage cManage;
    OrderManage oManage;
    Distribution _distribution;
    
    public void Distribute()
    {
        _distribution.distribution(0);
        cManage.CancelTheSendedCard();
        assemblepanel.SetActive(false);
        distributionpanel.SetActive(true);
    }

    // Use this for initialization
	void Start () {
        cManage = this.GetComponent<CardsManage>();
        oManage = this.GetComponent<OrderManage>();
        _distribution = this.GetComponent<Distribution>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
