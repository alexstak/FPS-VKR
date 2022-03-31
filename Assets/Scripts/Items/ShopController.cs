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

    void Update()
    {
        Time.timeScale = timer;
        if (ispuse == true)
        {
            timer = 0;
            guipuse = true;

        }
        else if (ispuse == false)
        {
            timer = 1f;
            guipuse = false;

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
                player.ApplyPoints(-20);
                player.SetBullets();
            }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 50f, deltaWidth + 150f, 45f), "Максимальное здоровье"))
            {
                player.ApplyPoints(-20);
                player.ApplyHeal(100);
            }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 0f, deltaWidth + 150f, 45f), "Замедление времени"))
            {

            }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) + 50f, deltaWidth + 150f, 45f), "Второе оружие"))
            {

            }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) + 100f, deltaWidth + 150f, 45f), "Пополнить боезапас"))
            {

            }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) + 150f, deltaWidth + 150f, 45f), "Пополнить здоровье"))
            {

            }
        }
    }
}
