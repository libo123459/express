using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DriverManage : MonoBehaviour {
    public GameObject DriverList;//装配界面，选择司机的列表
    public GameObject DriverPanel;//配送界面，司机状态的列表
    public Button currentDriver;
    public Driver _driver;
    public Text portrait;///司机头像暂定为文本

    List<Driver> actived_drivers = new List<Driver>();//已经解锁的司机列表
    private int _driverNum = 3;///司机数量（临时）
    private bool opened;
    private DriverData _driverData;
    private List<int> driver_id = new List<int>();
    private Text text_currentDriver;
    private TruckManage tManage;

    void InstanceDriver()///从司机数据库中实例出可用的司机
    {
        for (int i = 0; i < _driverNum; i++)
        {
            CreateDriverIndex();
            
        }
        for (int i = 0; i < _driverNum; i++)
        {
            int index = driver_id[i];
            Driver myDriver = Instantiate(_driver, DriverList.transform);            
            myDriver.name = _driverData.GetName(index);
            myDriver.price = _driverData.GetPrice(index);
            myDriver.salary = _driverData.GetSalary(index);
            myDriver.skillID = _driverData.GetSkillID(index);
            actived_drivers.Add(myDriver);
        }
    }
    void CreateDriverIndex()
    {
        int SameNum = 0;
        int n = Random.Range(1,5);
        if (driver_id.Count == 0)
        {
            driver_id.Add(n);
        }
        else {
            for (int i = 0; i < driver_id.Count; i++)
            {
                if (n == driver_id[i])
                {
                    SameNum++;
                    break;
                }
            }
            if (SameNum == 0)
            {
                driver_id.Add(n);
            }
            else {
                CreateDriverIndex();
            }
        }        
    }

    public void OpenDriverList()//展开司机列表
    {
        if (opened == false)
        {
            DriverList.SetActive(true);
            
            opened = true;
        } else {
            DriverList.SetActive(false);
            opened = false;
        }
    }

    public void confirmTheDriver(Driver theDriver)
    {
        DriverList.SetActive(false);
        opened = false;
        text_currentDriver.text = theDriver.name;
    }

    public void SendDriverToTruck(Truck thetruck,Driver thedirver)
    {
        thetruck.driverID = thedirver.id;
    }
	// Use this for initialization
	void Start () {
        tManage = this.GetComponent<TruckManage>();
        _driverData = this.GetComponent<DriverData>();
        text_currentDriver = currentDriver.transform.GetChild(0).GetComponent<Text>();
        InstanceDriver();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
