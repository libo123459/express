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

    void InstanceDriver()///初始开始的司机
    {
        driver_id.Clear();
        for (int i = 0; i < _driverNum; i++)
        {
            CreateDriverIndex(1,6);            
        }
        for (int i = 0; i < _driverNum; i++)
        {
            int index = driver_id[i];
            Driver myDriver = Instantiate(_driver, DriverList.transform);
            myDriver.id = index;
            GiveTheDriverPara(myDriver);
            actived_drivers.Add(myDriver);
        }
    }
    void CreateDriverIndex(int start,int end)
    {
        int SameNum = 0;
        int n = Random.Range(start,end);
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
                CreateDriverIndex(start,end);
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

    public void confirmTheDriver(Driver theDriver)//选择司机
    {
        DriverList.SetActive(false);
        opened = false;
        displayTheDriverName(theDriver);
        SendDriverToTruck(theDriver);
    }

    public static void displayTheDriverName(Driver theDriver)//当前司机名字
    {
        text_currentDriver.text = theDriver.name;
    }

    public static void clearTheDriverName()//清除
    {
        text_currentDriver.text = null;
    }

    public void SendDriverToTruck(Driver theDriver)///司机数据传到卡车上
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

    public void DisplayDriverInRecruitPanel(Transform recruitPanel) //招聘市场司机列表
    {
        driver_id.Clear();        

        if (recruitPanel.GetChild(0).transform.childCount < 3)
        {
            int n = 3 - recruitPanel.GetChild(0).transform.childCount;
            for (int i = 0; i < n; i++)
            {
                CreateRecuritDriverIndex(1, 10);
            }

            for (int i = 0; i < driver_id.Count; i++)
            {
                GameObject _driverInRecruit = Instantiate(driver_display_inrecruit, recruitPanel.GetChild(0).transform);
                
                buyDriver bdri = _driverInRecruit.GetComponent<buyDriver>();
                bdri.id = driver_id[i];
                _driverInRecruit.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = DriverData.GetName(bdri.id) 
                    + " " + DriverData.GetPrice(bdri.id);
                print(driver_id[i]);
            }
        }
        driver_id.Clear();
    }

    void CreateRecuritDriverIndex(int start, int end)
    {
        int SameNum = 0;
        int n = Random.Range(start, end);
        for (int i = 0; i < actived_drivers.Count; i++)
        {
            if (n == actived_drivers[i].id)
            {
                SameNum++;
                break;
            }
        }
        if (SameNum == 0)
        {
            driver_id.Add(n);
        }
        else
        {
            CreateRecuritDriverIndex(start, end);
        }
    }

    public void buyTheDriver(GameObject butBtn)//招聘司机
    {
        Driver mydriver = Instantiate(_driver);
        mydriver.transform.SetParent(DriverList.transform);
        mydriver.id = butBtn.GetComponent<buyDriver>().id;
        GiveTheDriverPara(mydriver);
        actived_drivers.Add(mydriver);
        Distribution.totalProfit -= _driver.price;
        Destroy(butBtn.gameObject);
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
