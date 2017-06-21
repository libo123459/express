using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverManage : MonoBehaviour {
    public GameObject DriverList;//装配界面，选择司机的列表
    public GameObject DriverPanel;//配送界面，司机状态的列表
    public DriverData _driverData;
    public Driver _driver;

    private int _driverNum;///司机数量（临时）
    private bool opened;

    void InstanceDriver()
    {
        for (int i = 0; i < _driverNum; i++)
        {
            GameObject myDriver = Instantiate(_driver.gameObject, DriverList.transform);
        }
    }

    public void OpenDriverList()//展开司机列表
    {
        if (opened == false)
        {
            DriverList.SetActive(true);
            
            opened = true;
        }        
    }
    public void CloseDriverList()
    {
        if (opened == true)
        {
            DriverList.SetActive(false);
            opened = false; 
        }       
    }

    public void DisplayTheDriver(Truck _truck)
    {

    }

    public void confirmTheDriver()
    {

    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
