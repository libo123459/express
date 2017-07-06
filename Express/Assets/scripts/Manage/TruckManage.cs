using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TruckManage : MonoBehaviour {
    public Truck _truck;
    //public GameObject truck_image;
    public static List<Truck> trucksList = new List<Truck>();
    public static List<Truck> trucksGarage = new List<Truck>();
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
	}

    public void DisplayTruckInShopPanel(Transform shopPanel)
    {
        if (shopPanel.GetChild(0).transform.childCount == 0)
        {
            for (int i = 0; i < 5; i++)//前5个车
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
        trucksGarage.Add(mytruck);
        GetTruckFromGarage(0);
        Distribution.totalProfit -= mytruck.price;
    }

    public void GetTruckFromGarage(int index)
    {
        if (trucksList.Count >= truckNumMax)
        {
            trucksList.Add(trucksGarage[index]);
        }

        List<Truck> tmp = new List<Truck>();
        for (int i = 0; i < trucksGarage.Count; i++)
        {
            if (trucksGarage[i] != null)
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
        _truck.row = tData.GetWidth(id);
        _truck.column = tData.GetHeight(id);
        _truck.consume = tData.GetConsume(id);
        _truck.price = tData.GetPrice(id);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
