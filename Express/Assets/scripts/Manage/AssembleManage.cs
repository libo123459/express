using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssembleManage : MonoBehaviour {
    
    public Transform _slotPanel;
    public static List<Slot> slotList = new List<Slot>();
    public Slot _slot;
    public static int currentTruck;
       
	// Use this for initialization
	void Start () {

        
        currentTruck = 0;
        
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
        
// Update is called once per frame
void Update () {
		
	}
}
