using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BTN_truckChoose : MonoBehaviour {
    public int id;
    public EventManage eManage;
	// Use this for initialization
	void Start () {
        
        this.GetComponent<Button>().onClick.AddListener(()=>eManage.ChooseTruck_Event02_1(id));
	}
    public void destory()
    {
        DestroyImmediate(this);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
