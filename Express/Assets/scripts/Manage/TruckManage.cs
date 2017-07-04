using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckManage : MonoBehaviour {
    public Truck _truck;
    public List<Truck> trucksList = new List<Truck>();
    public Transform truckPanel;
    public GameObject truck_display_inshop;
    public TruckData tData;

	// Use this for initialization
	void Start ()
    {
        tData = this.GetComponent<TruckData>();
        for (int i = 0; i < 3; i++)/////////临时
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
        for (int i = 0; i < tData.truckList.Count; i++)
        {
            GameObject _truckInshop = Instantiate(truck_display_inshop, shopPanel.GetChild(0).transform);
        }
    }

    public void buyTheTruck(Truck truck)
    {
        this.trucksList.Add(truck);
    }

    public void GiveTheTruckPara(Truck _truck,int id)
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
