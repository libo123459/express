using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manage : MonoBehaviour {
    public GameObject truckpanel;
    public GameObject cardpanel;
    public GameObject assemblepanel;
    public GameObject distributionpanel;
    public GameObject slotPanel;
    public GameObject destpanel;

    CardsManage cManage;
    //OrderManage oManage;
    Distribution dManage;
    AssembleManage aManage;

    int money;
    
    public void Distribute()
    {
        cManage.CancelTheSendedCard();//将装配上的物件从卡池中消除

        dManage.distribution(aManage.currentTruck);//配送

        ClearTheSlot();//清除slot

        assemblepanel.SetActive(false);
        distributionpanel.SetActive(true);
        
        truckpanel.SetActive(true);
    }

    public void Assemble(int truckNum)
    {
        aManage.Assemble(truckNum);
        dManage.ClearDest(truckNum);

        assemblepanel.SetActive(true);
        distributionpanel.SetActive(false);
        
        truckpanel.SetActive(false);
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
	void Start ()
    {
        cManage = this.GetComponent<CardsManage>();
       // oManage = this.GetComponent<OrderManage>();
        dManage = this.GetComponent<Distribution>();
        aManage = this.GetComponent<AssembleManage>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
