using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Level : MonoBehaviour {
    public int levelID;
    CardsData _cardsdata;
    // Use this for initialization
    void GoToLevel()
    {
        _cardsdata.LoadCardDataFile(this.levelID);
    }
	void Start () {
        this.GetComponent<Button>().onClick.AddListener(GoToLevel);
        _cardsdata = GameObject.Find("Manage").GetComponent<CardsData>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
