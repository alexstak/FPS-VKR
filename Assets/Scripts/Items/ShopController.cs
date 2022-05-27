using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    //public SC_DamageReceiver player;

    public float timer;
    public bool ispuse;
    public bool guipuse;

    private int deltaWidth = 20;
    private float timeToSlowMotion = 5.0f;
    private bool isSlowMotion = false;

    void Update()
    {
        if (!SC_DamageReceiver.Instance.IsOnPauseEsc())
        {
            Time.timeScale = timer;
        }
        if (!SC_DamageReceiver.Instance.IsOnPauseEsc() && ispuse == true && !isSlowMotion)
        {
            timer = 0;
            guipuse = true;

        }
        else if (!SC_DamageReceiver.Instance.IsOnPauseEsc() && ispuse == false && !isSlowMotion)
        {
            timer = 1f;
            guipuse = false;

        }


        if(Input.GetKeyDown(KeyCode.Q) && SC_DamageReceiver.Instance.checkSlowMotion())
        {
            StartCoroutine(SlowMotionMode());
        }
    }

    /*private void OnTriggerStay(Collider other)
    {
        if (!SC_DamageReceiver.Instance.IsOnPauseEsc() && other.tag == "Player"  && ispuse == false)
        {
            SC_DamageReceiver.Instance.SetOnPause(true);
            ispuse = true;
            SC_EnemySpawner.Instance.OpenShopDoor(true);
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (!SC_DamageReceiver.Instance.IsOnPauseEsc() && other.tag == "Player"  && ispuse == false)
        {
            SC_DamageReceiver.Instance.SetOnPause(true);
            ispuse = true;
            SC_EnemySpawner.Instance.OpenShopDoor(true);
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
                SC_DamageReceiver.Instance.SetOnPause(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 100f, deltaWidth + 150f, 45f), "Максимальный боезапас"))
            {
                SC_DamageReceiver.Instance.ApplyPoints(-100);
                SC_DamageReceiver.Instance.IncreaseBullets();
            }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 50f, deltaWidth + 150f, 45f), "Максимальное здоровье"))
            {
                SC_DamageReceiver.Instance.ApplyPoints(-100);
                SC_DamageReceiver.Instance.IncreaseHP();
            }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 0f, deltaWidth + 150f, 45f), "Замедление времени"))
            {
                SC_DamageReceiver.Instance.UnlockSlowMotion();
            }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) + 50f, deltaWidth + 150f, 45f), "Второе оружие"))
            {
                SC_DamageReceiver.Instance.ApplyPoints(-200);
                SC_DamageReceiver.Instance.UnlockSecondaryWeapon();
            }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) + 100f, deltaWidth + 150f, 45f), "Пополнить боезапас"))
            {
                SC_DamageReceiver.Instance.ApplyPoints(-20);
                SC_DamageReceiver.Instance.SetBullets();
            }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) + 150f, deltaWidth + 150f, 45f), "Пополнить здоровье"))
            {
                SC_DamageReceiver.Instance.ApplyPoints(-20);
                SC_DamageReceiver.Instance.RestoreHealth();
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
