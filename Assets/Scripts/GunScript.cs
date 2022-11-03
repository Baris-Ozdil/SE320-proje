using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    //mermi 
    public GameObject bullet;

    public Text bulletsLeftText;

    public Text magazineSizeText;
    Animator anim;
    //Audio
    AudioSource gunAS;
    AudioSource reloadAS;
    public AudioClip shootAC;
    public AudioClip reloadAC;
    public AudioClip changeWeaponsSFX;

    //merminin gücü
    public float shootForce, upwardForce;

    //Silahın Ozellikleri mermi kapasitesi dagılım vs
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;

    bool relosstart = true;

    int bulletsLeft, bulletsShot;

    //bools
    bool shooting, readyToShoot, reloading;
    private bool _isWeaponActive = false;
    private float _lastFired;
    public float fireRate = 10f;
    public GameObject M4a1;

    //merminin referans noktası
    public Camera fpsCam;
    public Transform attackPoint;

    //Grafik mesela patlama efekti gibi icin
    public GameObject muzzleFlash;

    void Start()
    {
        fpsCam = Camera.main;
        gunAS = GetComponent<AudioSource>();
        reloadAS = GetComponent<AudioSource>();
        bullet.GetComponent<BulletScript>().owner = gameObject.GetComponentInParent<Damageable>();
        anim = GetComponent<Animator>();
    }

    //reset icin
    public bool allowInvoke = true;

    private void Awake()
    {
        //magazin dolu ise atese baslar
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        if (!_isWeaponActive)
        {
            return;
        }

        MyInput();
    }

    private void MyInput()
    {
        if (allowButtonHold && Time.time - _lastFired > 1 / fireRate)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        //R tusu ile reload
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();
        //Mermi bittiğinde otomatik dolum
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0) Reload();

        //Atıs icin
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = 0;

            Shoot();
            gunAS.PlayOneShot(shootAC);
            UpdateAmmoUI();
            _lastFired = Time.time;
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        //ray noktasını bulur bu kısım silahtan cıkan merminin ekranimizin önüne gitmesi icin yoksa ortaya deil saga dogru gidicekti mermiler
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //ekran ortasina giden bir isin
        RaycastHit hit;

        //birşeye denk geldiyse
        Vector3 targetPoint;

        var bulletScript = bullet.GetComponent<BulletScript>();

        if (Physics.Raycast(ray, out hit))
        {
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

        bulletsLeft--;
        bulletsShot++;

        //resetShotu cagirir her timebetweenshooting suresinde mermi araligi icin 
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }

        //eger birden fazla tap olduysa shootu invokelar timebetweenshots süresinde
        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }

    private void ResetShot()
    {
        //Atıs reseti
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        //reloadStart();
        //reloadFinish();
        reloadAS.PlayOneShot(reloadAC);
        Invoke("ReloadFinished", reloadTime); //reloadfinished i cagırıyor reloadtime kadar bekledikten sonra yani Reload Speedimiz
    }

    private void ReloadFinished()
    {
        //doldurma tamamlandıgında magazine sayısına esitliyor
        bulletsLeft = magazineSize;
        reloading = false;
        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        bulletsLeftText.text = bulletsLeft.ToString();
        magazineSizeText.text = magazineSize.ToString();
    }

    public void ShowWeapon(bool show)
    {
        gameObject.SetActive(show);
        if (show)
        {
            UpdateAmmoUI();
            if (changeWeaponsSFX)
            {
                gunAS.PlayOneShot(changeWeaponsSFX);
            }
        }

        _isWeaponActive = show;
    }

    //IEnumerator reloadStart()
    //{
    //    relosstart = false;
    //    yield return new WaitForSeconds(0.1f); // vuruş animasyonu 43 frame 0.5 yeterli olur 
        
    //    relosstart = true;
    //}
    //private void reloadStart()
    //{
    //    for (int i = 0; i < 10; i++)
    //    {

    //        M4a1.transform.position = new Vector3(M4a1.transform.position.x, M4a1.transform.position.y - 0.03f, M4a1.transform.position.z) * Time.deltaTime;
    //    }
    //}

    //private void reloadFinish()
    //{
    //    for (int i = 0; i < 10; i++)
    //    {

    //        M4a1.transform.position = new Vector3(M4a1.transform.position.x, M4a1.transform.position.y + 0.003f, M4a1.transform.position.z);
    //    }
    //}

}
