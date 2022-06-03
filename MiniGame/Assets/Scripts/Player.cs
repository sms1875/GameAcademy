using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float jumpPower = 5;
    TextMesh scoreOutput;
    //Rigidbody rid_;
    int score = 0;

    private void Awake()
    {
        scoreOutput = GameObject.Find(name: "Score").GetComponent<TextMesh>();
    }

    void Start()
    {
        //rid_=GetComponent<Rigidbody>;
    }


    void Update()
    {
        if(Input.GetButtonDown("Jump"))
          GetComponent<Rigidbody>().velocity = new Vector3(0, jumpPower, 0);
    }

    private void OnCollisionEnter(Collision collision)//유니티 이벤트
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void addScore(int s)
    {
        score += s;
        scoreOutput.text = "점수: "+ score;
    }

    /*유니티 이벤트 3가지
     * 트리거
     * 콜리전
     * 레이캐스트
     * + 직접만드는 이벤트
    */

}
