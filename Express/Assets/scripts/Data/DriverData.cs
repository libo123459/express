using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverData : MonoBehaviour {
    
    public List<Driver> driverList = new List<Driver>();
    
	// Use this for initialization
	
    public string[][] Array;
    // Use this for initialization
    private void Awake()
    {
        //读取csv二进制文件  
        TextAsset binAsset = Resources.Load("DriverData", typeof(TextAsset)) as TextAsset;

        //读取每一行的内容  
        string[] lineArray = binAsset.text.Split("\r"[0]);

        //创建二维数组  
        Array = new string[lineArray.Length][];

        //把csv中的数据储存在二位数组中  
        for (int _i = 0; _i < lineArray.Length; _i++)
        {
            Array[_i] = lineArray[_i].Split(',');
        }


    }

    public string GetName(int nRow)
    {
        //if (Array.Length <= 0 || nRow >= Array.Length)
        //   return "";

        return Array[nRow][1];
    }

    public int GetPrice(int nRow)
    {
        return int.Parse(Array[nRow][2]);
    }

    public int GetSalary(int nRow)
    {
        return int.Parse(Array[nRow][3]);
    }

    public int GetSkillID(int nRow)
    {
        return int.Parse(Array[nRow][4]);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
