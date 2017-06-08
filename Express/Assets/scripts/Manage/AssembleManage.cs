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
        for (int i = 0; i < 20; i++)
        {
            Slot slot = Instantiate(_slot, _slotPanel);
            slotList.Add(slot);
        }
    }

    public void Assemble(int truckNum) //装配界面
    {
        Truck _truck = tManage.trucksList[truckNum];

        _slotPanel.GetComponent<GridLayoutGroup>().constraintCount = _truck.column;

        int count = _truck.row * _truck.column;

        for (int i = 0; i < count; i++)
        {
            Slot slot = Instantiate(_slot,_slotPanel);
            slotList.Add(slot);
        }
        currentTruck = truckNum;
        _truck._ordersList.Clear();
    }

	// Update is called once per frame
	void Update () {
		
	}
}
