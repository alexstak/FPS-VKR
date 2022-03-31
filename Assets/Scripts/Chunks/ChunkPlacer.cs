using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavMeshBuilder = UnityEngine.AI.NavMeshBuilder;

public class ChunkPlacer : MonoBehaviour
{
    public Transform player;
    public Chunk[] chunkPrefabs;
    public Chunk firstChunk;

    private List<Chunk> spawnedChunks = new List<Chunk>();

    void Start()
    {
        spawnedChunks.Add(firstChunk);
    }

    
    void Update()
    {
        //Debug.Log(Vector3.Distance(player.position, spawnedChunks[spawnedChunks.Count - 1].end.position));
        if(Vector3.Distance(player.position, spawnedChunks[spawnedChunks.Count - 1].end.position) < 50f)
        {
            SpawnChunk();
        }
        /*if(player.position.x > spawnedChunks[spawnedChunks.Count - 1].end.position.x - 100f)
        {
            SpawnChunk();
        }*/
    }

    private void SpawnChunk()
    {
        Chunk newChunk = Instantiate(chunkPrefabs[Random.Range(0, chunkPrefabs.Length)]);
        newChunk.transform.position = spawnedChunks[spawnedChunks.Count - 1].end.position - newChunk.begin.localPosition/3.33f;
        spawnedChunks.Add(newChunk);
        SC_EnemySpawner.Instance.DeleteSpawnPoints(2);
        if (spawnedChunks.Count > 3)
        {
            Destroy(spawnedChunks[0].gameObject);
            spawnedChunks.RemoveAt(0);
        }
    }
}
