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
