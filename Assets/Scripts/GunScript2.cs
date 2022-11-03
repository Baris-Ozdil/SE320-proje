using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript2 : MonoBehaviour
{
    public ParticleSystem muzzleflash;

    AudioSource gunAS;
    public AudioClip shootAC;

    RaycastHit hit;
    [SerializeField]
    float damageEnemy = 10f;
    [SerializeField]
    Transform shootPoint;

    [SerializeField]
    int currentAmmo;

    [SerializeField]
    float rateofFire;
    float nextFire = 0;

    [SerializeField]
    float weaponRange;
    void Start()
    {
        muzzleflash.Stop();
    } 
    void Update()
    {
        if(Input.GetButton("Fire1")&& currentAmmo > 0)
        {
            Shoot();
        }
    } 
    void Shoot()
    {
        if(Time.time > nextFire)
        {
            nextFire = Time.time + rateofFire;

            currentAmmo--;

            gunAS.PlayOneShot(shootAC);
            StartCoroutine(WeaponEffects());
            if(Physics.Raycast(shootPoint.position,shootPoint.forward,out hit, weaponRange))
            {
                if(hit.transform.tag == "Enemy")
                {
                    EnemyHealth enemyHealthScript = hit.transform.GetComponent<EnemyHealth>();
                    enemyHealthScript.TakeDamage(damageEnemy);

                }

                else
                {

                }

            }
           
        }


    }
    IEnumerator WeaponEffects()
    {
        muzzleflash.Play();
        yield return new WaitForEndOfFrame();
        muzzleflash.Stop();
    }

}

