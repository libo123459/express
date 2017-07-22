using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class image : MonoBehaviour {
    Material mat;
    private void OnTriggerEnter(Collider collision)
    {
        collision.transform.DOScale(0.2f,0.2f);
        // mat = collision.GetComponent<MeshRenderer>().material;
        //collision.GetComponent<MeshRenderer>().material = Resources.Load<Material>("1");
    }
    private void OnTriggerExit(Collider collision)
    {
        collision.transform.DOScale(0f,0.2f);
        //collision.GetComponent<MeshRenderer>().material = mat;
    }

    private void OnTriggerStay(Collider collision)
    {
        //collision.GetComponent<MeshRenderer>().material = Resources.Load<Material>("1");
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.localRotation *= Quaternion.Euler(0, 30 * Time.deltaTime, 30 * Time.deltaTime);
    }
}
