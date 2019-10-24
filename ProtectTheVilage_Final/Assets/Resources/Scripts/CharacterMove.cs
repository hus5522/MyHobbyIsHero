using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour {

    public float MovePwr;
    

    public Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        MovePwr = 30.0f;
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		

	}

    void FixedUpdate()
    {
        if (CharControl.instance.IsAction)
            return;

        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);

        rb.AddForce(movement * MovePwr);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag =="PARK")
        {
            //Debug.Log("enter");
        }
        else if(other.tag =="ENEMY")
        {
            //Debug.Log("ENEMY");
        }

        else if (other.tag == "HOMELESS")
        {
            //Debug.Log("HOMELESS");
            //other.gameObject.GetComponentInChildren<ParticleSystem>().Stop();
        }
        else if (other.tag == "DRUNKEN")
        {
            //Debug.Log("drunken");
            //CharControl.instance.ApState = CharControl.Char_State.drunken;
            //other.gameObject.GetComponentInChildren<ParticleSystem>().Stop();
        }

    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "PARK")
        {
            //Debug.Log("Stay");
            MovePwr = 10.0f;
        }
        if (other.tag == "DRUNKEN")
        {
            //Debug.Log("drunken");
            CharControl.instance.ApState = CharControl.Char_State.drunken;

            if (Input.GetKeyDown(KeyCode.LeftShift)&& !Police.instance.IsCall)
            {
                CharControl.instance.SolDrunken(other);
            }
            
        }
        else if(other.tag == "HOMELESS")
        {
            //Debug.Log("homeless");
            CharControl.instance.ApState = CharControl.Char_State.Homeless;
            if (Input.GetKeyDown(KeyCode.LeftShift) && !Police.instance.IsCall)
            {
                //Debug.Log("길잃은 사람222");
                CharControl.instance.SolHomeless(other);
            }
        }
        else if (other.tag == "LOST")
        {
            Debug.Log("lost");
            CharControl.instance.ApState = CharControl.Char_State.lost;

            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                //Debug.Log("길잃은 사람222");
                CharControl.instance.FindWay(other);
            }
        }

    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "PARK")
        {
            Debug.Log("exit");
            MovePwr = 30.0f;
        }
        CharControl.instance.ApState = CharControl.Char_State.idle;
    }
}
