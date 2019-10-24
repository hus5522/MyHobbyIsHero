using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharControl : MonoBehaviour {
    public enum Char_State
    {
        idle = 0,
        Homeless,
        drunken,
        theft,
        lost,
        stalker

    };

    public static CharControl instance;

    public Char_State ApState;
    public bool IsAction;
    // Use this for initialization
    void Start () {
        if (instance == null)
            instance = this;

        IsAction = false;
        ApState = Char_State.idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)&& !IsAction)
        {
            Debug.Log("쉬프트 누름");
            ShiftPressed();
        }
    }

    public void ShiftPressed()
    {

    }
    //길잃은 사람 부분
    public void FindWay(Collider2D other)
    {
        Debug.Log("FindWay");
        other.GetComponent<SpringJoint2D>().enabled = true;
        other.GetComponent<SpringJoint2D>().connectedBody = GameObject.Find("Character").GetComponent<Rigidbody2D>();


        int Goalidx = Random.Range(0, 7);
        //Debug.Log(other.transform.parent.name + "G");
        GameObject.Find(other.transform.parent.name + "G").GetComponent<BoxCollider2D>().enabled = true;
        GameObject.Find(other.transform.parent.name + "G").transform.Find("New Sprite").GetComponent<SpriteRenderer>().enabled = true;
        //other.GetComponentInParent<Transform>().name
    }
    //길잃은 사람 부분



    //취객 부분
    public void SolDrunken(Collider2D other)
    {
        IsAction = true;
        SoundMng.instance.PhoneRing();
        Police.instance.CallPolice(other.transform.parent.localPosition);
        Invoke("Cleardrunken", 2.1f);
        Invoke("ClearSound", 2.1f);
        Debug.Log("취객 해결");

    }
    public void Cleardrunken()
    {
        IsAction = false;
        Police.instance.IsCall = true;
    }
    //취객 부분

    //노숙자 부분
    public void SolHomeless(Collider2D other)   //전화 거는 부분
    {
        IsAction = true;
        SoundMng.instance.PhoneRing();
        Police.instance.CallPolice(other.transform.parent.localPosition);
        Invoke("Clearhomeless", 2.1f);
        Invoke("ClearSound", 2.1f);
    }
    public void Clearhomeless()
    {
        IsAction = false;
        Police.instance.IsCall = true;
        
        
    }
    //노숙자 부분


    //클리어 효과음
    public void ClearSound()
    {
        //SoundMng.instance.Clearsound();
    }
    
    
}
