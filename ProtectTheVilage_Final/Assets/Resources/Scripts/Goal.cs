using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            string[] originOB = transform.name.Split('G');
            Debug.Log(originOB[0]);


            LostGeneration.instance.Count--;
            UIManager.instance.SafetySpeed -= 0.05f;

            UIManager.instance.PersonSolution();
            UIManager.instance.score += 100;
            UIManager.instance.ScoreText.text = "" + UIManager.instance.score;

            Destroy(GameObject.Find("LostPoint").transform.Find(originOB[0]).Find("Lost(Clone)").gameObject);

            transform.GetComponent<BoxCollider2D>().enabled = false;
            transform.Find("New Sprite").GetComponent<SpriteRenderer>().enabled = false;

            LostGeneration.instance.Count--;
        }
    }
}
