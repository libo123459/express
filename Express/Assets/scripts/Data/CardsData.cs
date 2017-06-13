using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsData : MonoBehaviour {
    public List<Card> CardsList = new List<Card>();
    
    public string[][] Array;
    private void Awake()
    {
        
        //读取csv二进制文件  
        TextAsset binAsset = Resources.Load("CardData", typeof(TextAsset)) as TextAsset;

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
    
    public string GetDestination(int nRow)
    {
        if (Array.Length <= 0 || nRow >= Array.Length)
            return "";

        return Array[nRow][1];
    }

    public string GetTimeCast(int nRow)
    {
        return Array[nRow][2];
    }
    // Use this for initialization
    void Start () {
               
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
