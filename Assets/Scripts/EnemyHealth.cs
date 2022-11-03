using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : Health
{
    public EnemyAI enemyAI;
    public bool isEnemyDead;
    private DalekAI dalekAI;

    public Camera playerCamera;
    public GameObject healthBarUI;
    public Slider enemyHealthSlider;

    public Collider[] enemyColliders;

    private void Start()
    {
        playerCamera = Camera.main;
        enemyAI = GetComponent<EnemyAI>();
        enemyHealthSlider.value = 1;
        dalekAI = GetComponent<DalekAI>();
        //enemmy = GameObject.FindGameObjectWithTag("Enemy");
    }

    public override void TakeDamage(float deductHealth) // public method yapıyoruz ki daha sonra cagıralım 
    {
        if (!isEnemyDead) //tekrar tekrar ölmemesi için 
        {
            health -= deductHealth;
            //enemyHealthSlider.value -= deductHealth;
            if (health <= 0)
            {
                Die();
            }
        }
    }

    public void Update()
    {
        if (enemyHealthSlider.value < health)
        {
            healthBarUI.SetActive(true);
        }

        if (enemyHealthSlider.value <= 0)
        {
            healthBarUI.SetActive(false);
        }

        enemyHealthSlider.value = health / 100;
    }

    public void FixedUpdate()
    {
        healthBarUI.transform.LookAt(playerCamera.transform);
    }

    public override void Die()
    {
        if (gameObject.tag == "Enemy")
        {
            isEnemyDead = true;
            enemyAI.EnemyDeathAnim();
            enemyAI.agent.speed = 0f;
            foreach (var col in enemyColliders)
            {
                col.enabled = false; // cesetlerin collide capsulunu kapatır
            }

            health = 0f;
            //enemyHealthSlider.value = 0f;
            Destroy(gameObject, 10);
        }

        if (gameObject.tag == "dalek")
        {
            isEnemyDead = true;
            dalekAI.dalekDead();

            health = 0f;
            //enemyHealthSlider.value = 0f;
            Destroy(gameObject, 3);
        }
    }

    public override void TakeHealth(float health)
    {
      
    }
}