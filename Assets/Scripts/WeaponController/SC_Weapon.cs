using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class SC_Weapon : MonoBehaviour
{
    public bool singleFire = false;
    public float fireRate = 0.1f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public int bulletsPerMagazine = 30;
    public int bulletsTotal = 100;
    public float timeToReload = 1.5f;
    public float weaponDamage = 15; //How much damage should this weapon deal
    public AudioClip fireAudio;
    public AudioClip reloadAudio;
    public ParticleSystem shotFlash;

    [HideInInspector]
    public SC_WeaponManager manager;

    float nextFireTime = 0;
    bool canFire = true;
    int bulletsPerMagazineDefault = 0;
    int bulletsTotalDefault = 150;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        bulletsPerMagazineDefault = bulletsPerMagazine;
        bulletsTotalDefault = bulletsTotal;
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        //Make sound 3D
        audioSource.spatialBlend = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && singleFire)
        {
            Fire();
        }
        if (Input.GetMouseButton(0) && !singleFire)
        {
            Fire();
        }
        if (Input.GetKeyDown(KeyCode.R) && canFire)
        {
            if (bulletsTotal > 0)
            {
                StartCoroutine(Reload());
            }
        }
    }

    void Fire()
    {
        if (canFire && !SC_DamageReceiver.Instance.IsOnPause())
        {
            if (Time.time > nextFireTime)
            {
                nextFireTime = Time.time + fireRate;

                if (bulletsPerMagazine > 0)
                {
                    //Point fire point at the current center of Camera
                    Vector3 firePointPointerPosition = manager.playerCamera.transform.position + manager.playerCamera.transform.forward * 100;
                    RaycastHit hit;
                    if (Physics.Raycast(manager.playerCamera.transform.position, manager.playerCamera.transform.forward, out hit, 100))
                    {
                        firePointPointerPosition = hit.point;
                    }
                    firePoint.LookAt(firePointPointerPosition);
                    //Fire
                    GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    SC_Bullet bullet = bulletObject.GetComponent<SC_Bullet>();
                    //Set bullet damage according to weapon damage value
                    bullet.SetDamage(weaponDamage);

                    bulletsPerMagazine--;
                    audioSource.clip = fireAudio;
                    audioSource.Play();
                    shotFlash.Play();
                }
                else
                {
                    if (bulletsTotal > 0)
                    {
                        StartCoroutine(Reload());
                    }
                }
            }
        }
    }

    IEnumerator Reload()
    {
        canFire = false;

        audioSource.clip = reloadAudio;
        audioSource.Play();

        yield return new WaitForSeconds(timeToReload);

        if (bulletsPerMagazine > 0)
        {
            bulletsTotal += bulletsPerMagazine;
        }

        bulletsTotal -= bulletsPerMagazineDefault;

        if (bulletsTotal < 0)
        {
            bulletsPerMagazine = bulletsTotal + bulletsPerMagazineDefault;
            bulletsTotal = 0;
        }
        else
        {
            bulletsPerMagazine = bulletsPerMagazineDefault;
        }

        canFire = true;
    }

    //Called from SC_WeaponManager
    public void ActivateWeapon(bool activate)
    {
        StopAllCoroutines();
        canFire = true;
        gameObject.SetActive(activate);
    }

    public void SetBullets()
    {
        bulletsTotal = bulletsTotalDefault;
    }

    public void IncreaseBullets()
    {
        bulletsTotalDefault += bulletsPerMagazineDefault;
    }
}