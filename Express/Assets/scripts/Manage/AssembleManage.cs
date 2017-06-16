using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssembleManage : MonoBehaviour {
    
    public Transform _slotPanel;
    public List<Slot> slotList = new List<Slot>();
    public Slot _slot;
    public int currentTruck;
    TruckManage tManage;
    
	// Use this for initialization
	void Start () {
        tManage = this.GetComponent<TruckManage>();
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
        Truck _truck = tManage.trucksList[truckNum];
        currentTruck = truckNum;
        _truck.state = "assemble";

        _slotPanel.GetComponent<GridLayoutGroup>().constraintCount = _truck.column;

        int count = _truck.row * _truck.column;

        for (int i = 0; i < count; i++)
        {
            Slot slot = Instantiate(_slot,_slotPanel);
            slotList.Add(slot);
        }
        
        _truck.ClearAll(); ///清除truck上已有的订单
    }

	// Update is called once per frame
	void Update () {
		
	}
}
