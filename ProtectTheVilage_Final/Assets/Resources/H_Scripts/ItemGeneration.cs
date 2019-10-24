using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGeneration : MonoBehaviour {

    [SerializeField]
    private GameObject[] ItemList;

    [SerializeField]
    private GameObject[] ItemPoint;

	// Use this for initialization
	void Start () {

        StartCoroutine(SpawnItem());
        
	}
	
    IEnumerator SpawnItem()
    {
        while (true)
        {
            for (int i = 0; i < ItemPoint.Length; i++)
            {
                int index;
                index = Random.Range(0, 7);

                GameObject item = Instantiate(ItemList[index]) as GameObject;
                item.transform.position = ItemPoint[i].transform.position;
                Destroy(item, 8.5f);
            }//for

            yield return new WaitForSeconds(9.0f);
        }//while
    }

	// Update is called once per frame
	void Update () {
		
	}
   
}
