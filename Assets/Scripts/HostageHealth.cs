using UnityEngine;
using UnityEngine.UI;

public class HostageHealth : Health
{
    public HostageAI hostageAI;

    public GameObject healthBarUI;
    public Slider healthSlider;
    Animator anim;
    public Collider[] hostageColliders;

    private void Start()
    {
        hostageAI = GetComponent<HostageAI>();
        anim = GetComponent<Animator>();
    }

    public override void TakeDamage(float damage)
    {
        if (!isDead) //tekrar tekrar ölmemesi için 
        {
            health -= damage;
            //enemyHealthSlider.value -= deductHealth;
            if (health <= 0)
            {
                Die();
            }
        }
    }

    public void Update()
    {
        if (healthSlider.value < health)
        {
            healthBarUI.SetActive(true);
        }

        if (healthSlider.value <= 0)
        {
            healthBarUI.SetActive(false);
        }
        
        healthSlider.value = health / 100;
    }

    public override void Die()
    {
        isDead = true;
        // hostageAI.EnemyDeathAnim();
        hostageAI.agent.speed = 0f;
        // foreach (var col in hostageColliders)
        //{
        //  col.enabled = false; // cesetlerin collide capsulunu kapatır
        //}
        //enemyHealthSlider.value = 0f;
        Destroy(gameObject , 10);
        anim.SetBool("death" , true);
        anim.SetBool("walk", false);
        anim.SetBool("wait", false);
    }

    public override void TakeHealth(float health)
    {
        
    }

}