using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneration : MonoBehaviour {
    public static EnemyGeneration instance;
    [SerializeField]
    private GameObject[] Enemy;
    [SerializeField]
    private Transform[] EnemyZone;

    private int MaxEnemy;
    public int NumEnemy;

	// Use this for initialization
	void Start () {
        if (instance == null)
            instance = this;

        MaxEnemy = EnemyZone.Length;
        NumEnemy = 0;

        StartCoroutine(SpawnEnemy());
    }
	
	// Update is called once per frame
	void Update () {

    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            int enemyIndex, zoneIndex;
            enemyIndex = Random.Range(0, 2);        // 스토커 or 도둑
            zoneIndex = Random.Range(0, 4);           // 스폰 위치

            if ( EnemyZone[zoneIndex].Find("Stalker(Clone)") == false && EnemyZone[zoneIndex].Find("Theft(Clone)") == false)
            {
                /*
                GameObject gb = Instantiate(Enemy[enemyIndex], EnemyZone[zoneIndex]);
                if(EnemyZone[zoneIndex].Find("Theft(Clone)").Find("TheftStar")==true)
                {
                    if (EnemyZone[zoneIndex].Find("Theft(Clone)").Find("TheftStar").gameObject.activeSelf)
                    {
                        Destroy(gb);
                        gb = Instantiate(Enemy[enemyIndex], EnemyZone[zoneIndex]);
                    }
                }
                */
                
                Instantiate(Enemy[enemyIndex], EnemyZone[zoneIndex]);
                NumEnemy++;
                
                Debug.Log("도둑 수 : " + NumEnemy);
                yield return new WaitForSeconds(30.0f);
            }
            else if (NumEnemy == 4)
            {
                Debug.Log("악당들이 모두 스폰되어 있음");
                yield return new WaitForSeconds(0.5f);
            } else
            {
                yield return new WaitForSeconds(0.5f);
                continue;
            }
        }
        
    }
}
