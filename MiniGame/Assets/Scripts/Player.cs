using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float upDownValue = 2.5f; //ĳ���� y��ǥ �̵���
    public float HP = 55555; //ü��
    public Vector3 position; //��ġ

    public float moveSpeed = -5f;//�� �̵��ӵ�

    public GameObject attackPrefab; //���� ������
    public float attackSpeed = 15f;//���ݼӵ�
    public float attackInterval = 0.5f;//����
    //����
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
        term += Time.deltaTime;//���� �� 1�ʿ� 1
        //����
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
        //ĳ�����̵�
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

    private void OnCollisionEnter(Collision collision)//����Ƽ �̺�Ʈ
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
        scoreOutput.text = "����: "+ score;
    }

    public void move()
    {
        moveSpeed -= 0.5f;
    }

    /*����Ƽ �̺�Ʈ 3����
     * Ʈ����
     * �ݸ���
     * ����ĳ��Ʈ
     * + ��������� �̺�Ʈ
    */

}
