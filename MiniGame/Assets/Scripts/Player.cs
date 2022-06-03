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

    private void OnCollisionEnter(Collision collision)//����Ƽ �̺�Ʈ
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void addScore(int s)
    {
        score += s;
        scoreOutput.text = "����: "+ score;
    }

    /*����Ƽ �̺�Ʈ 3����
     * Ʈ����
     * �ݸ���
     * ����ĳ��Ʈ
     * + ��������� �̺�Ʈ
    */

}
