using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DriverManage : MonoBehaviour {
    public GameObject DriverList;//装配界面，选择司机的列表
    public GameObject DriverPanel;//配送界面，司机状态的列表
    public GameObject driver_display_inrecruit;
    public Button currentDriver;
    public Driver _driver;
    public Text portrait;///司机头像暂定为文本

    List<Driver> actived_drivers = new List<Driver>();//已经解锁的司机列表
    private int _driverNum = 3;///司机数量（临时）
    private bool opened;
    
    private List<int> driver_id = new List<int>();
    private static Text text_currentDriver;

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
            myDriver.id = index;
            myDriver.name = DriverData.GetName(index);
            myDriver.price = DriverData.GetPrice(index);
            myDriver.salary = DriverData.GetSalary(index);
            myDriver.skillID = DriverData.GetSkillID(index);
            actived_drivers.Add(myDriver);
        }
    }
    void CreateDriverIndex()
    {
        
        int SameNum = 0;
        int n = Random.Range(6,10);
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
        displayTheDriverName(theDriver);
        SendDriverToTruck(theDriver);
    }

    public static void displayTheDriverName(Driver theDriver)
    {
        text_currentDriver.text = theDriver.name;
    }

    public static void clearTheDriverName()
    {
        text_currentDriver.text = null;
    }

    public void SendDriverToTruck(Driver theDriver)
    {
        TruckManage.trucksList[AssembleManage.currentTruck].driver = theDriver;
        theDriver.truck = TruckManage.trucksList[AssembleManage.currentTruck];

        for (int i = 0; i < actived_drivers.Count; i++)
        {
            actived_drivers[i].GetComponent<Button>().interactable = true;
        }
        for (int i = 0; i < TruckManage.trucksList.Count; i++)
        {
            if (TruckManage.trucksList[i].driver != null)
            {
                TruckManage.trucksList[i].driver.GetComponent<Button>().interactable = false;
            }
        }        
    }

    public void DisplayDriverInRecruitPanel(Transform recruitPanel)
    {
        if (recruitPanel.GetChild(0).transform.childCount == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject _driverInRecruit = Instantiate(driver_display_inrecruit, recruitPanel.GetChild(0).transform);
            }
        }
        for (int i = 0; i < recruitPanel.transform.childCount; i++)
        {
            GameObject _driverInRecuit = recruitPanel.GetChild(0).GetChild(i).gameObject;
            int same = 0;
            for (int j = 0; j < actived_drivers.Count; j++)
            {
                
                if (_driverInRecuit.transform.GetSiblingIndex() + 1 == actived_drivers[j].id)
                {
                    same = 0;
                }
                else {
                    same++;
                }
            }

            if (same == 0)
            {
                _driverInRecuit.transform.GetChild(1).GetComponent<Button>().interactable = true;
            }
            else
            {
                _driverInRecuit.transform.GetChild(1).GetComponent<Button>().interactable = false;
            }
        }
    }

    public void buyTheDriver(GameObject butBtn)//招聘司机
    {
        Driver mydriver = Instantiate(_driver);
        mydriver.transform.SetParent(DriverList.transform);
        mydriver.id = butBtn.transform.GetSiblingIndex() + 1;
        GiveTheDriverPara(mydriver);
        Distribution.totalProfit -= _driver.price;
    }

    void GiveTheDriverPara(Driver _driver)
    {
        _driver.name = DriverData.GetName(_driver.id);
        _driver.price = DriverData.GetPrice(_driver.id);
        _driver.salary = DriverData.GetSalary(_driver.id);
        _driver.skillID = DriverData.GetSkillID(_driver.id);
    }

    public static void DriverSkill(Driver _driver)
    {
        int index = _driver.id;
        if (index >= 6)
        {
            switch (index)
            {
                case 6:
                    Skill01(_driver.truck);
                    break;
                case 7:
                    Skill02(_driver.truck);
                    break;
                case 8:
                    Skill03(_driver.truck);
                    break;
                case 9:
                    Skill04(_driver.truck);
                    break;
            }
        }        
    }

    static void Skill01(Truck truck)
    {
        if (truck.orderNum >= 3)
        {
            for (int i = 0; i < truck.timeCast.Count; i++)
            {
                truck.timeCast[i] -= 1;
            }
            truck.remain -= 3;
            truck.TotalTimecast -= 3;
        }
    }

    static void Skill02(Truck truck)
    {
        for (int i = 0; i < truck.credit.Count; i++)
        {
            truck.credit[i] += 1;
        }
    }

    static void Skill03(Truck truck)
    {
        for (int i = 0; i < truck.orderNum; i++)
        {
            truck.timeCast[i] = 1;
            truck.profit[i] = (int)(truck.profit[i] * 0.1f);
        }
        truck.remain = truck.timeCast.Count;
        truck.TotalTimecast = truck.remain;
    }

    static void Skill04(Truck truck)
    {
        if (truck.blockNum > truck.orderNum)
        {
            for (int i = 0; i < truck.orderNum; i++)
            {
                truck.profit[i] += truck.profit[i] * 0.2f;
                truck.credit[i] += truck.credit[i] * 0.2f;
            }
        }
        else {
            for (int i = 0; i < truck.orderNum; i++)
            {
                truck.profit[i] -= truck.profit[i] * 0.2f;
                truck.credit[i] -= truck.credit[i] * 0.2f;
            }
        }
    }

    private void Awake()
    {
        text_currentDriver = currentDriver.transform.GetChild(0).GetComponent<Text>();
    }
    // Use this for initialization
    void Start () {
        InstanceDriver();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
