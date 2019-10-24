using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class des : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.Find("Score_i").GetComponent<Text>().text = GameObject.Find("dontdestroy").GetComponent<DontDestroy>().score;

        Destroy(GameObject.Find("dontdestroy"));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
