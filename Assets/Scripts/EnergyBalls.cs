using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBalls : MonoBehaviour
{
    bool canDamage = true;
    public bool playerInEB = false;
    //EnergyAtack EA;
    //GameObject filtre;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destroy());
        //EA.GetComponent<EnergyAtack>();
        //filtre = GameObject.FindGameObjectWithTag("filtre");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        playerInEB = true;
    //        filtre.SetActive(true);
    //        if (EA.playerInEA == false)
    //        {
    //            filtre.SetActive(true);
    //        }
    //        //burda ekranın önündekinin mesh renderırını kaptacan ama ilk önce ana daleğin kürsisinin içinde olup olmadığını kontrol et
    //        //   Debug.Log("çıktı");
    //    }

    //}

    private void OnTriggerStay(Collider other)
    {
       // Debug.Log("trigger");
        
        if (canDamage && other.tag == "Player")
        {
            StartCoroutine(Attack());
        }
        
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        playerInEB = false;
    //        if (EA.playerInEA == false)
    //        {
    //            filtre.SetActive(false);
    //        }
    //        //burda ekranın önündekinin mesh renderırını kaptacan ama ilk önce ana daleğin kürsisinin içinde olup olmadığını kontrol et
    //        //   Debug.Log("çıktı");
    //    }
    //}

    IEnumerator Attack()
    {
        PlayerHealth.singleton.TakeDamage(5f);
        canDamage = false;
        yield return new WaitForSeconds(1f); // 1 saiyede bir vursun diye
        canDamage = true;
        
    }

    IEnumerator destroy()
    {
        //burda ekranın önündekinin mesh renderırını kaptacan ama ilk önce ana daleğin kürsisinin içinde olup olmadığını kontrol et
        yield return new WaitForSeconds(10f);
        //Debug.LogWarning("destroy");
        Destroy(gameObject);
    }
}
