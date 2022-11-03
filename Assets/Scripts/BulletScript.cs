using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Damageable owner;
    public GameObject bulletHole;
    public GameObject explosionEffect;

    public float damage = 10f;
    public float speed = 8f;
    public float lifeDuration = 2f;

    public bool isExplosive;
    public float explosionDamage = 7f;
    public float explosionRadius = 3f;
    public float reduceDamageInAreaCoef = 1;


    private float _lifeTimer;

    private Vector3 _lastRootPosition;

    // Start is called before the first frame update
    void Start()
    {
        _lifeTimer = lifeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        // we increase position by multiplying forward vector with speed
        _lifeTimer -= Time.deltaTime;
        if (_lifeTimer <= 0f)
        {
            Destroy(gameObject);
        }

        // to destroy too many bullet objects
    }

    public void InflictDamage(RaycastHit hit)
    {
        InflictStandardDamage(hit);
        if (isExplosive)
        {
            InflictExplosionDamage(hit);
        }
    }

    private void InflictStandardDamage(RaycastHit hit)
    {
        Damageable damageable = hit.transform.GetComponent<Damageable>() ??
                                hit.transform.GetComponentInParent<Damageable>();
        if (damageable)
        {
            damageable.InflictDamage(damage, false, owner, hit);
        }

        CreateBulletEffect(hit);
    }

    private void InflictExplosionDamage(RaycastHit hit)
    {
        var colls = Physics.OverlapSphere(hit.point, explosionRadius);
        foreach (Collider col in colls)
        {
            var distance = Vector3.Distance(col.transform.position, hit.point);
            var dmg = explosionDamage - (distance * reduceDamageInAreaCoef);
            var damageable = col.GetComponent<Damageable>();
            if (damageable)
            {
                damageable.InflictDamage(dmg, true, owner, hit);
            }
        }
    }

    private void CreateBulletEffect(RaycastHit hit)
    {
        if (isExplosive)
        {
            Instantiate(explosionEffect, hit.point, Quaternion.identity);
        }
        else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // Create bullet hole if it is not an explosive bullet
            Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
        }
    }
}