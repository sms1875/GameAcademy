using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject wallPrefab; //��
    public float interval = 1.5f;
    public float range = 3;
    float term;

    void Start()
    {
        term = interval;
    }

    void Update()
    {
        term += Time.deltaTime;//���� �� 1�ʿ� 1
        if(term>=interval)
        {
            Vector3 pos = transform.position;
            pos.y += Random.Range(-range, range); //-3~3 ���� ������
            Instantiate(wallPrefab, pos, transform.rotation);
            term -= interval;
        }
    }
}
