using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float speed = -5; // x y z (position)
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find(name: "Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);//x
        if(transform.position.x < -10)
        {  
            player.addScore(1);
            Destroy(gameObject);
        }
    }
}
