using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    public static PlayerHealth singleton; // PlayerHealth.singleton yazıldıgında tum publiclere erisir
    public float currentHealth;
    public float maxHealth = 100f;

    public Slider healthSlider;
    public Text healthCounter;


    private void Awake()
    {
        singleton = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.value = maxHealth;
        UpdateHealthCounter();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
    }

    public override void TakeDamage(float damage)
    {
        if (currentHealth > 0)
        {
            if (damage >= currentHealth)
            {
                Die();
            }
            else
            {
                currentHealth -= damage;
                healthSlider.value -= damage;
            }

            UpdateHealthCounter();
        }
    }

    public override void TakeHealth(float health)
    {
        if (currentHealth > 0)
        {
            
            currentHealth += health;
            healthSlider.value += health;
            


            UpdateHealthCounter();
        }
    }

    public override void Die()
    {
        currentHealth = 0;
        isDead = true;
        healthSlider.value = 0;
        UpdateHealthCounter();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void UpdateHealthCounter()
    {
        healthCounter.text = Convert.ToInt16(currentHealth).ToString(); // canmiktarı text olarak stringe convert
    }


}