using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMng : MonoBehaviour {

    public static SoundMng instance;
    public AudioClip[] Clips;
    AudioSource AS;

    public bool IsPlayingJesus;
    public bool IsPlayingThunder;
    public bool IsPlayingFire;
    public bool IsPlayingBatSound;
    public bool IsPlayingKickBoardSound;

	// Use this for initialization
	void Start () {
        if (instance == null)
            instance = this;

        IsPlayingJesus = false;
        IsPlayingThunder = false;
        IsPlayingFire = false;
        IsPlayingBatSound = false;
        IsPlayingKickBoardSound = false;

        AS = this.GetComponent<AudioSource>();
        Clips = Resources.LoadAll<AudioClip>("SFX");

    }


    public void PhoneRing()
    {
        AS.PlayOneShot(Clips[0]); //폰 통화 소리
    }

    public void Clearsound()
    {
        AS.PlayOneShot(Clips[1]); //전화해서 해결한 소리
    }

    public void Jesus()
    {
        IsPlayingJesus = true;
        AS.PlayOneShot(Clips[3]); //성경책 소리
    }

    public void Thunder()
    {
        IsPlayingThunder = true;
        AS.PlayOneShot(Clips[4]); //번개 소리
    }

    public void Spray()
    {
        AS.PlayOneShot(Clips[5]); // 스프레이 소리
    }

    public void Fire()
    {
        IsPlayingFire = true;
        AS.PlayOneShot(Clips[6], 0.1f);   // 불타는 소리
        
    }

    public void DustWind()
    {
        AS.PlayOneShot(Clips[7]);   // 먼지 바람 부는 소리
    }

    public void CarDoorSound()
    {
        AS.PlayOneShot(Clips[8]);   // 차 문 닫히는 소리
    }

    public void KickBoardSound()
    {
        IsPlayingKickBoardSound = true;
        AS.PlayOneShot(Clips[9]);  // 전동킥보드 타는 소리
    }

    public void WaterSound()
    {
        AS.PlayOneShot(Clips[10]);  // 투척용 소화기 터지는 소리
    }

    public void HitSound()
    {
        AS.PlayOneShot(Clips[11]);  // 배트에 맞는 소리
    }

    public void StarSound()
    {
        AS.PlayOneShot(Clips[12]);  // 별 돌아가는 소리
    }

    public void BatSound()
    {
        IsPlayingBatSound = true;
        AS.PlayOneShot(Clips[13]);   // 배트 휘두르는 소리
    }

}
