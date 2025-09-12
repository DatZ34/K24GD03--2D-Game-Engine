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
    public int dame = 1;
    public bool attack; // set tấn công trong lượt
    public int detectionRange = 3; // biến tùy chỉnh phạm vi zone
    public bool canMove;
    [Header("Enemy Type")]
    [SerializeField] protected bool normalEnemy;
    [SerializeField] protected bool eliteEnemy;
    [SerializeField] protected bool bossEnemy;
    [Header("Running status")]
    [SerializeField] protected Vector3Int currentTargetPlayerPos;
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
    [SerializeField] public GridController gridCL; //script

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
        CombatManager.Instance.RegisterEnemy(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyTurn)
        {
            ZoneCheck(); // kiểm tra trước khi hành động

            if (playerDetected && targetPos != null)
            {
                ChasePlayer(gridCL.GroundMap.WorldToCell(targetPos.position), moveRange);
            }
            else
            {
                MoveRandom();
            }
        }

        ShowZone(); // hiển thị vùng zone
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
                    enemyTurn = false;
                }
            }
        }
    }
    public virtual void ChasePlayer(Vector3Int playerCell, int moveRange)
    {

        if (canMove)
        {
            // Kiểm tra có player cạnh bên không
            Vector3Int startCell = gridCL.GroundMap.WorldToCell(rb.position);

            if (gridCL.GroundMap.HasTile(startCell) && gridCL.GroundMap.HasTile(playerCell) && !isMoving)
            {
                if (currentTargetPlayerPos != null || currentTargetPlayerPos != playerCell)
                {
                    currentTargetPlayerPos = playerCell;
                    // Tìm path
                    path = gridCL.FindPath(startCell, playerCell);
                }
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
                if (Vector2.Distance(rb.position, target) < 0.2f)
                {
                    
                    // Nếu chưa đạt số bước tối đa thì tiến sang bước kế
                    if (currentPathIndex < moveRange - 1)
                    {
                        currentPathIndex++;
                    }
                    else
                    {
                         // Đã hoàn thành lượt di chuyển
                        isMoving = false;
                        canMove = false;

                        enemyTurn = false;

                        currentPathIndex = 0;
                    }
                }
            }
            // Kiểm tra ô hiện tại có tile trên zonePlayerMap không
            if (gridCL.zonePlayerMap.HasTile(startCell))
            {
                rb.position = path[0];
                // Nếu đã đi hết path
                isMoving = false;
                canMove = false;

                if (!attack) // chỉ tấn công một lần
                {
                    AutoAttack();
                    attack = true; // đánh dấu đã tấn công
                }
                currentPathIndex = 0;

                return;
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
    public void TakeDamage(int dmg)
    {
        heath -= dmg;
        if (heath <= 0)
        {
            Die();
        }
        Debug.Log("HP: " + heath);
    }

    private void Die()
    {
        CombatManager.Instance.UnregisterEnemy(this);
        Destroy(gameObject);
    }
    public void Attack(CharacterBase target)
    {
        Debug.Log("target: " + target);

        CombatManager.Instance.DealDamagePlayer(target, dame);
    }
    public CharacterBase FindClosestplayer()
    {
        CharacterBase closest = null;
        float minDist = Mathf.Infinity;

        foreach (var player in CombatManager.Instance.GetPlayers())
        {
            Debug.Log("player : " + player);
            float dist = Vector3.Distance(transform.position, player.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = player;
            }
        }
        return closest;
    }

    public void AutoAttack()
    {
        CharacterBase target = FindClosestplayer();
        if (target != null)
        {
            Debug.Log("target: " + target);
            Attack(target);
        }
        else
        {
            Debug.Log("target: " + target);

        }
    }
    protected virtual void ShowZone()
    {
        Vector3Int startCell = gridCL.GroundMap.WorldToCell(rb.position);
        gridCL.HightLightZoneEnemy8Dir(startCell);
    }
}
