using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
public class GridController : MonoBehaviour
{
    public Tilemap GroundMap;
    public Tilemap highlightMap;
    public Tile highlightTile;
    public Camera mainCam;
    private Vector3Int[] directions = new Vector3Int[]
    {
        new Vector3Int(1,0, 0),
        new Vector3Int(-1,0, 0),
        new Vector3Int(0,1,0),
        new Vector3Int(0,-1,0)
    };
    private Vector3Int[] diagonelDirections = new Vector3Int[] // hướng xéo
    {
        new Vector3Int(1,-1, 0),
        new Vector3Int(-1,1, 0),
        new Vector3Int(1,1,0),
        new Vector3Int(-1,-1,0)
    };
    // biến dùng để bắt Screen màn hình
    [SerializeField] private float scaleFactor = 1.2f;
    [SerializeField] private float yOffset = 2f;
    private enum ScreenOrientationType { Portrait, Landscape, Square}
    private ScreenOrientationType currentOrientation;
    private ScreenOrientationType newOrientation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GroundMap.CompressBounds(); // Gom gọn cellBounds chỉ chứa tile có vẽ

        currentOrientation = GetOrientationType();
        HandleOrientationChange(currentOrientation);
    }

    // Update is called once per frame
    void Update()
    {
        CatchOrientation();
    }
    public List<Vector3> FindPath(Vector3Int startCell, Vector3Int endCell)
    {
        Queue<Vector3Int> frontier = new Queue<Vector3Int>();
        frontier.Enqueue(startCell);

        Dictionary<Vector3Int, Vector3Int?> cameFrom = new Dictionary<Vector3Int, Vector3Int?>();
        cameFrom[startCell] = null;

        while (frontier.Count > 0)
        {
            var current = frontier.Dequeue();
            if (current == endCell) break;
            foreach (var dir in directions)
            {
                var next = current + dir;
                if (!GroundMap.HasTile(next)) continue;
                if (!cameFrom.ContainsKey(next))
                {
                    frontier.Enqueue(next);
                    cameFrom[next] = current;
                }
            }
        }
        List<Vector3> path = new List<Vector3>();
        if (!cameFrom.ContainsKey(endCell)) return path;

        Vector3Int cur = endCell;
        while (cur != startCell)
        {
            path.Add(GroundMap.GetCellCenterWorld(cur));
            cur = cameFrom[cur].Value;
        }
        path.Reverse();
        return path;
    }


    public void ClearMap(Tilemap map)
    {
        map.ClearAllTiles();
    }
    public void ShowMove(Vector3Int currentCell) // show 1 line up 2 ô
    {
        ClearMap(highlightMap);
        Vector3Int forward1 = new Vector3Int(currentCell.x, currentCell.y + 1, currentCell.z);
        if (GroundMap.HasTile(forward1))
        {
            highlightMap.SetTile(forward1,highlightTile);
        }
        Vector3Int forward2 = new Vector3Int(currentCell.x, currentCell.y + 2, currentCell.z);
        if(GroundMap.HasTile(forward1) && GroundMap.HasTile(forward2))
        {
            highlightMap.SetTile(forward2, highlightTile);
        }
    }
    public void ShowChosenTile(Vector3Int endCell)
    {
        ClearMap(highlightMap);
        Vector3Int chosen = new Vector3Int(endCell.x, endCell.y, endCell.z);
        if(GroundMap.HasTile(endCell))
        {
            highlightMap.SetTile(chosen,highlightTile);
        }
    }
    public void HightLightMove(Vector3Int startcell, int range = 1) // Show 4 hướng theo range(phạm vi di chuyển)
    {
        ClearMap(highlightMap);
        foreach(var dir in directions)
        {
            for(int i =1; i <= range; i++)
            {
                Vector3Int nextCell = startcell + dir * i;
                if (GroundMap.HasTile(nextCell))
                {
                    highlightMap.SetTile(nextCell, highlightTile);
                }
            }
        }
    }
    void FitCameraToMap()
    {
        BoundsInt cellBounds = GroundMap.cellBounds;

        // Lấy world position của min và max trong cellBounds
        Vector3 min = GroundMap.CellToWorld(cellBounds.min);
        Vector3 max = GroundMap.CellToWorld(cellBounds.max);


        // Tính center
        Vector3 worldCenter = (min + max) / 2f;
        mainCam.transform.position = new Vector3(worldCenter.x, worldCenter.y, -10);

        float mapWidth = cellBounds.size.x * GroundMap.cellSize.x;
        float mapHeight = cellBounds.size.y * GroundMap.cellSize.y;


        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = mapWidth /mapHeight;

        if(screenRatio >= targetRatio)
        {
            mainCam.orthographicSize = mapHeight / 2f;


        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            mainCam.orthographicSize = mapHeight / 2f * differenceInSize;
        }
        // --- 🔹 Tách chế độ ngang/dọc ---
        if (Screen.width > Screen.height) // Landscape
        {
            
            mainCam.orthographicSize *= scaleFactor;

            
            mainCam.transform.position = new Vector3(worldCenter.x, worldCenter.y + yOffset, -10);
        }
    
    }
    ScreenOrientationType GetOrientationType()
    {
        float aspect = (float)Screen.width / Screen.height;
        if (aspect > 1.2f)
            return ScreenOrientationType.Landscape;
        else if (aspect < 0.8f)
            return ScreenOrientationType.Portrait;
        else
            return ScreenOrientationType.Square;
    }
    void HandleOrientationChange(ScreenOrientationType orientation)
    {
        switch(orientation)
        {
            case ScreenOrientationType.Portrait:
                Debug.Log("Đang ở chế độ dọc");
                FitCameraToMap();
                break;
            case ScreenOrientationType.Landscape:
                Debug.Log("Đang ở chế độ ngang");

                FitCameraToMap();
                break;
            case ScreenOrientationType.Square:
                Debug.Log("Đang ở chế độ vuông hoặc ko xác định");
                FitCameraToMap();
                break;

        }
    }
    void CatchOrientation()
    {
        newOrientation = GetOrientationType();
        if (newOrientation != currentOrientation)
        {
            currentOrientation = newOrientation;
            HandleOrientationChange(newOrientation);
        }
    }
}
