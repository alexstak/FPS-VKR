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
    private List<Chunk> spawnedChunksShop = new List<Chunk>();

    private int shopChunkPerChunks = 0;

    public static ChunkPlacer Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        spawnedChunks.Add(firstChunk);
        SC_EnemySpawner.Instance.SetNextChunk(true);
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
        if (shopChunkPerChunks == 2)
        {
            shopChunkPerChunks = 0;
            Chunk newChunk = Instantiate(chunkPrefabs[0]);
            newChunk.transform.position = spawnedChunks[spawnedChunks.Count - 1].end.position - newChunk.begin.localPosition / 3.33f;
            spawnedChunks.Add(newChunk);
            SC_EnemySpawner.Instance.SetNextChunk(true);
        }
        else
        {
            Chunk newChunk = Instantiate(chunkPrefabs[Random.Range(1, chunkPrefabs.Length)]);
            newChunk.transform.position = spawnedChunks[spawnedChunks.Count - 1].end.position - newChunk.begin.localPosition / 3.33f;
            spawnedChunks.Add(newChunk);
            shopChunkPerChunks++;
            SC_EnemySpawner.Instance.SetNextChunk(true);
        }
        if (spawnedChunks.Count > 3)
        {
            Destroy(spawnedChunks[0].gameObject);
            spawnedChunks.RemoveAt(0);
        }
    }

    public void OpenDoor()
    {
        if (!spawnedChunks[0].checkDoor())
        {
            spawnedChunks[0].OpenDoor();
        }
        else if(!spawnedChunks[1].checkDoor())
        {
            spawnedChunks[1].OpenDoor();
        }
    }
}
