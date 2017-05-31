using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    public int consume;//油耗
    public List<Block> BlockList = new List<Block>(); //物品分几块的list
    public int BlockNum; //块的数量
    public string Property; //物品特征
    public bool allIn; //所有块都在车库
    public Vector3 startPos;
	// Use this for initialization
	void Start ()
    {
        BlockNum = this.transform.childCount;
        for (int i = 0; i < this.transform.childCount; i++)
        {
            GameObject block = this.transform.GetChild(i).gameObject;
            BlockList.Add(block.GetComponent<Block> ());
        }
        startPos = this.transform.position;
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
