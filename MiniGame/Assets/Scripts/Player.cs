using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float upDownValue = 2.5f; //캐릭터 y좌표 이동값
    public float HP = 55555; //체력
    public Vector3 position; //위치

    public float moveSpeed = -5f;//벽 이동속도

    public GameObject attackPrefab; //공격 프리팹
    public float attackSpeed = 15f;//공격속도
    public float attackInterval = 0.5f;//간격
    //점수
    TextMesh scoreOutput;
    int score = 0;
    float term;
    private void Awake()
    {
        scoreOutput = GameObject.Find(name: "Score").GetComponent<TextMesh>();
    }

    void Start()
    {
        term = attackInterval;
        position = GetComponent<Rigidbody>().position;   
    }

    void Update()
    { 
        term += Time.deltaTime;//보간 값 1초에 1
        //공격
        if (Input.GetKey(KeyCode.Space))
        {
            if (term >= attackInterval)
            {
                Vector3 attackPosition = position;
                attackPosition.x = position.x + 1f;
                Instantiate(attackPrefab, attackPosition, transform.rotation);
                term -= attackInterval;
            }
        }
        //캐릭터이동
        if (Input.GetKeyDown(KeyCode.W) && position.y < 4f)
        {
            transform.position = new Vector3(position.x, position.y + upDownValue, position.z);
            position.y += upDownValue;
        }
        else if (Input.GetKeyDown(KeyCode.S) && position.y > -4f)
        {
            transform.position = new Vector3(position.x, position.y - upDownValue, position.z);
            position.y -= upDownValue;
        }
    }

    private void OnCollisionEnter(Collision collision)//유니티 이벤트
    {
        Debug.Log(HP);
        HP --;
        if (HP == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void addScore(int s)
    {
        score += s;
        scoreOutput.text = "점수: "+ score;
    }

    public void move()
    {
        moveSpeed -= 0.5f;
    }

    /*유니티 이벤트 3가지
     * 트리거
     * 콜리전
     * 레이캐스트
     * + 직접만드는 이벤트
    */

}
