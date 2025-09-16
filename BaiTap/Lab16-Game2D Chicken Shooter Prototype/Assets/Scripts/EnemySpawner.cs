using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Cài đặt spawn")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyBossDoublePrefab;
    [SerializeField] private GameObject enemyBossPrefab;
    [SerializeField] private float spawnInterval = 30f;
    [SerializeField] private int maxEnemy = 10;

    [Header("Vị trí spawn")]
    [SerializeField] private float spawnYPercent = 1.1f;

    [Header("Vị trí dừng")]
    [SerializeField] private float stopPercent = 0.75f; // điểm dừng trên màn hình

    [Header("Chỗ chứa spawn")]
    [SerializeField] private Transform spawnContainer;
    [SerializeField] public Dictionary<int, Transform> DictionSaveObject;
    private int currentEnemyCount = 0;
    private int currentWave = 0;
    private bool isSpawning = false;
    private bool isSpawn1Line = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DictionSaveObject = new Dictionary<int, Transform>();
        InvokeRepeating("RandomShootEgg", 3, 5);

    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawn1Line)
        {
            CheckEnemyCount();
        }
        if (!isSpawning && spawnContainer.childCount == 0)
        {
            isSpawning = true;
            currentWave++;
            StartCoroutine(SpawnWave(currentWave));
        }
    }
    void SpawnEnemy()
    {
        if (currentEnemyCount >= maxEnemy) return;
        if (spawnContainer.transform.childCount == 0)
        {
            for (int i = 1; i < 10; i ++)
            {
                float XPercent =(float)i / 10; // phần trăm chiều ngang
                float screenWidth = Screen.width;
                float screenHeight = Screen.height;

                // Tọa độ spawn (phía trên màn hình)
                Vector3 screenSpawnPos = new Vector3(screenWidth * XPercent, screenHeight * spawnYPercent, Camera.main.transform.position.z * -1f);
                Vector3 worldSpawnPos = Camera.main.ScreenToWorldPoint(screenSpawnPos);

                GameObject enemyObj = Instantiate(enemyPrefab, worldSpawnPos, Quaternion.identity, spawnContainer);
                Enemy enemy = enemyObj.GetComponent<Enemy>();
                DictionSaveObject[i] = enemyObj.transform;
                //Debug.Log("Dic " + i + " : " + DictionSaveObject[i]);
                if (enemy != null)
                {
                    // Tọa độ điểm dừng (trong màn hình)
                    Vector3 screenStopPoint = new Vector3(screenWidth * XPercent, screenHeight * stopPercent, Camera.main.transform.position.z * -1f);
                    Vector3 worldStopPos = Camera.main.ScreenToWorldPoint(screenStopPoint);
                    enemy.SetStopPoint(worldStopPos);
                }
            }
        }
        else
        {
            for (int i = 1; i < 10; i++)
            {
                if (!DictionSaveObject.ContainsKey(i) || DictionSaveObject[i] == null)
                {
                    SpawnAtKey(i);
                }
                else if (DictionSaveObject[i].parent != spawnContainer)
                {
                    SpawnAtKey(i);
                }
            }
        }
    }
    void CheckEnemyCount()
    {
        currentEnemyCount = spawnContainer.transform.childCount;
        if(currentEnemyCount != maxEnemy)
        {
            SpawnEnemy();
        }
    }
    public void SpawnAtKey(int key)
    {
        float XPercent = (float)key / 10; 
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        Vector3 screenSpawnPos = new Vector3(screenWidth * XPercent, screenHeight * spawnYPercent, Camera.main.transform.position.z * -1f);
        Vector3 worldSpawnPos = Camera.main.ScreenToWorldPoint(screenSpawnPos);

        GameObject enemyObj = Instantiate(enemyPrefab, worldSpawnPos, Quaternion.identity, spawnContainer);
        Enemy enemy = enemyObj.GetComponent<Enemy>();
        if (enemy != null)
        {
            Vector3 screenStopPoint = new Vector3(screenWidth * XPercent, screenHeight * stopPercent, Camera.main.transform.position.z * -1f);
            Vector3 worldStopPos = Camera.main.ScreenToWorldPoint(screenStopPoint);
            enemy.SetStopPoint(worldStopPos);
        }
    }
    public void SpawnTwoRows()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        for (int row = 0; row < 2; row++)
        {
            float rowStopPercent = stopPercent - row * 0.1f; // hàng dưới thấp hơn

            for (int i = 1; i < 10; i++)
            {
                float XPercent = (float)i / 10;
                Vector3 screenSpawnPos = new Vector3(screenWidth * XPercent, screenHeight * spawnYPercent, Camera.main.transform.position.z * -1f);
                Vector3 worldSpawnPos = Camera.main.ScreenToWorldPoint(screenSpawnPos);

                GameObject enemyObj = Instantiate(enemyPrefab, worldSpawnPos, Quaternion.identity, spawnContainer);
                Enemy enemy = enemyObj.GetComponent<Enemy>();
                if (enemy != null)
                {
                    Vector3 screenStopPoint = new Vector3(screenWidth * XPercent, screenHeight * rowStopPercent, Camera.main.transform.position.z * -1f);
                    Vector3 worldStopPos = Camera.main.ScreenToWorldPoint(screenStopPoint);
                    enemy.SetStopPoint(worldStopPos);
                }
            }
        }
    }
    public void SpawnVShape()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        for (int i = 1; i < 10; i++)
        {
            float XPercent = (float)i / 10;
            float offset = Mathf.Abs(5 - i) * 0.05f; // tạo hình chữ V bằng khoảng cách dọc

            float stopY = stopPercent - offset;

            Vector3 screenSpawnPos = new Vector3(screenWidth * XPercent, screenHeight * spawnYPercent, Camera.main.transform.position.z * -1f);
            Vector3 worldSpawnPos = Camera.main.ScreenToWorldPoint(screenSpawnPos);

            GameObject enemyObj = Instantiate(enemyPrefab, worldSpawnPos, Quaternion.identity, spawnContainer);
            Enemy enemy = enemyObj.GetComponent<Enemy>();
            if (enemy != null)
            {
                Vector3 screenStopPoint = new Vector3(screenWidth * XPercent, screenHeight * stopY, Camera.main.transform.position.z * -1f);
                Vector3 worldStopPos = Camera.main.ScreenToWorldPoint(screenStopPoint);
                enemy.SetStopPoint(worldStopPos);
            }
        }
    }
    private IEnumerator<WaitForSeconds> SpawnWave(int wave)
    {
        Debug.Log("Wave " + wave + " bắt đầu");

        switch (wave)
        {
            case 1:
                isSpawn1Line = true;
                SpawnEnemy(); // hàng đơn

                break;
            case 2:
                isSpawn1Line = false;
                SpawnTwoRows(); // 2 hàng
                break;
            case 3:
                isSpawn1Line = false;
                SpawnVShape(); // hình chữ V
                break;
            default:
                SpawnEnemy(); // lặp lại kiểu đơn
                break;
        }

        yield return new WaitForSeconds(spawnInterval);
        isSpawning = false;
    }
    void RandomShootEgg()
    {
        if(spawnContainer != null)
        {
            if(spawnContainer != null && spawnContainer.childCount > 0)
            {
                int randomIndex = Random.Range(0,spawnContainer.childCount);    
                Transform randomChild = spawnContainer.GetChild(randomIndex);

                Enemy enemy = randomChild.GetComponent<Enemy>();
                if(enemy != null)
                {
                    enemy.Shoot();
                }
            }
        }
    }
}
