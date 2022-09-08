using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveInChess : MonoBehaviour
{
    public Transform Player;
    public float speed;
    bool a, b;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    public void Right()
    {

        Player.position += new Vector3(10, 0, 0);
    }
    public void Left()
    {
        Player.position += new Vector3(-10, 0, 0);
    }
    public void Up()
    {
        Player.position += new Vector3(0, 0, 10);
    }
    public void Down()
    {
        Player.position += new Vector3(0, 0, -10);
    }
    public void RightUp()
    {
        Player.position += new Vector3(10, 0, 10);
    }
    public void RightDown()
    {
        Player.position += new Vector3(10, 0, -10);
    }
    public void LeftUp()
    {
        Player.position += new Vector3(-10, 0, 10);
    }
    public void LeftDown()
    {
        Player.position += new Vector3(-10, 0, -10);
    }
}
