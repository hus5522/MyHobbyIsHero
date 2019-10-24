using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionWithObject : MonoBehaviour
{

    public static InteractionWithObject instance;
    public float MovePwr;

    public Rigidbody2D rb;

    [SerializeField]
    private Transform playerTrans;


    /* 즉발성 item 발동 유무 확인 변수 */
    private bool IsBat;
    private bool IsBatting;
    private bool IsRollerSkate;
    private bool IsKickBoard;
    public bool IsBible;

    [SerializeField]
    private GameObject Bat;
    private EdgeCollider2D BatEdge;
    public Animator swingAnimator;

    [SerializeField]
    private CircleCollider2D Extinguisher_Range;

    [SerializeField]
    private CircleCollider2D Spray_Range;

    /* pause */
    private bool IsPause;
    [SerializeField]
    private GameObject PauseMenu;

    // Use this for initialization
    void Start()
    {
        if (instance == null)
            instance = this;

        IsPause = false;
        PauseMenu.SetActive(false);

        MovePwr = 30.0f;
        rb = GetComponent<Rigidbody2D>();
        Extinguisher_Range.enabled = false;     //소화기 아이템 얻을 시 활성화
        Spray_Range.enabled = false;
        Bat.SetActive(false);
        swingAnimator = Bat.GetComponent<Animator>();

        SettingValue();     //초기값 설정

    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();

    }

    void FixedUpdate()
    {
        if (CharControl.instance.IsAction)
            return;

        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);

        rb.AddForce(movement * MovePwr);
    }

    public void SettingValue()
    {
        IsBat = false;
        IsBatting = false;
        IsBible = false;
        IsRollerSkate = false;
        IsKickBoard = false;

        //  Debug.Log("소화기 개수 : " + UIManager.instance.Num_FireExtinguisher);
        // Debug.Log("투척용 소화기 개수 : " + UIManager.instance.Num_ThrowFireExitinguisher);
        //  Debug.Log("스프레이 개수 : " + UIManager.instance.Num_Spray);
        //  Debug.Log("빠따 내구도 : " + UIManager.instance.Num_Usable_Bat);
    }

    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        IsPause = false;
    }

    private void HandleInput()
    {
        /* pause 사용 */
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!IsPause)
            {
                PauseMenu.SetActive(true);
                Time.timeScale = 0;
                IsPause = true;
                return;
            }

            if (IsPause)
            {
                PauseMenu.SetActive(false);
                Time.timeScale = 1;
                IsPause = false;
                return;
            }
        }

        /* bat 사용 */
        if (Input.GetKeyDown(KeyCode.Space) && IsBat && !IsBatting && UIManager.instance.Num_Usable_Bat >= 1)
        {
            StartCoroutine(SwingBat());
        }

        /* 소화기 사용 */
        if (Input.GetKeyDown(KeyCode.F) && UIManager.instance.Num_FireExtinguisher >= 1)
        {
            StartCoroutine(UsingFireExtinguisher());
        }

        /* 스프레이 사용 */
        if (Input.GetKeyDown(KeyCode.R) && UIManager.instance.Num_Spray >= 1)
        {
            StartCoroutine(UsingSpray());
        }

        /* 투척용 소화기 사용 */
        if (Input.GetMouseButtonDown(0) && UIManager.instance.Num_ThrowFireExitinguisher >= 1)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray2D ray = new Ray2D(pos, Vector2.zero);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider == null)
            {
                Debug.Log("불 아님");
            }
            else if (hit.collider.tag == "Fire")
            {
                Destroy(hit.collider.gameObject);
                UIManager.instance.Num_ThrowFireExitinguisher -= 1;
                GameMng.instance.FireCnt--;
                UIManager.instance.ExtinguisherHome();
                Debug.Log("투척용 소화기 사용, 남은 개수 : " + UIManager.instance.Num_ThrowFireExitinguisher);
                UIManager.instance.TFEText.text = "x " + UIManager.instance.Num_ThrowFireExitinguisher;

                UIManager.instance.score += 20;
                UIManager.instance.ScoreText.text = "" + UIManager.instance.score;
                SoundMng.instance.WaterSound();
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        /////////////////////////////////////////////////////////////////
        if (other.tag == "PARK")
        {
            //Debug.Log("enter");
        }
        else if (other.tag == "ENEMY")
        {
            //Debug.Log("ENEMY");
        }

        else if (other.tag == "HOMELESS")
        {
            //Debug.Log("HOMELESS");
            //other.gameObject.GetComponentInChildren<ParticleSystem>().Stop();
        }
        else if (other.tag == "DRUNKEN")
        {
            //Debug.Log("drunken");
            //CharControl.instance.ApState = CharControl.Char_State.drunken;
            //other.gameObject.GetComponentInChildren<ParticleSystem>().Stop();
        }

        //////////////////////////////////////////////////////////////////
        /* 배트 */
        if (other.gameObject.tag == "Bat")
        {
            UIManager.instance.Num_Usable_Bat = 3;         // 내구도 3으로 갱신
            IsBat = true;
            other.gameObject.SetActive(false);
            Bat.SetActive(true);


            BatEdge = Bat.GetComponentInChildren<EdgeCollider2D>();
            BatEdge.enabled = false;
            UIManager.instance.BatText.text = "x " + UIManager.instance.Num_Usable_Bat;
            //Debug.Log("빠따 내구도 : " + UIManager.instance.Num_Usable_Bat);
        }

        /* 성경책 */
        if (other.gameObject.tag == "Bible")
        {
            /* 성경책 먹엇을 때 도둑이 있는가 없는가 판별 */
            GameObject temp = GameObject.Find("Theft(Clone)");
            if (temp == null)
            {
                IsBible = false;
            } else if (temp != null)
            {
                IsBible = true;
            }

            other.gameObject.SetActive(false);
            //SoundMng.instance.Jesus();
            //Debug.Log("성격책 획득!");

        }

        /* 킥보드 */
        if (other.gameObject.tag == "KickBoard")
        {
            IsKickBoard = true;
            other.gameObject.SetActive(false);

            // Debug.Log("킥보드 탑승. 2초동안 이속 증가");

            StartCoroutine(UsingKickBoard());
        }

        /* 소화기 */
        if (other.gameObject.tag == "Fire_Extinguisher")
        {
            UIManager.instance.Num_FireExtinguisher += 1;      // 소지량 1 증가

            other.gameObject.SetActive(false);
            UIManager.instance.FEText.text = "x " + UIManager.instance.Num_FireExtinguisher;
            //Debug.Log("소화기 개수 : " + UIManager.instance.Num_FireExtinguisher);
        }

        /* 롤러스케이트 */
        if (other.gameObject.tag == "Skate")
        {
            IsRollerSkate = true;
            other.gameObject.SetActive(false);

            //Debug.Log("롤러 스케이트 착용, 3초동안 이속 증가");

            StartCoroutine(UsingSkate());
        }

        /* 투척용 소화기 */
        if (other.gameObject.tag == "Throw_Fire_Extinguisher")
        {
            UIManager.instance.Num_ThrowFireExitinguisher += 1;        // 소지량 1 증가
            other.gameObject.SetActive(false);

            UIManager.instance.TFEText.text = "x " + UIManager.instance.Num_ThrowFireExitinguisher;
            //Debug.Log("투척용 소화기 개수 : " + UIManager.instance.Num_ThrowFireExitinguisher);
        }

        /* 스프레이 */
        if (other.gameObject.tag == "Spray")
        {
            UIManager.instance.Num_Spray += 1;     // 소지량 1 증가
            other.gameObject.SetActive(false);
            UIManager.instance.SprayText.text = "x " + UIManager.instance.Num_Spray;
            // Debug.Log("스프레이 개수 : " + UIManager.instance.Num_Spray);
        }

    }//OnCollisionEnter2D

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "PARK")
        {
            if (IsRollerSkate)
            {
                MovePwr = 30.0f;
            }
            else if (IsKickBoard)
            {
                MovePwr = 50.0f;
            }
            else if (IsRollerSkate && IsKickBoard)
            {
                MovePwr = 50.0f;
            }
            else
            {
                MovePwr = 10.0f;
            }
        }
        if (other.tag == "DRUNKEN")
        {
            //Debug.Log("drunken");
            CharControl.instance.ApState = CharControl.Char_State.drunken;
        }
        else if (other.tag == "HOMELESS")
        {
            //Debug.Log("homeless");
            CharControl.instance.ApState = CharControl.Char_State.Homeless;
        }
    }


    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "PARK")
        {
            if (IsRollerSkate)
            {
                MovePwr = 50.0f;
            }
            else if (IsKickBoard)
            {
                MovePwr = 70.0f;
            }
            else if (IsRollerSkate && IsKickBoard)
            {
                MovePwr = 70.0f;
            }
            else
            {
                MovePwr = 30.0f;
            }
        }
        CharControl.instance.ApState = CharControl.Char_State.idle;
    }

    IEnumerator UsingFireExtinguisher()
    {
        Extinguisher_Range.enabled = true;
        SoundMng.instance.Spray();
        UIManager.instance.Num_FireExtinguisher -= 1;
        GameMng.instance.FireCnt--;
        UIManager.instance.SafetySpeed -= 0.05f;
        UIManager.instance.ExtinguisherHome();
        Debug.Log("소화기 사용, 남은 개수 : " + UIManager.instance.Num_FireExtinguisher);
        UIManager.instance.FEText.text = "x " + UIManager.instance.Num_FireExtinguisher;
        yield return new WaitForSeconds(0.1f);

        Extinguisher_Range.enabled = false;
        yield return null;
    }

    IEnumerator UsingSpray()
    {
        Debug.Log("스프레이 코루틴");
        Spray_Range.enabled = true;
        SoundMng.instance.Spray();
        UIManager.instance.Num_Spray -= 1;
        UIManager.instance.SafetySpeed -= 0.05f;
        UIManager.instance.PersonSolution();
        Debug.Log("스프레이 사용, 남은 개수 : " + UIManager.instance.Num_Spray);
        UIManager.instance.SprayText.text = "x " + UIManager.instance.Num_Spray;
        yield return new WaitForSeconds(0.1f);

        Spray_Range.enabled = false;
        yield return null;
    }
    IEnumerator SwingBat()
    {
        swingAnimator.SetTrigger("Batting");
        if (!SoundMng.instance.IsPlayingBatSound)
        {
            SoundMng.instance.BatSound();
        }
        IsBatting = true;
        UIManager.instance.Num_Usable_Bat -= 1;        // 배트 사용량 1 감소

        BatEdge.enabled = true;
        Debug.Log("배트 사용! 현재 내구도 : " + UIManager.instance.Num_Usable_Bat);
        UIManager.instance.BatText.text = "x " + UIManager.instance.Num_Usable_Bat;
        yield return new WaitForSeconds(1.0f);

        SoundMng.instance.IsPlayingBatSound = false;
        IsBatting = false;
        BatEdge.enabled = false;
        if (UIManager.instance.Num_Usable_Bat <= 0)
            Bat.SetActive(false);
        yield return null;
    }

    IEnumerator UsingSkate()
    {
        if (IsRollerSkate)
        {
            /*
            if (!SoundMng.instance.IsPlayingKickBoardSound)
            {
                SoundMng.instance.KickBoardSound();
            }
            */
            SoundMng.instance.KickBoardSound();
            MovePwr = 50.0f;
            yield return new WaitForSeconds(2.0f);
            IsRollerSkate = false;
            MovePwr = 30.0f;
            Debug.Log("3초가 지나서 이속 원상 복귀");
            yield return null;
        }
    }

    IEnumerator UsingKickBoard()
    {
        if (IsKickBoard)
        {
            /*
            if (!SoundMng.instance.IsPlayingKickBoardSound)
            {
                SoundMng.instance.KickBoardSound();
            }
            */
            SoundMng.instance.KickBoardSound();
            MovePwr = 70.0f;
            yield return new WaitForSeconds(1.0f);
            SoundMng.instance.IsPlayingKickBoardSound = false;
            IsKickBoard = false;
            MovePwr = 30.0f;
            Debug.Log("2초가 지나서 이속 원상 복귀");
            yield return null;
        }
    }


}
