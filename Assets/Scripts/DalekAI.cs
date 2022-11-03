using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DalekAI : MonoBehaviour
{
    Transform target;
    [HideInInspector]
    public NavMeshAgent agent;
  
    [SerializeField]
    float chaseDistance = 20f;
    [SerializeField]
    float turnSpeed = 5f;
    public float damageAmount = 25f;
  
    public bool canAttack = true;

    public GameObject death;
    
   public bool atack = false;

    public dalekGun dalekgun;
    

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
       
    }

    // Update is called once per frame
    void Update()
    {
        // ateş = true;
        
        float distance = Vector3.Distance(transform.position, target.position);

        if ( !PlayerHealth.singleton.isDead) // if player is alive and enemy is alive 
        {
            if (distance >= chaseDistance /*|| dalekgun.hitControl != "Player"*/ /*|| dalekgun.hit.rigidbody == null || !dalekgun.hit.rigidbody.name.StartsWith("Player")*/) // uzaktaysa kovala yakındaysa saldır
            {
                ChasePlayer();
            }
            //else if (distance < chaseDistance && dalekgun.hit.collider.gameObject.tag != "Player") //can attack right attack if its true
            //{

            //    ChasePlayer();
                
            //}
            else if(distance < chaseDistance/* && dalekgun.hitControl == "Player"*/)
            {
                agent.updateRotation = true;
                Vector3 direction = target.position - transform.position; //player yukarıdamı aşağıdamı 
                direction.y = 0; // eğilip bükülmemesi için y ekseninde
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime); // smooth olması için
                agent.updatePosition = false;
                agent.SetDestination(target.position);

                if (canAttack)
                {
                    Debug.Log(this.name + " ATTACK!");
                    AttackPlayer();
                }
            }
            //else // geri kalan durumlarda enemy'i disablelar örneğin player öldüğünde vs
            //{
            //    DisableEnemy();
            //}
        }

        
        
    }
 
    void ChasePlayer()
    {
        agent.updateRotation = true; // tekrar navmesh devam eder
        agent.updatePosition = true;
        agent.SetDestination(target.position);
       
    }

    void AttackPlayer()
    {
        StartCoroutine(AttackTime());
    }

    //void DisableEnemy()
    //{
    //    canAttack = false;
        
    //}
    IEnumerator AttackTime()
     {
        
        canAttack = false;
        

        //PlayerHealth.singleton.TakeDamage(damageAmount); // PlayerHealth scriptteki DamagePlayer(damageAmount)
        atack = true;
        
        //dalekgun.Shoot();
        yield return new WaitForSeconds(3f);
        canAttack = true;
        
    }

    public void dalekDead()
    {
        death.SetActive(true);
    }
}
