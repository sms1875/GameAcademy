using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    Player player; //플레이어
    public GameObject[] wallPrefab; //벽

    public float interval = 1.5f;//간격
    float[] range = { -5f, -2.5f, 0f, 2.5f, 5f };//높이 종류
    List<float> randRange = new List<float>();//랜덤 순서로 높이값이 들어감

    float term;

    void Start()
    {
        player = GameObject.Find(name: "Player").GetComponent<Player>();
        term = interval;
    }

    void Update()
    {
        term += Time.deltaTime;//보간 값 1초에 1
        if(term>=interval)
        {
            int obstacle = Random.Range(2, 5);//장애물 개수
            Vector3 pos = transform.position;//스포너위치(10,0,0
            randRange.Clear();//높이 초기화
            int i = 0;//for문에 사

            //랜덤 높이 설정
            for(int count=0; count < obstacle; count++)
            {
                float y = range[Random.Range(0, 5)];//range에서 랜덤한 높이 추출
                //중복된 값이 없을때
                if (!randRange.Contains(y))
                {
                    randRange.Add(y);
                }
                else
                {
                    while (randRange.Contains(y))
                    //while 반복문을 통하여 새로 또 추출한다.
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
            //강화 object 10%확률 생성
            if(Random.Range(0, 10)%10==0)
            {
                pos.y = randRange[i]; //y값 추가
                Instantiate(wallPrefab[2], pos, transform.rotation);
                i++;
            }
            */
            //붉은 장애물 30%확률로 생성
            if(Random.Range(0, 10)<3)
            {
                pos.y = randRange[i++]; //y값 추가
                Instantiate(wallPrefab[1], pos, transform.rotation);
            }

            for (;i< obstacle; i++)
            {            
                pos.y = randRange[i]; //y값 추가
                Instantiate(wallPrefab[0], pos, transform.rotation);   
            }
            player.move();
            player.addScore(1);
            term -= interval;
        }
    }
}
