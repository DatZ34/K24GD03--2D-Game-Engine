using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject CoinPrefab;
    public GameObject TreePrefab;

    public Transform containerCoin;
    public Transform containerTree;
    private int n;
    public int SpeedTree = 5;
    public float countDownSpawnTree = 10f;
    public float repeatingTimeTree = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartInVoke()
    {
        InvokeRepeating("SpawnCoin", 2f, 5f);
        InvokeRepeating("SpawnObstacles", repeatingTimeTree, countDownSpawnTree);
    }
    
    public void StopInVoke()
    {
        CancelInvoke("SpawnCoin");
        CancelInvoke("SpawnObstacles");
    }
    void SpawnCoin()
    {
        Vector3 spawnPos = new Vector3(20f, -2.6f, 0f);
        GameObject coin = Instantiate(CoinPrefab, spawnPos , Quaternion.identity, containerCoin);
        Destroy(coin, 40f);
    }
    void SpawnObstacles()
    {
        n = Random.Range(1, 3);
        for (int i = 0; i < n; i++)
        {
            Vector3 spawnPos = new Vector3(18f + i, 0f, 0f);
            GameObject tree = Instantiate(TreePrefab, spawnPos, Quaternion.identity, containerTree);
            tree.GetComponent<MoveLeft>().speed = SpeedTree;
            Destroy(tree, 40f);

        }
    }
}
