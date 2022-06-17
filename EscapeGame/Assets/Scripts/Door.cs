using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Player player;
    Animator anim;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                if (player.KeyTrigger)
                {
                    anim.SetTrigger("Key");
                }
                break;
        }

    }
}
