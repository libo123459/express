using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manage : MonoBehaviour {
    public GameObject truckpanel;
    public GameObject cardpanel;
    public GameObject assemblepanel;
    public GameObject distributionpanel;
    public GameObject slotPanel;

    CardsManage cManage;
    OrderManage oManage;
    Distribution _distribution;
    AssembleManage aManage;
    
    public void Distribute()
    {
        cManage.CancelTheSendedCard();

        _distribution.distribution(aManage.currentTruck);

        ClearTheSlot();

        assemblepanel.SetActive(false);
        distributionpanel.SetActive(true);
    }

    public void Assemble(int truckNum)
    {
        aManage.Assemble(truckNum);

        assemblepanel.SetActive(true);
        distributionpanel.SetActive(false);
        
    }

    public void ClearTheSlot()
    {
        
        for (int i = 0; i < aManage.slotList.Count; i++)
        {
            DestroyImmediate(aManage.slotList[i].gameObject);
        }
        aManage.slotList.Clear();
    }

    // Use this for initialization
	void Start () {
        cManage = this.GetComponent<CardsManage>();
        oManage = this.GetComponent<OrderManage>();
        _distribution = this.GetComponent<Distribution>();
        aManage = this.GetComponent<AssembleManage>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
