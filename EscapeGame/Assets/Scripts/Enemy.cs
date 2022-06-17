using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform playertr;
    public Transform enemytr;
    NavMeshAgent agent;
    Animator anim;
    public SphereCollider sphereCollider;

    public bool chase;
    public float chaseDelay=3f;

    void Start()
    {
        enemytr = GetComponentInChildren<Transform>();
        playertr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        sphereCollider = gameObject.GetComponent<SphereCollider>();
        chase = true;
    }

    void Update()
    {
        if (chase)
        {
            chasing();
        }
        else
        {
            delay();
        }
    }
    
    void chasing()
    {
        agent.destination = playertr.position;
        anim.SetFloat("Walk", 1f);
    }

    void delay()
    {
        chaseDelay -= Time.deltaTime;
        if (chaseDelay <= 0)
        {
            anim.SetTrigger("SleepEnd");
            chaseDelay = 3f;
            chase = true;
            sphereCollider.enabled = true;
        }
    }

    void setDelay()
    {
        sphereCollider.enabled = false;
        chase = false;
        agent.destination = transform.position;
        anim.SetFloat("Walk", 0);
        anim.SetTrigger("SleepStart"); 
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                setDelay();
                break;
        }

    }
}
