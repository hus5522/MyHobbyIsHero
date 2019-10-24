using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameMng : MonoBehaviour
{
    public static GameMng instance;

    public GameObject Plane;
    public GameObject Fire;

    public GameObject Areapoint;

    public float planetimming;
    public float planeinternval;

    public float Timming;
    public int FireCnt;

    public float FireIntervaltime;
    public float FireAddtime;   //랜덤성있게 간격범위있는 변수
    // Use this for initialization
    void Start()
    {
        if (instance == null)
            instance = this;

        FireIntervaltime = 5.0f;
        planeinternval = 30.0f;
        planetimming = 0;
        FireCnt = 0;
        FireAddtime = Random.Range(0, 3.0f);


        Fire = Resources.Load<GameObject>("H_Prefabs/Fire");
        Plane = GameObject.Find("Plane");
        Areapoint = GameObject.Find("AreaPoint");
        
    }


    public void FireCreate()
    {
        bool check = true;


        while (check && FireCnt <= 20)
        {
            int tmpidx = Random.Range(0, 27);
            if (Areapoint.GetComponent<BuildGeneration>().PointArr[tmpidx].tag == "HOME" &&
                Areapoint.GetComponent<BuildGeneration>().PointArr[tmpidx].Find("Fire(Clone)") == false)
            {
                Instantiate(Fire, Areapoint.GetComponent<BuildGeneration>().PointArr[tmpidx]);

                if(!SoundMng.instance.IsPlayingFire)
                    SoundMng.instance.Fire();

                Areapoint.GetComponent<BuildGeneration>().PointArr[tmpidx].Find("Fire(Clone)").tag = "Fire";
                check = false;
                FireAddtime = Random.Range(0, 3.0f);
                Timming = 0;
                FireCnt++;
            }
            else
            {
                continue;
            }

        }
    }
    // Update is called once per frame
    void Update()
    {


        Timming += Time.deltaTime;

        planetimming += Time.deltaTime;
        if (Timming >= FireIntervaltime + FireAddtime)    //몇초 간격마다 집에 불남
        {
            bool check = true;
            
            while (check&& FireCnt <=20)
            {
                int tmpidx = Random.Range(0, 27);
                if (Areapoint.GetComponent<BuildGeneration>().PointArr[tmpidx].tag == "HOME"&& 
                    Areapoint.GetComponent<BuildGeneration>().PointArr[tmpidx].Find("Fire(Clone)")==false )
                {
                    Instantiate(Fire, Areapoint.GetComponent<BuildGeneration>().PointArr[tmpidx]);
                    Areapoint.GetComponent<BuildGeneration>().PointArr[tmpidx].Find("Fire(Clone)").tag = "Fire";

                    if (!SoundMng.instance.IsPlayingFire)
                        SoundMng.instance.Fire();

                    check = false;
                    FireAddtime = Random.Range(0, 3.0f);
                    Timming = 0;
                    FireCnt++;
                }
                else
                {
                    continue;
                }

            }
        }

        if(planetimming>=planeinternval)
        {
            planetimming = 0;
            planeinternval = 50.0f;
            Plane.GetComponent<PlayableDirector>().Play();
        }


        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            FireCreate();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Plane.GetComponent<PlayableDirector>().Play();
        }
    }
}
