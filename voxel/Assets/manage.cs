using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manage : MonoBehaviour {
    public GameObject block;
    public Transform grid;
    public List<GameObject> blocklist = new List<GameObject>();
    float coe = 0.2f;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                for (int k = 0; k < 20; k++)
                {
                    GameObject _block = Instantiate(block,grid);
                    _block.transform.localPosition = new Vector3(coe * i,coe * j,coe * k);
                    blocklist.Add(_block);
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
