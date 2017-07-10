using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssembleManage : MonoBehaviour {
    
    public Transform _slotPanel;
    public static List<Slot> slotList = new List<Slot>();
    public Slot _slot;
    public static int currentTruck;
    public Button changeDriver;
    DriverManage dManage;
    
	// Use this for initialization
	void Start () {

        dManage = this.GetComponent<DriverManage>();
        currentTruck = 0;
        /*for (int i = 0; i < 12; i++)
        {
            Slot slot = Instantiate(_slot, _slotPanel);
            slotList.Add(slot);
        }*/
        Assemble(0);
    }

    public void Assemble(int truckNum) //装配界面
    {
        Truck _truck = TruckManage.trucksList[truckNum];
        currentTruck = truckNum;
        _truck.state = "assemble";
        _slotPanel.GetComponent<GridLayoutGroup>().constraintCount = _truck.column;

        creatCapicity(_truck);
        _truck.ClearAll(); ///清除truck上已有的订单       
        if (_truck.driver != null)
        {
            DriverManage.displayTheDriverName(_truck.driver);
        }
        else {
            DriverManage.clearTheDriverName();
        }
        
        checkTruckSkill8();

    }

    void creatCapicity(Truck _truck)
    {
        int count = _truck.row * _truck.column;

        for (int i = 0; i < count; i++)
        {
            Slot slot = Instantiate(_slot, _slotPanel);
            slotList.Add(slot);
        }
    }

    void checkTruckSkill8()
    {
        if (TruckManage.trucksList[AssembleManage.currentTruck].ID == 8)
        {
            changeDriver.GetComponent<Button>().interactable = false;
        }
        else
        {
            changeDriver.GetComponent<Button>().interactable = true;
        }
    }
        
// Update is called once per frame
void Update () {
		
	}
}
