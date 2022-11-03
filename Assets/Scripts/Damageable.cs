using UnityEngine;

public class Damageable : MonoBehaviour
{
    [Tooltip("Multiplier to apply to the received damage")]
    public float damageMultiplier = 1f;

    [Range(0, 1)] [Tooltip("Multiplier to apply to self damage")]
    public float sensibilityToSelfdamage = 0.5f;

    [Tooltip("Multiplier for headshots")] public float headshotMultiplier = 2.5f;

    public GameObject bloodEffect;

    public Health health { get; private set; }

    void Awake()
    {
        // find the health component either at the same level, or higher in the hierarchy
        health = GetComponent<Health>();
        if (!health)
        {
            health = GetComponentInParent<Health>();
        }
    }

    public void InflictDamage(float damage, bool isExplosionDamage, Damageable damageSource, RaycastHit hit)
    {
        if (health)
        {
            var totalDamage = damage;
            var byHimSelf = health == damageSource.health;

            // skip the crit multiplier if it's from an explosion
            if (!isExplosionDamage)
            {
                // Do not let directly shoot himself
                if (byHimSelf)
                {
                    return;
                }
                totalDamage *= damageMultiplier;
            }

            // potentially reduce damages if inflicted by self
            if (byHimSelf)
            {
                totalDamage *= sensibilityToSelfdamage;
            }

            var isHeadShot = hit.transform.CompareTag("Headshot");

            if (isHeadShot)
            {
                totalDamage *= headshotMultiplier;
            }

            health.TakeDamage(totalDamage);
            // apply the damages

            if (isHeadShot && health.health == 0f)
            {
                hit.transform.gameObject.SetActive(false);
            }
        }

        if (bloodEffect)
        {
            var blood = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
            blood.transform.parent = hit.transform;
        }
    }
}