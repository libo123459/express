using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TruckManage : MonoBehaviour {
    public Truck _truck;
    //public GameObject truck_image;
    public static List<Truck> trucksList = new List<Truck>();
    public static List<int> trucksGarage = new List<int>();
    public Transform truckPanel;
    public Transform changePanel;
    public Transform garagePanel;
    public Transform gridInGarage;
    public GameObject truck_display_inshop;
    public truckInGarage truck_display_ingarage;
   
    public int preTruckNum;

    TruckData tData;
    public static int truckNumMax = 3;

	// Use this for initialization
	void Start ()
    {
        tData = this.GetComponent<TruckData>();
        for (int i = 0; i < truckNumMax; i++)/////////临时
        {
            Truck mytruck = Instantiate(_truck);

            mytruck.transform.SetParent(truckPanel);
            mytruck.transform.localPosition = new Vector3(-720, 200 * i - 25, 0);
            mytruck.transform.localScale = new Vector3(1,1,1);
            mytruck.ID = 1;
            GiveTheTruckPara(mytruck,mytruck.ID);
            trucksList.Add(mytruck);
        }

        trucksList[1].active = false;
        trucksList[1].gameObject.SetActive(false);
        trucksList[2].active = false;
        trucksList[2].gameObject.SetActive(false);
	}

    public void DisplayTruckInShopPanel(Transform shopPanel)
    {
        if (shopPanel.GetChild(0).transform.childCount == 0)
        {
            for (int i = 2; i < 11; i++)//前11个车
            {
                GameObject _truckInshop = Instantiate(truck_display_inshop, shopPanel.GetChild(0).transform);
                //_truckInshop.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = TruckData.GetName(i) + " " + TruckData.GetPrice(i);
            }
        }        
    }

    public void buyTheTruck(GameObject butBtn)
    {
        int ID = butBtn.transform.GetSiblingIndex() + 2;
        truckInGarage tIng = Instantiate(truck_display_ingarage,gridInGarage);
        tIng.id = ID;
        //GetTruckFromGarage(0);
        //Distribution.totalProfit -= TruckData.GetPrice(ID);        
    }    

    void GiveTheTruckPara(Truck _truck,int id)
    {
        _truck.row = 3;//TruckData.GetWidth(id);
        _truck.column = 3;//TruckData.GetHeight(id);
        //_truck.consume = 1;//TruckData.GetConsume(id);
        //_truck.price = TruckData.GetPrice(id);
       // _truck.skillID = TruckData.GetSkillId(id);
    }

    public static void TruckSkill(Truck _truck)
    {        
        int index = _truck.ID;
        print(_truck.ID);
        if (index > 6)
        {
            switch (index)
            {
                case 7:
                    Skill01(_truck);
                    break;
                case 8:
                    Skill02(_truck);
                    break;
                case 9:
                    Skill03(_truck);
                    break;
                case 10:
                    Skill04(_truck);
                    
                    break;
            }
        }
    }

    static void Skill01(Truck _truck) //10回合后油耗加倍
    {
        if (_truck.state == "dist")
        {
            int n = _truck.TotalTimecast - _truck.remain;
            if (n > 10)
            {
                _truck.consume = _truck.consume * 5;
            }
        }        
    }

    static void Skill02(Truck _truck)
    {

    }

    static void Skill03(Truck _truck)
    {
        

    }

    static void Skill04(Truck _truck)//跃迁
    {
        if (_truck.state == "dist")
        {
            int move = Random.Range(1, 101);
            if (move <= 50)
            {
                if (_truck.stopTime == 0)
                {
                    _truck.remain -= 2;
                    float unitShift = 0;
                    unitShift = 1300.0f / (float)_truck.TotalTimecast;
                    float xPos = _truck.transform.localPosition.x + (unitShift * 2);
                    float yPos = _truck.transform.localPosition.y;
                    _truck.transform.localPosition = new Vector3(xPos, yPos, 0);
                }
                else
                {
                    _truck.stopTime -= 2;
                }
            }
            if (move >50 && move <=90)
            {
                if (_truck.stopTime == 0)
                {
                    _truck.remain -= 1;
                    float unitShift = 0;
                    unitShift = 1300.0f / (float)_truck.TotalTimecast;
                    float xPos = _truck.transform.localPosition.x + unitShift;
                    float yPos = _truck.transform.localPosition.y;
                    _truck.transform.localPosition = new Vector3(xPos, yPos, 0);
                }
                else
                {
                    _truck.stopTime -= 1;
                }
            }
            print("move" + move.ToString());
        }
    }

    public void openChangePanel()
    {
        garagePanel.gameObject.SetActive(true);
        for (int i = 0; i < gridInGarage.childCount; i++)
        {
            int id = gridInGarage.GetChild(i).GetComponent<truckInGarage>().id;
           // gridInGarage.GetChild(i).GetChild(0).GetComponent<Text>().text = TruckData.GetName(id);
        }        
    }

    public void pressTruck(Truck truck)
    {
       preTruckNum = truck.transform.GetSiblingIndex();
    }

    public void closeChangePanel()
    {
        garagePanel.gameObject.SetActive(false);
    }

    public void changeTheTruck(truckInGarage truckInGarage)
    {
        Truck truck = trucksList[preTruckNum];
        int id = truck.ID;
        truck.ID = truckInGarage.id;
        truckInGarage.id = id;
        GiveTheTruckPara(truck, truck.ID);        
        closeChangePanel();
    }
    
    // Update is called once per frame
    void Update () {
		
	}
}
