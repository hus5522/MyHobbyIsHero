using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : MonoBehaviour {
    public static Police instance;

    Vector3 tmpvec3;
    public bool IsCall;
    public int Carspeed;
    public Transform tr;
	// Use this for initialization
	void Start () {
        if (instance == null)
            instance = this;

        IsCall = false;
        Carspeed = 10;

        tr = this.transform;

       // tr.SetPositionAndRotation(new Vector3(-9.6f, 2.0f, 0), Quaternion.identity);
        tr.localPosition = new Vector3(-9.6f, 2.0f, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
        if(IsCall)
        {
            tr.Translate(Vector3.right * Time.deltaTime*Carspeed);

        }
	}
    
    public void CallPolice(Vector3 PersonPosition)
    {
        tmpvec3 = PersonPosition;
        tr.localPosition = new Vector3(-10.0f, PersonPosition.y, 0.0f);
        

        Invoke("StartCar", 2.1f);
    }

    public void StartCar()
    {
        //IsCall = true;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "PoliceCarColl")
        {
            IsCall = false;
            SoundMng.instance.Clearsound();
            Debug.Log("경찰차 도착");
        }

        if (other.tag == "HOMELESS")
        {
            Debug.Log("노숙자 대려감");
            HLGeneration.instance.Count--;
            UIManager.instance.SafetySpeed -= 0.05f;
            UIManager.instance.PersonSolution();
            //SoundMng.instance.Clearsound();
            SoundMng.instance.CarDoorSound();

            UIManager.instance.score += 30;
            UIManager.instance.ScoreText.text = "" + UIManager.instance.score;

            Destroy(other.gameObject);
        }
        else if(other.tag == "DRUNKEN")
        {
            Debug.Log("취객 대려감");
            DrunkenGeneration.instance.Count--;
            UIManager.instance.SafetySpeed -= 0.05f;
            UIManager.instance.PersonSolution();
            //SoundMng.instance.Clearsound();
            SoundMng.instance.CarDoorSound();

            UIManager.instance.score += 30;
            UIManager.instance.ScoreText.text = "" + UIManager.instance.score;

            Destroy(other.gameObject);
        }
    }
}
