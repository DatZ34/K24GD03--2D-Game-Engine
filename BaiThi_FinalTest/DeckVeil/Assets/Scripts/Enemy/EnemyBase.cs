using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class EnemyBase : MonoBehaviour
{
    [Header("Status")]
    public int ZoneIndexRange;
    public int moveRange;
    public float speed = 5;
    public int heath = 100;
    public int dame = 5;
    public bool attack; // set tấn công trong lượt
    public int detectionRange = 3; // biến tùy chỉnh phạm vi zone
    public bool canMove;
    [Header("Enemy Type")]
    [SerializeField] protected bool normalEnemy;
    [SerializeField] protected bool eliteEnemy;
    [SerializeField] protected bool bossEnemy;
    [Header("Running status")]
    [SerializeField] protected Transform targetPos;
    public bool isMoving;
    public bool enemyTurn;
    public bool playerDetected;
    [SerializeField] protected List<Vector3> path = new List<Vector3> ();
    [SerializeField] protected int currentPathIndex;
    [Header("Add")]
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected BoxCollider2D bx;
    [SerializeField] protected LayerMask playerLayer;
    [Header("Class add")]
    [SerializeField] protected GridController gridCL; //script

    private Vector3Int[] directions = new Vector3Int[]
    {
        new Vector3Int(1,0, 0),
        new Vector3Int(-1,0, 0),
        new Vector3Int(0,1,0),
        new Vector3Int(0,-1,0)
    };
    private Vector3Int[] direction8 = new Vector3Int[]
{
        new Vector3Int(1,0, 0),
        new Vector3Int(-1,0, 0),
        new Vector3Int(0,1,0),
        new Vector3Int(0,-1,0),
        new Vector3Int(1,-1, 0),
        new Vector3Int(-1,1, 0),
        new Vector3Int(1,1,0),
        new Vector3Int(-1,-1,0)
    };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetected)
        {
            ChasePlayer(gridCL.GroundMap.WorldToCell(targetPos.position), moveRange);
        }
        else
        {
            MoveRandom();
            ZoneCheck();
        }
    }

    public virtual void MoveRandom()
    {
        if (canMove)
        {
            if (!isMoving)
            {
                int randomN = Random.Range(0, 4);

                Vector3Int startCell = gridCL.GroundMap.WorldToCell(rb.position);
                Vector3Int endCell = startCell + directions[randomN];

                if (gridCL.GroundMap.HasTile(endCell))
                {
                    path = gridCL.FindPath(startCell, endCell);
                    if (path.Count > 0)
                    {
                        currentPathIndex = 0;
                        isMoving = true;
                    }
                    //Vector3 target = gridCL.GroundMap.GetCellCenterWorld(endCell); // luôn về tâm tile
                    //rb.position = Vector2.MoveTowards(rb.position, target, speed * Time.deltaTime);

                }
            }
            if (isMoving && path != null && path.Count > currentPathIndex)
            {
                Vector3 target = path[currentPathIndex];
                rb.position = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                if (Vector2.Distance(rb.position, target) < 0.05f)
                {
                    currentPathIndex++;
                    isMoving = false;
                    canMove = false;
                }
            }
        }
    }
    public virtual void ChasePlayer(Vector3Int playerCell, int moveRange)
    {

        if (canMove)
        {

            // Lấy cell hiện tại của enemy
            Vector3Int startCell = gridCL.GroundMap.WorldToCell(rb.position);
            //Vector3 targetPlayer = gridCL.GroundMap.GetCellCenterWorld(playerCell);
            //// Kiểm tra nếu enemy đang ở 8 ô kề hoặc cùng ô player
            //if (startCell == targetPlayer)
            //{
            //    // Đang cùng ô với player
            //    isMoving = false;
            //    canMove = false;
            //    return;
            //}

            //foreach (var dir in direction8)
            //{
            //    Vector3Int neighbor = playerCell + dir;
            //    if (startCell == neighbor)
            //    {
            //        // Enemy đã đứng ngay cạnh player -> không chase
            //        isMoving = false;
            //        canMove = false;
            //        return;
            //    }
            //}
            if (gridCL.GroundMap.HasTile(startCell) && gridCL.GroundMap.HasTile(playerCell) && !isMoving)
            {
                // Tìm path
                path = gridCL.FindPath(startCell, playerCell);
                if (path.Count > 0)
                {
                    currentPathIndex = 0;
                    isMoving = true;
                }
            }
            if (isMoving && path != null && currentPathIndex < path.Count)
            {
                Vector3 target = path[currentPathIndex];
                rb.position = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                if (Vector2.Distance(rb.position, target) < 0.05f)
                {
                    // Nếu chưa đạt số bước tối đa thì tiến sang bước kế
                    if (currentPathIndex < moveRange - 1)
                    {
                        currentPathIndex++;
                    }
                    else
                    {
                        // Đã hoàn thành lượt di chuyển (đạt moveRange hoặc hết path)
                        isMoving = false;
                        canMove = false;

                        // Làm sạch / reset để lần sau có thể tính lại
                        path.Clear();
                        currentPathIndex = 0;
                    }
                }
            }
        }

    }



    public virtual void Attack()
    {

    }
    public virtual void ZoneCheck() // Vẫn còn bug bị nở Hướng Vevtor.Down range + 1
    {
        Vector3Int startCel = gridCL.GroundMap.WorldToCell(rb.position);
        for(int x = -ZoneIndexRange; x <= ZoneIndexRange; x++)
        {
            for(int y = - ZoneIndexRange; y <= ZoneIndexRange; y++)
            {
                Vector3Int checkCell = new Vector3Int(startCel.x + x, startCel.y + y, startCel.z);

                if (gridCL.GroundMap.HasTile(checkCell))
                {
                    Vector3 worldPosCheck = gridCL.GroundMap.GetCellCenterWorld(checkCell);
                    Collider2D hit = Physics2D.OverlapPoint(worldPosCheck, playerLayer);
                    if (hit != null && hit.CompareTag("Player"))
                    {
                        playerDetected = true;
                        targetPos = hit.transform;
                        return;
                    }

                }
            }
        }
        playerDetected = false;
        targetPos = null;

    }
    public virtual void TakeDame(int dame)
    {

    }
    public virtual void CheckHP()
    {

    }
}
