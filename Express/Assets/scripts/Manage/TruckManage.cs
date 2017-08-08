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

    

    void GiveTheTruckPara(Truck _truck,int id)
    {
        _truck.row = 3;//TruckData.GetWidth(id);
        _truck.column = 3;//TruckData.GetHeight(id);
        //_truck.consume = 1;//TruckData.GetConsume(id);
        //_truck.price = TruckData.GetPrice(id);
       // _truck.skillID = TruckData.GetSkillId(id);
    }

    public static void TeamSkill(Truck _truck)
    {        
        
    }
    
    // Update is called once per frame
    void Update () {
		
	}
}
