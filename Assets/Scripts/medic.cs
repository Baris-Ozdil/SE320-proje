using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class medic : MonoBehaviour
{

    private float pHelth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pHelth = (PlayerHealth.singleton.maxHealth - PlayerHealth.singleton.currentHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (PlayerHealth.singleton.maxHealth != PlayerHealth.singleton.currentHealth) 
            {
                PlayerHealth.singleton.TakeHealth(pHelth);
                Destroy(gameObject);
            }
        }
    }
}
