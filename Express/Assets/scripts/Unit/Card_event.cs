using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card_event : Card {
    public int CountDown;
    public int eventID;
	public string type;
	public string name;

	public Button use;

	public EventManage eManage;
	// Use this for initialization
	void Awake () {
		cManage = GameObject.Find("Manage").GetComponent<CardsManage>();
		eManage = GameObject.Find("Manage").GetComponent<EventManage>();

		use = this.transform.Find("use").GetComponent<Button>();
		use.onClick.AddListener(()=> eManage.CheckEvent02(eventID));
		use.onClick.AddListener(()=> cManage.DestoryTheCard(this));


		destination = this.transform.GetChild(0).GetComponent<Text>();
		_cancel = this.transform.Find("cancel").GetComponent<Button>();
		_cancel.onClick.AddListener(() => cManage.cancelTheCard(this));

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
