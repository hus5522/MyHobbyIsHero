using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCollisionManger : MonoBehaviour {

    [SerializeField]
    private GameObject fire;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            UIManager.instance.score += 20;
            UIManager.instance.ScoreText.text = "" + UIManager.instance.score;
            Destroy(fire);
        }
    }

}
