using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manage : MonoBehaviour {
    public GameObject truckpanel;
    public GameObject cardpanel;
    public GameObject assemblepanel;
    public GameObject distributionpanel;
    public GameObject slotPanel;
    public GameObject shopPanel;
    public GameObject recruitPanel;

    CardsManage cManage;
    Distribution dManage;
    
    AssembleManage aManage;
    TruckManage tManage;

    int money;

    public void OpenLevel()
    {

    }


    public void Distribute()
    {
        cManage.CancelTheSendedCard();//将装配上的物件从卡池中消除

        dManage.distribution(AssembleManage.currentTruck);//配送

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
        for (int i = 0; i < AssembleManage.slotList.Count; i++)
        {
            DestroyImmediate(AssembleManage.slotList[i].gameObject);
        }
        AssembleManage.slotList.Clear();
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
    }

    public void OpenRecruit()
    {
        recruitPanel.SetActive(true);
    }

    public void CloseRecruit()
    {
        recruitPanel.SetActive(false);
    }

    // Use this for initialization
    void Start ()
    {
        cManage = this.GetComponent<CardsManage>();
        aManage = this.GetComponent<AssembleManage>();
        dManage = this.GetComponent<Distribution>();
        
        tManage = this.GetComponent<TruckManage>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
