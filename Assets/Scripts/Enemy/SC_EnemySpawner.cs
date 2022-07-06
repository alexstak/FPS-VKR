using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public SC_DamageReceiver player;  
    public Texture crosshairTexture;
    public float spawnInterval = 2; //Spawn new enemy each n seconds
    public int enemiesPerWave = 5; //How many enemies per wave
    public DataManager dataManager;
    public Transform[] spawnPoints;

    float nextSpawnTime = 0;
    int waveNumber = 1;
    bool waitingForWave = true;
    float newWaveTimer = 0;
    int enemiesToEliminate;
    //How many enemies we already eliminated in the current wave
    int enemiesEliminated = 0;
    int totalEnemiesSpawned = 0;
    int points = 5;

    private bool nextChunk = false;
    private bool spawnEnemies = true;
    private bool openShopDoor = false;

    public static SC_EnemySpawner Instance { get; private set; }

    private List<Transform> spawnPointsList = new List<Transform>();

    private void Awake()
    {
        Instance = this;
    }

    public void addSpawnPoint(Transform spawnPoint)
    {
        spawnPointsList.Add(spawnPoint);
        //print(spawnPointsList.Count);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //Wait 10 seconds for new wave to start
        newWaveTimer = 10;
        waitingForWave = true;

        dataManager.LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(openShopDoor)
        {
            openShopDoor = false;
            ChunkPlacer.Instance.OpenDoor();
        }

        if (waitingForWave)
        {
            if (newWaveTimer >= 0)
            {
                newWaveTimer -= Time.deltaTime;
            }
            else if(newWaveTimer <=0 &&  nextChunk)
            {
                //Initialize new wave
                enemiesToEliminate = waveNumber + enemiesPerWave;
                enemiesEliminated = 0;
                totalEnemiesSpawned = 0;
                waitingForWave = false;
                nextChunk = false;
                spawnEnemies = true;
            }
            else
            {
                print("HERE");
            }
        }
        else
        {
            if (Time.time > nextSpawnTime)
            {
                nextSpawnTime = Time.time + spawnInterval;

                //Spawn enemy 
                if (totalEnemiesSpawned < enemiesToEliminate)
                {
                    Transform randomPoint = spawnPointsList[Random.Range(0, 2)]; //spawnPointsList.Count - 1)];

                    GameObject enemy = Instantiate(enemyPrefab, randomPoint.position, Quaternion.identity);
                    SC_NPCEnemy npc = enemy.GetComponent<SC_NPCEnemy>();
                    npc.playerTransform = player.transform;
                    npc.es = this;
                    totalEnemiesSpawned++;
                }
            }
        }

        if (player.playerHP <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
        }
    }

    void OnGUI()
    {
        GUI.Box(new Rect(10, Screen.height - 65, 100, 25), ((int)player.playerPoints).ToString() + " Points");
        GUI.Box(new Rect(10, Screen.height - 35, 100, 25), ((int)player.playerHP).ToString() + " HP");
        GUI.Box(new Rect(Screen.width / 2 - 35, Screen.height - 35, 70, 25), player.weaponManager.selectedWeapon.bulletsPerMagazine.ToString() + "/" + player.weaponManager.selectedWeapon.bulletsTotal.ToString());

        if (player.playerHP <= 0)
        {
            GUI.Box(new Rect(Screen.width / 2 - 75, Screen.height / 2 - 20, 150, 40), "Game Over\nPress 'Space' to Restart");
        }
        else
        {
            GUI.DrawTexture(new Rect(Screen.width / 2 - 3, Screen.height / 2 - 3, 6, 6), crosshairTexture);
        }

        GUI.Box(new Rect(Screen.width / 2 - 50, 10, 100, 25), (enemiesToEliminate - enemiesEliminated).ToString());

        if (waitingForWave)
        {
            GUI.Box(new Rect(Screen.width / 2 - 125, Screen.height / 4 - 12, 250, 25), "Waiting for Wave " + waveNumber.ToString() + ". " + ((int)newWaveTimer).ToString() + " seconds left...");
        }
    }

    public void EnemyEliminated(SC_NPCEnemy enemy)
    {
        enemiesEliminated++;
        player.ApplyPoints(points);
        points++;

        if (enemiesToEliminate - enemiesEliminated <= 0 && spawnEnemies)
        {
            //Start next wave
            newWaveTimer = 10;
            waitingForWave = true;
            waveNumber++;
            ChunkPlacer.Instance.OpenDoor();
            DeleteSpawnPoints(2);
            spawnEnemies = false;

            dataManager.SaveGame();
        }
    }

    public void SetNextChunk(bool logic)
    {
        nextChunk = logic;
    }

    public void DeleteSpawnPoints(int count)
    {
        spawnPointsList.RemoveAt(0);
        spawnPointsList.RemoveAt(0);
    }

    public void OpenShopDoor(bool logic)
    {
        openShopDoor = logic;
    }
}
