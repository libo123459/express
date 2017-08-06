using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsData : MonoBehaviour {
    public List<Card> CardsList = new List<Card>();

    //public string[][] Array;
    //public List<string> row = new List<string>();
    public List<List<string>> column = new List<List<string>>();
    ItemData _itemData;
    private void Awake()
    {
        LoadCardDataFile(0);
        _itemData = this.GetComponent<ItemData>();
     
    }

    public void LoadCardDataFile(int level)
    {
        column.Clear();
        //读取csv二进制文件  
        TextAsset binAsset = Resources.Load("CardData" + level.ToString(), typeof(TextAsset)) as TextAsset;
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
    
    public string GetDestination(int nRow)
    {
        //if (Array.Length <= 0 || nRow >= Array.Length)
          //  return "";

        return column[nRow][1];
    }

    public int GetTimeCast(int nRow)
    {
        return int.Parse(column[nRow][2]);
    }

    public Item GetItem(int nRow)
    {
        int blockNum = int.Parse(column[nRow][3]);
        int itemindex = int.Parse(column[nRow][4]);
        return _itemData.ItemsList[blockNum-1][itemindex-1];
    }
    public int GetSkillID(int nRow)
    {
       
        int index = int.Parse(column[nRow][6]);
        
        return index;
    }

    public string GetSkillDes(int nRow)
    {
        return column[nRow][7];
    }

    public int GetStageID(int nRow)
    {
        return int.Parse(column[nRow][5]);
    }
    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
