using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrunkenGeneration : MonoBehaviour {

    public static DrunkenGeneration instance;
    public Transform[] DrunkArr;

    public GameObject Drunken;

    public float timming;
    public int Count;
    public float CreateTimeInterval;
    public float CreateTime;

    public bool check;

    // Use this for initialization
    void Start()
    {

        if (instance == null)
            instance = this;
        Count = 0;
        check = false;
        Drunken = Resources.Load<GameObject>("Prefabs/Drunken");
        CreateTimeInterval = 10;
        timming = 0;

        DrunkArr = new Transform[9];

        for (int i = 0; i < DrunkArr.Length; i++)
        {
            DrunkArr[i] = transform.Find((i + 1).ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        timming += Time.deltaTime;
        if (timming >= CreateTimeInterval)
        {
            //check = true;
            while (Count<=8)
            {
                
                int tempidx = Random.Range(0, 9);
                if (DrunkArr[tempidx].Find("Drunken(Clone)") == false)
                {
                    //생성
                    Debug.Log("취객생성");
                    Instantiate(Drunken, DrunkArr[tempidx]);
                    timming = 0;
                    Count++;
                    UIManager.instance.SafetySpeed += 0.05f;
                    break;
                    //check = false;
                }
                else //중복된다면.
                {
                    Debug.Log("취객중복");
                    continue;
                }
            }

        }


    }
}
