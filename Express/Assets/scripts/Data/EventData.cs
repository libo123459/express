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
        string[] _name = new string[] {"大风","前方封路","大雪","恐怖袭击","精力充沛"};
        for (int i = 0; i < _name.Length; i++)
        {
            namelist.Add(_name[i]);
        }
    }

    public void influnce(string _name)
    {

    }

	// Update is called once per frame
	void Update () {
		
	}
}
