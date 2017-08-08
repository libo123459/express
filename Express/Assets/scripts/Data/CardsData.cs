using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsData : MonoBehaviour {
    public List<Card> CardsList = new List<Card>();
    
    public List<List<string>> column = new List<List<string>>();
    List<string> SkillDes = new List<string>();
    ItemData _itemData;
    private void Awake()
    {
        LoadCardDataFile(0);
        IntiSkillDesList();
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
    
    public int GetTimeCast(int nRow)
    {
        return int.Parse(column[nRow][1]);
    }

    public Item GetItem(int nRow)
    {
        int blockNum = int.Parse(column[nRow][2]);
        int itemindex = int.Parse(column[nRow][3]);
        return _itemData.ItemsList[blockNum-1][itemindex-1];
    }
    public int GetStageID(int nRow)
    {
        return int.Parse(column[nRow][4]);
    }
    public int GetSkillID(int nRow)
    {       
        int index = int.Parse(column[nRow][5]);
        
        return index;
    }
    public string GetSkillDes(int skill)
    {
        if (skill > 0)
        {
            return SkillDes[skill - 1];
        }
        else {
            return "";
        }
        
    }

    void IntiSkillDesList()
    {
        string skill1 = "该次运输额外增加一点信用值";
        string skill2 = "该次运输不增加信用值";
        string skill3 = "与其他货物一起运输增加一回合";
        string skill4 = "与其他货物一起运输减少一回合";
        string skill5 = "单独运输增加一回合";
        string skill6 = "单独运输减少一回合";
        string skill7 = "运输成功则增加10业务点，减少5信用";
        string skill8 = "运输期间行动点数保持最大";
        string skill9 = "运输期间行动点数保持最小";
        string skill10 = "运输超过5回合则减少5信用";
        string skill11 = "运输成功获得业务点数翻倍";
        string skill12 = "每过一回合该货物增加一回合运输时间，最多加3";
        string skill13 = "每过一回合该货物减少一回合运输时间，最少到1";
        string skill14 = "每成功运输一个货物，该货物减少一回合运输时间";
        string skill15 = "运输成功则信用值恢复到最大值";
        string skill16 = "运输成功则信用值恢复到降至1";
        SkillDes.Add(skill1);
        SkillDes.Add(skill2);
        SkillDes.Add(skill3);
        SkillDes.Add(skill4);
        SkillDes.Add(skill5);
        SkillDes.Add(skill6);
        SkillDes.Add(skill7);
        SkillDes.Add(skill8);
        SkillDes.Add(skill9);
        SkillDes.Add(skill10);
        SkillDes.Add(skill11);
        SkillDes.Add(skill12);
        SkillDes.Add(skill13);
        SkillDes.Add(skill14);
        SkillDes.Add(skill15);
        SkillDes.Add(skill16);
    }
    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
