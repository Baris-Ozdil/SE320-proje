using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    Transform target;
    [HideInInspector]
    public NavMeshAgent agent;
    Animator anim;
    bool Death = false;
    [SerializeField]
    float chaseDistance = 2f;
    [SerializeField]
    float turnSpeed = 5f;
    public float damageAmount = 25f;
    [SerializeField]
    float attackTime = 2f;
    public bool canAttack = true;

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
        if(!Death && !PlayerHealth.singleton.isDead) // if player is alive and enemy is alive 
        {
            if (distance > chaseDistance) // uzaktaysa kovala yakındaysa saldır
            {
                ChasePlayer();
            }
            else if (distance < chaseDistance && canAttack) //can attack right attack if its true
            {
                AttackPlayer();
            }
            else // geri kalan durumlarda enemy'i disablelar örneğin player öldüğünde vs
            {
                DisableEnemy();
            }
        }
        
    }
    public void EnemyDeathAnim()
    {
        Death = true;
        anim.SetTrigger("Death");
    }
    void ChasePlayer()
    {
        agent.updateRotation = true; // tekrar navmesh devam eder
        agent.updatePosition = true; 
        agent.SetDestination(target.position);
        anim.SetBool("Walking", true);
        anim.SetBool("Attacking", false);
        
    }
    void AttackPlayer()
    {
        agent.updateRotation = false;
        Vector3 direction = target.position - transform.position; //player yukarıdamı aşağıdamı 
        direction.y = 0; // eğilip bükülmemesi için y ekseninde
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed*Time.deltaTime); // smooth olması için
        agent.updatePosition = false; // saldırırken hareket etmemesi için
        anim.SetBool("Walking", false);
        anim.SetBool("Attacking", true);
        StartCoroutine(AttackTime());
        
    }
    void DisableEnemy()
    {
        canAttack = false;
        anim.SetBool("Walking", false);
        anim.SetBool("Attacking", true);

    }
    IEnumerator AttackTime()
    {
        canAttack = false;
        yield return new WaitForSeconds(0.5f); // vuruş animasyonu 43 frame 0.5 yeterli olur 
        PlayerHealth.singleton.TakeDamage(damageAmount); // PlayerHealth scriptteki DamagePlayer(damageAmount)
        yield return new WaitForSeconds(attackTime);
        canAttack = true;
    }
}
