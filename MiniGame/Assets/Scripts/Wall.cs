using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float speed;
    public float HP;
    Player player;

    void Start()
    {
        player = GameObject.Find(name: "Player").GetComponent<Player>();       
        speed = player.GetComponent<Player>().moveSpeed;
        Debug.Log(speed);
    }

    void Update()
    {  
        transform.Translate(speed * Time.deltaTime, 0, 0);//x
        if(transform.position.x < -10)
        {  
            Destroy(gameObject);
        }
    }

      private void OnCollisionEnter(Collision collision)//유니티 이벤트
    {
        Destroy(gameObject);
    }
}
