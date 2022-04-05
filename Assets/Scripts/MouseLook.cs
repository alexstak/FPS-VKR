using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // Start is called before the first frame update
    public float mouseSensetivity = 300f;
    public Transform playerbody;

    private float xRotation = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!SC_DamageReceiver.Instance.IsOnPause())
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensetivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensetivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerbody.Rotate(Vector3.up * mouseX);
        }
    }
}
