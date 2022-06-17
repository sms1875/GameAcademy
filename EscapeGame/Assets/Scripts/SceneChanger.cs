using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string nextSceneName;
    public bool gameWin;
    public bool gameLose;
    public Animator anim;

    private void Start()
    {
        anim.SetBool("Win", gameWin);
        anim.SetBool("Lose", gameLose);
    }

    void Update()
    {
        if (Input.GetButtonDown("Submit"))//Submit== 엔터, 스페이스바
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
