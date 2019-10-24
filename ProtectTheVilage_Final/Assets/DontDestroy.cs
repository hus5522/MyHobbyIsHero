using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour {

    public string score;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
