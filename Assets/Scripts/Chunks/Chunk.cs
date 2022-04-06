using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public Transform begin;
    public Transform end;
    public Transform door;
    public Transform backDoor;

    private bool isDoorOpened = false;
    private bool isBackDoorClosed = false;
    private bool isFirstChunk = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor()
    {
        Destroy(door.gameObject);
        isDoorOpened = true;
    }

    public void CloseBackDoor()
    {
        print("CloseBackDoor");
        if (!isFirstChunk)
        {
            backDoor.gameObject.SetActive(true);
        }
        isBackDoorClosed = true;
    }

    public bool checkDoor()
    {
        return isDoorOpened;
    }

    public bool checkBackDoor()
    {
        return isBackDoorClosed;
    }

    public void setFirstChunk()
    {
        isFirstChunk = true;
    }
}


