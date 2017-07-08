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
    public Transform garage;
    public GameObject truck_display_inshop;
    TruckData tData;
    int truckNumMax = 3;

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
            mytruck.ID = i;
            GiveTheTruckPara(mytruck,1);
            trucksList.Add(mytruck);            
        }
        print("trucknum" + trucksList.Count);
	}

    public void DisplayTruckInShopPanel(Transform shopPanel)
    {
        if (shopPanel.GetChild(0).transform.childCount == 0)
        {
            for (int i = 0; i < 11; i++)//前11个车
            {
                GameObject _truckInshop = Instantiate(truck_display_inshop, shopPanel.GetChild(0).transform);
            }
        }        
    }

    public void buyTheTruck(GameObject butBtn)
    {
        Truck mytruck = Instantiate(_truck);
        mytruck.transform.SetParent(garage);
     
        mytruck.ID = butBtn.transform.GetSiblingIndex() + 1;
        GiveTheTruckPara(mytruck,mytruck.ID);
        trucksGarage.Add(mytruck.ID);
        //GetTruckFromGarage(0);
        Distribution.totalProfit -= mytruck.price;
    }

    public void GetTruckFromGarage(int index)
    {
        if (trucksList.Count >= truckNumMax)
        {
            trucksList.Add(GetTruck(trucksGarage[index]));
        }

        List<int> tmp = new List<int>();
        for (int i = 0; i < trucksGarage.Count; i++)
        {
            if (trucksGarage[i] != 0)
            {
                tmp.Add(trucksGarage[i]);
            }
        }
        trucksGarage.Clear();
        for (int i = 0; i < tmp.Count; i++)
        {
            trucksGarage.Add(tmp[i]);
        }
    }

    void GiveTheTruckPara(Truck _truck,int id)
    {
        _truck.row = TruckData.GetWidth(id);
        _truck.column = TruckData.GetHeight(id);
        _truck.consume = TruckData.GetConsume(id);
        _truck.price = TruckData.GetPrice(id);
        _truck.skillID = TruckData.GetSkillId(id);
    }

    public static void TruckSkill(Truck _truck)
    {
        int index = _truck.ID;
        if (index >= 6)
        {
            switch (index)
            {
                case 6:
                    Skill01(_truck);
                    break;
                case 7:
                    Skill02(_truck);
                    break;
                case 8:
                    Skill03(_truck);
                    break;
                case 9:
                    Skill04(_truck);
                    break;
            }
        }
    }

    static void Skill01(Truck _truck)
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

    static void Skill04(Truck _truck)
    {
        if (_truck.state == "dist")
        {
            int move = Random.Range(1, 101);
            if (move <= 50)
            {
                if (_truck.stopTime == 0)
                {
                    _truck.remain--;
                    float unitShift = 0;
                    unitShift = 1300.0f / (float)_truck.TotalTimecast;
                    float xPos = _truck.transform.localPosition.x + unitShift;
                    float yPos = _truck.transform.localPosition.y;
                    _truck.transform.localPosition = new Vector3(xPos, yPos, 0);
                }
                else
                {
                    _truck.stopTime--;
                }
            }
            if (move <= 10)
            {
                _truck.stopTime += 1;
            }
        }       
    }

    Truck GetTruck(int id)
    {
        Truck truck = new Truck();
        truck.consume = TruckData.GetConsume(id);
        truck.column = TruckData.GetWidth(id);
        truck.row = TruckData.GetHeight(id);
        truck.price = TruckData.GetPrice(id);
        truck.skillID = TruckData.GetSkillId(id);
        return truck;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
