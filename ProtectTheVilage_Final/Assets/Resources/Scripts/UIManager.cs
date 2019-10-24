using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    /* 점수
    스토커 = 100
    도둑 = 50
    노숙자&취객 = 30
    길잃은아이가 도착지점 도착시 = 100
    불끄기 = 20
    
        UIManager.instance.score += 100;
        UIManager.instance.ScoreText.text = "" + UIManager.instance.score;
    */

    public static UIManager instance;
    /* item 관련 텍스트 */
    public Text FEText;
    public Text SprayText;
    public Text TFEText;
    public Text BatText;
    public Text ScoreText;

    /* 사용할 수 있는 Item 개수  */
    public int Num_FireExtinguisher;
    public int Num_ThrowFireExitinguisher;
    public int Num_Spray;
    public int Num_Usable_Bat;

    public int score;

    public float Safety;
    public float SafetySpeed;
    public Slider SafetyGauge;

    // Use this for initialization
    void Start()
    {
        if (instance == null)
            instance = this;

        SafetyGauge = GameObject.Find("Slider").GetComponent<Slider>();
        Safety = 0.5f;
        SafetySpeed = 0.0f;
        Num_FireExtinguisher = 0;
        Num_ThrowFireExitinguisher = 0;
        Num_Spray = 0;
        Num_Usable_Bat = 0;
        score = 0;

        FEText = GameObject.Find("NumberofFE").GetComponent<Text>();
        SprayText = GameObject.Find("NumberofSpray").GetComponent<Text>();
        TFEText = GameObject.Find("NumberofTFE").GetComponent<Text>();
        BatText = GameObject.Find("NumberofBat").GetComponent<Text>();
        ScoreText = GameObject.Find("ScoreNum").GetComponent<Text>();
        
    }

    public void PersonSolution()
    {
        SafetyGauge.value += 15;

    }

    public void ExtinguisherHome()
    {
        SafetyGauge.value += 80;
    }
    



    void FixedUpdate()
    {
        if (SafetySpeed <= 0)
            SafetySpeed = 0;

        SafetyGauge.value -= SafetySpeed;

        if (SafetyGauge.value <= 0)
        {
            GameObject.Find("dontdestroy").GetComponent<DontDestroy>().score = GameObject.Find("ScoreNum").GetComponent<Text>().text;
            SceneManager.LoadScene("End");

        }
    }
}
