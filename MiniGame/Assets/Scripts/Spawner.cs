using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    Player player; //�÷��̾�
    public GameObject[] wallPrefab; //��

    public float interval = 1.5f;//����
    float[] range = { -5f, -2.5f, 0f, 2.5f, 5f };//���� ����
    List<float> randRange = new List<float>();//���� ������ ���̰��� ��

    float term;

    void Start()
    {
        player = GameObject.Find(name: "Player").GetComponent<Player>();
        term = interval;
    }

    void Update()
    {
        term += Time.deltaTime;//���� �� 1�ʿ� 1
        if(term>=interval)
        {
            int obstacle = Random.Range(2, 5);//��ֹ� ����
            Vector3 pos = transform.position;//��������ġ(10,0,0
            randRange.Clear();//���� �ʱ�ȭ
            int i = 0;//for���� ��

            //���� ���� ����
            for(int count=0; count < obstacle; count++)
            {
                float y = range[Random.Range(0, 5)];//range���� ������ ���� ����
                //�ߺ��� ���� ������
                if (!randRange.Contains(y))
                {
                    randRange.Add(y);
                }
                else
                {
                    while (randRange.Contains(y))
                    //while �ݺ����� ���Ͽ� ���� �� �����Ѵ�.
                    {
                        y = range[Random.Range(0, 5)];
                        if (!randRange.Contains(y))
                            break;
                        else
                            continue;
                    }
                    randRange.Add(y);
                }
            }

            /*
            //��ȭ object 10%Ȯ�� ����
            if(Random.Range(0, 10)%10==0)
            {
                pos.y = randRange[i]; //y�� �߰�
                Instantiate(wallPrefab[2], pos, transform.rotation);
                i++;
            }
            */
            //���� ��ֹ� 30%Ȯ���� ����
            if(Random.Range(0, 10)<3)
            {
                pos.y = randRange[i++]; //y�� �߰�
                Instantiate(wallPrefab[1], pos, transform.rotation);
            }

            for (;i< obstacle; i++)
            {            
                pos.y = randRange[i]; //y�� �߰�
                Instantiate(wallPrefab[0], pos, transform.rotation);   
            }
            player.move();
            player.addScore(1);
            term -= interval;
        }
    }
}
