using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnoint_script : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform spawnPointTransform;
    void Start()
    {
        SC_EnemySpawner.Instance.addSpawnPoint(spawnPointTransform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
