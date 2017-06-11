using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    public int consume;//油耗
    //public int timeCast;//耗时
    public List<Block> BlockList = new List<Block>(); //物品分几块的list
    public int BlockNum; //块的数量
    public string Property; //物品特征
    public Vector3 SlotPos;
    public Vector3 StartPos;
    public new Collider2D collider = null;

    // Use this for initialization
    void Start ()
    {
        StartPos = this.transform.position;
        BlockNum = this.transform.childCount;
        for (int i = 0; i < this.transform.childCount; i++)
        {
            GameObject block = this.transform.GetChild(i).gameObject;
            BlockList.Add(block.GetComponent<Block> ());
        }
   	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collider = collision;
        if (collision.gameObject.tag == "slot")
        {
            SlotPos = collision.transform.position;
        }
        if (collision.gameObject.tag == "card")
        {
            StartPos = collision.transform.position;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
