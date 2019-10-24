using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HLGeneration : MonoBehaviour {
    public static HLGeneration instance;
    public Transform[] HLArr;

    public GameObject Homeless;

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
        check = false;
        Count = 0;
        Homeless = Resources.Load<GameObject>("Prefabs/Homeless");
        CreateTimeInterval = 10;
        timming = 0;

        HLArr = new Transform[6];

        for (int i = 0; i < HLArr.Length; i++)
        {
            HLArr[i] = transform.Find((i + 1).ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        timming += Time.deltaTime;
        if (timming >= CreateTimeInterval)
        {
            //check = true;
            while(Count<=5)
            {
                int tempidx = Random.Range(0, 6);
                if (HLArr[tempidx].Find("Homeless(Clone)") == false)
                {
                    //생성
                    Instantiate(Homeless, HLArr[tempidx]);
                    timming = 0;
                    Count++;
                    UIManager.instance.SafetySpeed += 0.05f;
                    break;
                   // check = false;
                }
                else //중복된다면.
                {
                    Debug.Log("노숙자 중복");
                    continue;
                }
            }

        }


    }
}
