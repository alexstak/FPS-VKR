using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBag : MonoBehaviour
{
    public SC_Weapon[] weapons;
    public GameObject model;
    public SC_DamageReceiver player;
    public AudioClip ammos;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        //Make sound 3D
        audioSource.spatialBlend = 1f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(DestroyAmmoBag());

        }

    }

    IEnumerator DestroyAmmoBag()
    {
        audioSource.clip = ammos;
        audioSource.Play();

        foreach (SC_Weapon weapon in weapons)
        {
            weapon.SetBullets();
        }

        yield return new WaitForSeconds(1.5f);
        Destroy(model);
    }
}
