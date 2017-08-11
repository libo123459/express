using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckData : MonoBehaviour {
    public static List<List<string>> column = new List<List<string>>();
   
    //public List<Truck> truckList = new List<Truck>();
    
    private void Awake()
    {
        TextAsset binAsset = Resources.Load("capacity", typeof(TextAsset)) as TextAsset;
        //读取每一行的内容  
        string[] lineArray = binAsset.text.Split("\r"[0]);
        //创建二维数组  
        string[][] Array = new string[lineArray.Length][];
        //把csv中的数据储存在二位数组中  
        for (int _i = 0; _i < lineArray.Length; _i++)
        {
            Array[_i] = lineArray[_i].Split(',');
        }
        for (int i = 0; i < lineArray.Length; i++)
        {
            List<string> row = new List<string>();
            for (int j = 0; j < Array[i].Length; j++)
            {
                row.Add(Array[i][j]);
            }
            column.Add(row);
        }
    }

    public int GetTruckID(int nRow)
    {
        return int.Parse(column[nRow][0]);
    }
    public static int GetWidth(int nRow, int truckNum)
    {
        return int.Parse(column[nRow][truckNum]);
    }
    public static int GetHeight(int nRow, int truckNum)
    {
        return int.Parse(column[nRow][truckNum + 3]);
    }
    public static int GetDiceMax(int nRow, int level)
    {
        return int.Parse(column[nRow][level + 6]);
    }
    public static int GetDiceMin(int nRow, int level)
    {
        return int.Parse(column[nRow][level + 9]);
    }
    public static int GetSkillID(int nRow)
    {
        return int.Parse(column[nRow][13]);
    }
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
