using UnityEngine;
using UnityEngine.Tilemaps; // cần import

public class LevelMapManager : MonoBehaviour
{
    [SerializeField] private GameObject[] levelPrefabs; // danh sách prefab ải
    [SerializeField] private Grid grid; 
    private GameObject currentLevel;
    private GameManager GameManager;
    public void LoadLevel(int levelID)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel); 
        }

        if (levelID >= 0 && levelID < levelPrefabs.Length)
        {
            // Lấy GridController trong Grid chính
            var g = grid.GetComponentInChildren<GridController>();

            // Instance prefab dưới Grid
            currentLevel = Instantiate(levelPrefabs[levelID], Vector3.zero, Quaternion.identity, grid.transform);

            // Tìm Tilemap trong prefab
            Tilemap groundTilemap = currentLevel.GetComponentInChildren<Tilemap>();

            // Gán Tilemap vào GridController
            g.GroundMap = groundTilemap;
        }
        else
        {
            Debug.LogWarning("Level ID không hợp lệ: " + levelID);
        }
    }
}
