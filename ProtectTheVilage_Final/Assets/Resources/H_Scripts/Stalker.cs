using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalker : MonoBehaviour {

    private SpriteRenderer Star;
    private GameObject StarOb;

    public Animator StarAnimator;
    
	// Use this for initialization
	void Start () { 
        Star = GameObject.Find("StalkerStar").GetComponent<SpriteRenderer>();
        StarOb = GameObject.Find("StalkerStar");
        StarOb.SetActive(false);

        UIManager.instance.SafetySpeed += 0.05f;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerSpray")
        {
            Debug.Log("으악!!!!!1");
            StartCoroutine(DefeatedStalker());
        }
    }

    IEnumerator DefeatedStalker()
    {
        Debug.Log("스토커가 스프레이에 맞았다!");
        StarOb.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        StarAnimator.SetTrigger("Star");
        SoundMng.instance.StarSound();
        yield return new WaitForSeconds(2.1f);
        EnemyGeneration.instance.NumEnemy -= 1;
        UIManager.instance.SafetySpeed -= 0.05f;
        Debug.Log("스토커가 사라집니다");
        StarOb.SetActive(false);

        UIManager.instance.score += 100;
        UIManager.instance.ScoreText.text = "" + UIManager.instance.score;

        yield return new WaitForSeconds(0.1f);

        Destroy(this.gameObject);
        yield return null;
    }

}
