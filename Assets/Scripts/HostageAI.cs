using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class HostageAI : MonoBehaviour
{
    Transform target;
    [HideInInspector] public NavMeshAgent agent;
    Animator anim;
    bool Death = false;
    [SerializeField] float chaseDistance = 2f;
    [SerializeField] bool control = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance < 3 && Input.GetKeyDown(KeyCode.E))
        {
            control = true;
        }

        if (control)
        {
            if (!Death && !PlayerHealth.singleton.isDead) // if player is alive 
            {
                if (distance > chaseDistance) // uzaktaysa kovala
                {
                    ChasePlayer();
                }
                else
                {
                    anim.SetBool("walk", false);
                    anim.SetBool("wait", true);
                }
            }

            void ChasePlayer()
            {
                agent.updateRotation = true; // tekrar navmesh devam eder
                agent.updatePosition = true;
                agent.SetDestination(target.position);
                anim.SetBool("walk", true);
                anim.SetBool("wait", false);
            }
        }
    }
}