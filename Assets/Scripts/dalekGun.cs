using UnityEngine;
using UnityEngine.UI;

public class dalekGun : MonoBehaviour
{



    //mermi 
    public GameObject bullet;



    //Audio
    AudioSource gunAS;
    public AudioClip shootAC;

    public AudioClip changeWeaponsSFX;

    //merminin gücü
    public float shootForce, upwardForce;

    //Silahın özellikleri 
    public float timeBetweenShooting, spread, timeBetweenShots;
    public int  bulletsPerTap;
    public bool allowButtonHold;

    int  bulletsShot;

    //bools
    public bool serbest = false;
    private float _lastFired;
    public float fireRate = 10f;
    private bool atackGun = true;


    //merminin referans noktası
    public Camera fpsCam;
    public Transform attackPoint;

    //Grafik mesela patlama efekti gibi icin
    public GameObject muzzleFlash;

    public DalekAI dalekai;


    public RaycastHit hit;
    bool rayH ;
    Ray ray;
    public string hitControl = "nice";

    void Start()
    {
        
        gunAS = GetComponent<AudioSource>();
        bullet.GetComponent<BulletScript>().owner = gameObject.GetComponentInParent<Damageable>();

        
        
    }

    private void Update()
    {
        
        atackGun = dalekai.atack;
        

        ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //ekran ortasina giden bir isin
        rayH = Physics.Raycast(ray, out hit,200f);
        
        //if(!(hit.collider.gameObject.tag== null))
        //{
        //    hitControl=hit.collider.gameObject.tag;
        //}

        if (atackGun)
        {
            
            Shoot();
        }
        //if (hit.collider.gameObject.tag != null) 
        //{
        //    hitControl = hit.collider.gameObject.tag;
        //}else if(hit.collider.gameObject.tag == null)
        //{
        //    hitControl = "nice";
        //}

    }

    

    public void Shoot()
    {
        dalekai.atack = false;

        Debug.LogWarning("Shoot çalıştı");

        //ray noktasını bulur bu kısım silahtan cıkan merminin ekranimizin önüne gitmesi icin yoksa ortaya deil saga dogru gidicekti mermiler
        
        //RaycastHit hit;
        
        //birşeye denk geldiyse
        Vector3 targetPoint;

        var bulletScript = bullet.GetComponent<BulletScript>();

        if (rayH)
        {
            if(hit.rigidbody == null || !hit.rigidbody.name.StartsWith("Player"))
            {
                Debug.Log(this.name + " - Player değil ateş etme!");
                return;
            }

            bulletScript.InflictDamage(hit);
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75); //Just a point far away from the player
        }

        

        //Calculate direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //spread icin sacılma
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //sacilma dolasıyla olan yeni gidisat
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); //yeni bir yöne spreadi ekler

        //bulleti baslatir ve currentBulletta saklar baslatilan buleti
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);

        //atıs yonune mermiyi dondurur
        currentBullet.transform.forward = directionWithSpread.normalized;

        //İvme
        currentBullet.GetComponent<Rigidbody>()
            .AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

        //Efekt icin
        if (muzzleFlash != null)
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.LookRotation(directionWithoutSpread));

        
       
        
    }

}
