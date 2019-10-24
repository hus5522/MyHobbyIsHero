using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostGeneration : MonoBehaviour {

    public static LostGeneration instance;
    public Transform[] LostArr;

    public GameObject Lost;

    public float timming;
    public float CreateTimeInterval;
    public float CreateTime;
    public int Count;

	// Use this for initialization
	void Start () {
        if (instance == null)
            instance = this;

        Lost = Resources.Load<GameObject>("Prefabs/Lost");
        CreateTimeInterval = 10;
        timming = 0;

        LostArr = new Transform[7];

        for (int i = 0; i < LostArr.Length; i++)
        {
            LostArr[i] = transform.Find((i + 1).ToString());
        }
	}
	
	// Update is called once per frame
	void Update () {
        timming += Time.deltaTime;
        if(timming>=CreateTimeInterval)
        {
            int tempidx = Random.Range(0, 7);
            if (LostArr[tempidx].Find("Lost(Clone)") == false)
            {
                //생성
                Instantiate(Lost, LostArr[tempidx]);
                Count++;
                UIManager.instance.SafetySpeed += 0.05f;
                timming = 0;

            }
            else
                Debug.Log("길 잃은 사람 중복");

        }

		
	}
}
