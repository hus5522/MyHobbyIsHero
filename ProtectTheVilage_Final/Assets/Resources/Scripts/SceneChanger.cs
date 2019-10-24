using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    [SerializeField]
    private Animator[] TitleLetter;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(CreatTtile());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TextScaleChange()
    {

    }

    IEnumerator CreatTtile()
    {
        for (int i = 0; i < TitleLetter.Length; i++)
        {
            TitleLetter[i].SetTrigger("Start");
            yield return new WaitForSeconds(0.6f);
        }

        yield return null;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("InGame");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}