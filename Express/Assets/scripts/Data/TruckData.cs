using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckData : MonoBehaviour {
    public List<Truck> truckList = new List<Truck>();
    public string[][] Array;
    // Use this for initialization
    private void Awake()
    {
        //读取csv二进制文件  
        TextAsset binAsset = Resources.Load("TruckData", typeof(TextAsset)) as TextAsset;

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

    public int GetWidth(int nRow)
    {
        //if (Array.Length <= 0 || nRow >= Array.Length)
        //   return "";
        
        return int.Parse(Array[nRow][1]);
    }

    public int GetHeight(int nRow)
    {
        return int.Parse(Array[nRow][2]);
    }

    public int GetConsume(int nRow)
    {
        return int.Parse(Array[nRow][3]);
    }

    public int GetPrice(int nRow)
    {
        return int.Parse(Array[nRow][4]);
    }

    public int GetSkillId(int nRow)
    {
        return int.Parse(Array[nRow][5]);
    }

    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
