using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour {
    public List<Item> ItemsList1 = new List<Item>();
    public List<Item> ItemsList2 = new List<Item>();
    public List<Item> ItemsList3 = new List<Item>();
    public List<Item> ItemsList4 = new List<Item>();
    public List<List<Item>> ItemsList = new List<List<Item>>();
    // Use this for initialization
    void Start () {
        ItemsList.Add(ItemsList1);
        ItemsList.Add(ItemsList2);
        ItemsList.Add(ItemsList3);
        ItemsList.Add(ItemsList4);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
