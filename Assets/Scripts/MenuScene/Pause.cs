using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public float timer;
    public bool ispuse;
    public bool guipuse;

    public GameObject menu;
    public GameObject settings;

    public static Pause Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }



    void Update()
        {
            Time.timeScale = timer;
            if (Input.GetKeyDown(KeyCode.Escape) && ispuse == false)
            {
                ispuse = true;
                menu.SetActive(true);
                Cursor.visible = true;// включаем отображение курсора
                Cursor.lockState = CursorLockMode.None;
                SC_DamageReceiver.Instance.SetOnPause(true);
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && ispuse == true)
            {
                ispuse = false;
                menu.SetActive(false);
                settings.SetActive(false);
                Cursor.visible = false;
                SC_DamageReceiver.Instance.SetOnPause(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
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

    public void ContinueGame()
    {
        ispuse = false;
        menu.SetActive(false);
        settings.SetActive(false);
        Cursor.visible = false;
        SC_DamageReceiver.Instance.SetOnPause(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
