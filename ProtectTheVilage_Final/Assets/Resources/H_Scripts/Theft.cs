using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Theft : MonoBehaviour {

    private GameObject ThunderOb;
    private GameObject StarOb;
    private SpriteRenderer Star;

    public Animator StarAnimator;
    private bool IsAnimating;
    private bool IsScored;

	// Use this for initialization
	void Start () {
        IsAnimating = false;
        IsScored = false;
        ThunderOb = GameObject.Find("Thunder");
        ThunderOb.SetActive(false);

        //Star = GameObject.Find("TheftStar").GetComponent<SpriteRenderer>();
        //Star.enabled = false;
        StarOb = GameObject.Find("TheftStar");
        StarOb.SetActive(false);
        UIManager.instance.SafetySpeed += 0.05f;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (InteractionWithObject.instance.IsBible && !SoundMng.instance.IsPlayingThunder)//&& InteractionWithObject.instance.onceBible)
            destroyTheft();
            
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "PlayerBat")// && !IsAnimating)
        {
            StartCoroutine(DefeatedTheft());
        }
    }

    public void destroyTheft()
    {
        StartCoroutine(DestroyTheft());
    }

    IEnumerator DestroyTheft()
    {
        //InteractionWithObject.instance.onceBible = false;
        /* 한번만 실행, 소리 나오고 있을때는 실행 안함 */
        if (!SoundMng.instance.IsPlayingJesus)
            SoundMng.instance.Jesus();
        yield return new WaitForSeconds(1.332f);

        SoundMng.instance.IsPlayingJesus = false;
        Debug.Log("도둑에게 번개가 내려칩니다.");
        ThunderOb.SetActive(true);
        /* 한번만 실행, 소리 나오고 있을때는 실행 안함 */
        if (!SoundMng.instance.IsPlayingThunder)
            SoundMng.instance.Thunder();
        yield return new WaitForSeconds(5.224f);

        SoundMng.instance.IsPlayingThunder = false;
        //ThunderOb.SetActive(false);
        InteractionWithObject.instance.IsBible = false;
        yield return new WaitForSeconds(0.1f);

        EnemyGeneration.instance.NumEnemy -= 1;
        UIManager.instance.SafetySpeed -= 0.05f;
        UIManager.instance.PersonSolution();
        Debug.Log("도둑 수 : " + EnemyGeneration.instance.NumEnemy);
        Debug.Log("도둑이 사라집니다.");

        /* 한번씩만 점수 상승 */
        if (!IsScored)
        {
            UIManager.instance.score += 50;
            UIManager.instance.ScoreText.text = "" + UIManager.instance.score;
            IsScored = true;
        }
        StopCoroutine(DestroyTheft());
        yield return new WaitForSeconds(0.1f);
        //this.gameObject.SetActive(false);
        Destroy(this.gameObject);
        yield return null;
    }

    IEnumerator DefeatedTheft()
    {
        //yield return new WaitForSeconds(0.1f);
        SoundMng.instance.HitSound();
        IsAnimating = true;
        Debug.Log("도둑이 빠따에 맞았다!");
        StarOb.SetActive(true);
        StarAnimator.SetTrigger("Star");
        SoundMng.instance.StarSound();
        yield return new WaitForSeconds(2.1f);
        StarOb.SetActive(false);
        Debug.Log("도둑이 사라집니다");
        EnemyGeneration.instance.NumEnemy -= 1;
        UIManager.instance.SafetySpeed -= 0.05f;
        UIManager.instance.PersonSolution();
        IsAnimating = false;
        UIManager.instance.score += 50;
        UIManager.instance.ScoreText.text = "" + UIManager.instance.score;
        //Debug.Log("도둑 수 : " + EnemyGeneration.instance.NumEnemy);
        StopCoroutine(DefeatedTheft());
        yield return new WaitForSeconds(0.1f);
        //this.gameObject.SetActive(false);
        Destroy(this.gameObject);
        yield return null;
    }
    
}
