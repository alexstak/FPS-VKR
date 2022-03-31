using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    public int hpHeal = 50;
    public GameObject model;
    public SC_DamageReceiver player;
    public AudioClip healPlayer;

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
            StartCoroutine(DestroyBottle());

        }

    }

    IEnumerator DestroyBottle()
    {
        player.ApplyHeal(hpHeal);
        audioSource.clip = healPlayer;
        audioSource.Play();       
        yield return new WaitForSeconds(1.5f);
        Destroy(model);
    }

}
