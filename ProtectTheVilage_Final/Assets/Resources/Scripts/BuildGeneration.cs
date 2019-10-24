using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildGeneration : MonoBehaviour
{

    public Transform[] PointArr;
    public Sprite[] HomeSp;
    public Sprite[] PublicSp;
    // Use this for initialization
    void Start()
    {
        HomeSp = new Sprite[4];
        HomeSp = Resources.LoadAll<Sprite>("Sprites/Homes");
        PublicSp = new Sprite[6];
        PublicSp = Resources.LoadAll<Sprite>("Sprites/PublicBuilding");

        PointArr = new Transform[28];

        for (int i = 0; i < PointArr.Length; i++)
            PointArr[i] = transform.Find((i + 1).ToString()).transform;

        for (int i = 0; i < 6; i++) //공공건물 배치 소스
        {
            int tmp = Random.Range(0, 28);
            if (PointArr[tmp].Find("Home Sprite").GetComponent<SpriteRenderer>().sprite == true)//이미 포인트에 건물이 있다면.
            {
                i--;
                Debug.Log("건물 이미 있음");
            }
            else
                PointArr[tmp].Find("Home Sprite").GetComponent<SpriteRenderer>().sprite = PublicSp[i];

        }
        Debug.Log("공공건물 배치 완료");


        for (int i = 0; i < PointArr.Length; i++)
        {
            int idx;
            idx = Random.Range(0, 4);
            if (PointArr[i].Find("Home Sprite").GetComponent<SpriteRenderer>().sprite == true)
            {
                continue;
            }
            else
            {
                PointArr[i].Find("Home Sprite").GetComponent<SpriteRenderer>().sprite = HomeSp[idx];
                PointArr[i].tag = "HOME";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
