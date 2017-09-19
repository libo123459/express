﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TruckManage : MonoBehaviour {
    public Truck _truck;
    public Distribution dManage;
    public CardsData cData;
    //public GameObject truck_image;
    public static List<Truck> trucksList = new List<Truck>();
   
    public Transform truckPanel;

    public static int truckNumMax = 3;
    public static int teamID;
    public static int skillID;
	// Use this for initialization
	void Start ()
    {
        cData = GameObject.Find("Manage").GetComponent<CardsData>();
        dManage = GameObject.Find("Manage").GetComponent<Distribution>();
        GiveTheTeamPara(2);
        for (int i = 0; i < truckNumMax; i++)/////////临时
        {
            Truck mytruck = Instantiate(_truck);

            mytruck.transform.SetParent(truckPanel);
            mytruck.transform.localPosition = new Vector3(-720, 200 * i - 25, 0);
            mytruck.transform.localScale = new Vector3(1,1,1);
            mytruck.ID = 1;
            GiveTheTruckPara(mytruck,teamID);
            trucksList.Add(mytruck);
        }

        trucksList[1].active = false;
        trucksList[1].gameObject.SetActive(false);
        trucksList[2].active = false;
        trucksList[2].gameObject.SetActive(false);
	}   

    void GiveTheTruckPara(Truck _truck,int id)
    {
        _truck.row = TruckData.GetWidth(id,_truck.ID);
        _truck.column = TruckData.GetHeight(id,_truck.ID);        
    }

    void GiveTheTeamPara(int id)//加载车队序号
    {
        teamID = id;        
    }

    public void TeamSkill(int teamID)
    {
        switch (teamID)
        {
            case 1:
                TeamSkill01();
                break;
            case 2:
                TeamSkill02();
                break;
        }
    }

    void TeamSkill01()
    {        
        if (Distribution.totalProfit >= 10)
        {
            if (Distribution.level < 2)
            {
                Distribution.level += 1;
                Distribution.MaxCredit += 5;
            }            
        }
        if (Distribution.totalProfit >= 20)
        {
            if (Distribution.level < 3)
            {
                Distribution.level += 1;
                if (Distribution.stage < 2)
                {
                    Distribution.stage += 1;
                    TruckManage.trucksList[1].active = true;
                    TruckManage.trucksList[1].gameObject.SetActive(true);
                    dManage.destPanelList[1].SetActive(true);
                }
            }            
        }
        if (Distribution.totalProfit >= 30)
        {
            if (Distribution.level < 4)
            {
                Distribution.level += 1;
                Distribution.MaxCredit += 5;
            }            
        }
        if (Distribution.totalProfit >= 40)
        {
            if (Distribution.level < 5)
            {
                Distribution.level += 1;
                if (Distribution.stage < 3)
                {
                    Distribution.stage += 1;
                    TruckManage.trucksList[2].active = true;
                    TruckManage.trucksList[2].gameObject.SetActive(true);
                    dManage.destPanelList[2].SetActive(true);
                }
            }            
        }
        if (Distribution.totalProfit >= 50)
        {
            if (Distribution.level < 6)
            {
                Distribution.level += 1;
                Distribution.coe_timeCast_Team1 = -1;
            }            
        }
    }
    void TeamSkill02()
    {
        if (Distribution.totalProfit >= 10)
        {
            if (Distribution.level < 2)
            {
                Distribution.level += 1;
                CardsManage.MaxCount += 1;
                print(CardsManage.MaxCount);
            }
        }
        if (Distribution.totalProfit >= 20)
        {
            if (Distribution.level < 3)
            {
                Distribution.level += 1;
                if (Distribution.stage < 2)
                {
                    Distribution.stage += 1;
                    TruckManage.trucksList[1].active = true;
                    TruckManage.trucksList[1].gameObject.SetActive(true);
                    dManage.destPanelList[1].SetActive(true);
                }
            }
        }
        if (Distribution.totalProfit >= 30)
        {
            if (Distribution.level < 4)
            {
                Distribution.level += 1;
                CardsManage.MaxCount += 1;
            }
        }
        if (Distribution.totalProfit >= 40)
        {
            if (Distribution.level < 5)
            {
                Distribution.level += 1;
                if (Distribution.stage < 3)
                {
                    Distribution.stage += 1;
                    TruckManage.trucksList[2].active = true;
                    TruckManage.trucksList[2].gameObject.SetActive(true);
                    dManage.destPanelList[2].SetActive(true);
                }
            }
        }
        if (Distribution.totalProfit >= 50)
        {
            if (Distribution.level < 6)
            {
                Distribution.level += 1;
                List<int> tmp = new List<int>();
                int same = 0;
                int num = AssembleManage.currentTruck;
                for (int i = 0; i < trucksList[num].orderNum; i++)
                {
                    if (tmp.Count == 0)
                    {
                        tmp.Add(trucksList[num].IDs[i]);
                    }
                    else {
                        if (trucksList[num].IDs[i] != trucksList[num].IDs[i - 1])
                        {
                            tmp.Add(trucksList[num].IDs[i]);
                        }
                        else {
                            same = trucksList[num].IDs[i];
                        }
                    }
                }
                if (tmp.Count < trucksList[num].orderNum)
                {
                    Distribution.coe_timeCast_Team1 = -cData.GetTimeCast(same);
                }
            }
        }
    }
    
    // Update is called once per frame
    void Update () {
		
	}
}
