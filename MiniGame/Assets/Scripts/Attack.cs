using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float speed;
    Player player;

    void Start()
    {
        player = GameObject.Find(name: "Player").GetComponent<Player>();
        speed = player.GetComponent<Player>().attackSpeed;
    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);//x
        if (transform.position.x > 10)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)//유니티 이벤트
    {
        Destroy(gameObject);
    }
}

