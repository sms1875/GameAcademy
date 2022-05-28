using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float jumpPower = 5;
    //Rigidbody rid_;

    private void Awake()
    {

    
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
}
