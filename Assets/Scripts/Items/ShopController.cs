using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public SC_DamageReceiver player;

    public float timer;
    public bool ispuse;
    public bool guipuse;

    private int deltaWidth = 20;
    private float timeToSlowMotion = 5.0f;
    private bool isSlowMotion = false;

    void Update()
    {
        Time.timeScale = timer;
        if (ispuse == true && !isSlowMotion)
        {
            timer = 0;
            guipuse = true;

        }
        else if (ispuse == false && !isSlowMotion)
        {
            timer = 1f;
            guipuse = false;

        }

        if(Input.GetKeyDown(KeyCode.Q) && player.checkSlowMotion())
        {
            StartCoroutine(SlowMotionMode());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.F) && ispuse == false)
        {
            player.SetOnPause(true);
            ispuse = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.F) && ispuse == false)
        {
            player.SetOnPause(true);
            ispuse = true;
        }
    }

    public void OnGUI()
    {
        if (guipuse == true)
        {
            Cursor.visible = true;// включаем отображение курсора
            Cursor.lockState = CursorLockMode.None;
            if (GUI.Button(new Rect((float)(Screen.width / 2) + 350f, (float)(Screen.height / 2) - 250f, 150f, 45f), "Продолжить"))
            {
                ispuse = false;
                timer = 0;
                Cursor.visible = false;
                player.SetOnPause(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 100f, deltaWidth + 150f, 45f), "Максимальный боезапас"))
            {
                player.ApplyPoints(-100);
                player.IncreaseBullets();
            }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 50f, deltaWidth + 150f, 45f), "Максимальное здоровье"))
            {
                player.ApplyPoints(-100);
                player.IncreaseHP();
            }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 0f, deltaWidth + 150f, 45f), "Замедление времени"))
            {
                player.UnlockSlowMotion();
            }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) + 50f, deltaWidth + 150f, 45f), "Второе оружие"))
            {
                player.ApplyPoints(-200);
                player.UnlockSecondaryWeapon();
            }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) + 100f, deltaWidth + 150f, 45f), "Пополнить боезапас"))
            {
                player.ApplyPoints(-20);
                player.SetBullets();
            }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) + 150f, deltaWidth + 150f, 45f), "Пополнить здоровье"))
            {
                player.ApplyPoints(-20);
                player.RestoreHealth();
            }
        }
    }

    IEnumerator SlowMotionMode()
    {
        isSlowMotion = true;
        timer = 0.5f;

        yield return new WaitForSeconds(timeToSlowMotion);

        timer = 1.0f;       
        isSlowMotion = false;
    }
}
