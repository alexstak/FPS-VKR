using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBag : MonoBehaviour
{
    public GameObject model;
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

        SC_WeaponManager.Instance.SetBullets();

        yield return new WaitForSeconds(1.5f);
        Destroy(model);
    }
}
