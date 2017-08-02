using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Level : MonoBehaviour {
    public int levelID;
    // Use this for initialization
    void GoToLevel(int ID)
    {

    }
	void Start () {
        this.GetComponent<Button>().onClick.AddListener(() => GoToLevel(levelID));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
