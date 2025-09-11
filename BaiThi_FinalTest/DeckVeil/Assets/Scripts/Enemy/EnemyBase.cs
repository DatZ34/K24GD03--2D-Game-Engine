using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class EnemyBase : MonoBehaviour
{
    [Header("Status")]
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
    protected Transform targetPos;
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveRandom();
        ZoneCheck();
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
    public virtual void ChasePlayer(Vector3Int player)
    {

    }
    public virtual void Attack()
    {

    }
    public virtual void ZoneCheck()
    {
        Collider2D hit = Physics2D.OverlapBox(rb.position, new Vector2(25, 25), 0, playerLayer);
        if (hit != null && hit.CompareTag("Player"))
        {
            playerDetected = true;
            targetPos = hit.transform;
        }
        else
        {
            playerDetected = false;
            targetPos = null;
        }

    }
    public virtual void TakeDame(int dame)
    {

    }
    public virtual void CheckHP()
    {

    }
}
