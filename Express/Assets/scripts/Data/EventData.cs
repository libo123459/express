using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventData : MonoBehaviour {
    public List<string> namelist = new List<string>();
	// Use this for initialization
	void Start () {
        GiveEventNames();
	}

    void GiveEventNames()
    {
        string[] _name = new string[] { "购物狂欢节", "大雪", "信用严打周", "这货送不了",
            "信用锦旗", "光速驾驶", "交通堵塞通知", "下周大甩卖",
            "合理搭配", "大量搬运", "据单有理" };
        for (int i = 0; i < _name.Length; i++)
        {
            namelist.Add(_name[i]);
        }
    }

    
	// Update is called once per frame
	void Update () {
		
	}
}
