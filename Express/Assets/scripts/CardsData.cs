using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsData : MonoBehaviour {
    public List<Card> CardsList = new List<Card>();
    public List<string> destinations = new List<string>();

    private void Awake()
    {
        string a = "beijing";
        string b = "shanghai";
        string c = "london";
        string d = "NewYork";

        string e = "Shenyang";
        string f = "Hangzhou";
        string g = "Tokyo";
        string h = "Wuhan";

        string i = "Dalian";
        string j = "Pari";
        destinations.Add(a);
        destinations.Add(b);
        destinations.Add(c);
        destinations.Add(d);
        destinations.Add(e);
        destinations.Add(f);
        destinations.Add(g);
        destinations.Add(h);
        destinations.Add(i);
        destinations.Add(j);
    }
    // Use this for initialization
    void Start () {
               
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
