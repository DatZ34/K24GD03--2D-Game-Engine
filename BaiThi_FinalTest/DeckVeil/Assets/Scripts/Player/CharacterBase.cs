using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharacterBase : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] protected float speed = 5;

    public int hp;
    public int mana;
    public int damage;
    public bool canMove;
    public bool canAttack;
    [Header("Running data")]
    public bool PlayerTurn;
    [SerializeField] protected bool hasMoving;
    [SerializeField] protected float horizontal;
    [SerializeField] protected float vertical;
    [SerializeField] protected List<Vector3> path = new List<Vector3> ();
    public bool isNormalMove;
    public bool isDeckMove;
    protected int currentPathIndex;
    [Header("Component Add")]
    [SerializeField] protected Rigidbody2D rb;

    [Header("Class Add")]
    [SerializeField] public GridController grilCtl;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        CombatManager.Instance.RegisterPlayer(this);

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        MoveWASD();

    }
    protected virtual void FixedUpdate()
    {
        if (PlayerTurn)
        {
            if (canMove)
            {
                if (isDeckMove)
                {
                    UseDeckMoveClick();
                }
                if (isNormalMove)
                {
                    NormalMove();
                }
            }
        }
        ShowZone();

    }
    protected virtual void MoveWASD()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        // Chuyển input sang hướng isometric
        Vector2 isoMovement = new Vector2(
            input.x - input.y,       // X hướng isometric
            (input.x + input.y) / 2  // Y hướng isometric
        );
        rb.MovePosition(rb.position + isoMovement * speed * Time.fixedDeltaTime);

    }
    protected virtual void UseDeckMoveClick()
    {
        if (!hasMoving)
        {
            grilCtl.ClearMap(grilCtl.highlightMap);
        }
        Vector3? inputTargetWorldPos = null;
        if(Input.GetMouseButtonDown(0))
        {
            inputTargetWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        
        if(Input.touchCount > 0)
        {
            Touch tound = Input.GetTouch(0);
            if(tound.phase == TouchPhase.Began)
            {
                inputTargetWorldPos = Camera.main.ScreenToWorldPoint(tound.position);
            }
        }
        if (inputTargetWorldPos.HasValue)
        {
            Vector2 inputPos2D = new Vector2(inputTargetWorldPos.Value.x, inputTargetWorldPos.Value.y);
            Vector3Int startCell = grilCtl.GroundMap.WorldToCell(rb.position);

            RaycastHit2D hit = Physics2D.Raycast(inputPos2D, Vector2.zero);
            if(hit.collider != null && hit.collider.GetComponent<TilemapCollider2D>())
            {
                Vector3Int cellPos = grilCtl.GroundMap.WorldToCell(hit.point);
                if(grilCtl.GroundMap.HasTile(cellPos) && grilCtl.GroundMap.HasTile(startCell) && !hasMoving)
                {
                    grilCtl.ShowChosenTile(cellPos);
                    path = grilCtl.FindPath(startCell, cellPos);
                    if(path.Count > 0)
                    {
                        currentPathIndex = 0;
                        hasMoving = true;
                    }
                }
            }
        }
        if(hasMoving && path != null && currentPathIndex < path.Count)
        {
            Vector3 target = path[currentPathIndex];
            rb.position = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);

            if(Vector2.Distance(rb.position, target) < 0.05f)
            {
                currentPathIndex++;
                if(currentPathIndex >= path.Count)
                {
                    hasMoving = false;
                    canMove = false;
                    if(CheckAutoAttack()) isDeckMove = false;

                }
            }
        }


    }
    protected virtual void ShowZone()
    {
        Vector3Int startCell = grilCtl.GroundMap.WorldToCell(rb.position);
        grilCtl.HightLightZonePlayer8Dir(startCell);
    }
    protected virtual bool CheckAutoAttack()
    {
        Vector3Int startCell = grilCtl.GroundMap.WorldToCell(rb.position);
        if (grilCtl.zoneEnemyMap.HasTile(startCell) && path.Count != 0)
        {
            rb.position = path[path.Count - 1];
            if (!canAttack)
            {
                AutoAttack();
                canAttack = true;
                canMove = false;
            }
            return true; // báo đã xử lý
        }
        return false;
    }

    protected virtual void NormalMove()
    {
        Vector3Int startCell = grilCtl.GroundMap.WorldToCell(rb.position);
        if (!hasMoving)
        {
            grilCtl.HightLightMove(startCell, 1);
        }

        Vector3? inputTargetWorldPos = null;
        if (Input.GetMouseButtonDown(0))
        {
            inputTargetWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                inputTargetWorldPos = Camera.main.ScreenToWorldPoint(touch.position);
            }
        }
        if (inputTargetWorldPos.HasValue)
        {
            Vector2 inputPos2D = new Vector2(inputTargetWorldPos.Value.x, inputTargetWorldPos.Value.y);
            RaycastHit2D hit = Physics2D.Raycast(inputPos2D, Vector2.zero);

            if(hit.collider != null && hit.collider.GetComponent<TilemapCollider2D>())
            {
                Vector3Int cellPoss = grilCtl.GroundMap.WorldToCell(hit.point);
                if(grilCtl.GroundMap.HasTile(cellPoss) && grilCtl.GroundMap.HasTile(startCell) && grilCtl.highlightMap.HasTile(cellPoss) && !hasMoving )
                {
                    grilCtl.ShowChosenTile(cellPoss);
                    path = grilCtl.FindPath(startCell, cellPoss);
                    if(path.Count > 0)
                    {
                        currentPathIndex = 0;
                        hasMoving = true;
                    }
                }
            }
        }
        if(hasMoving && path != null && path.Count > currentPathIndex)
        {
            Vector3 target = path[currentPathIndex];
            rb.position = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            if(Vector2.Distance(rb.position, target) < 0.05f)
            {
                currentPathIndex++;
                hasMoving = false;
                canMove = false;
                if (CheckAutoAttack()) isNormalMove = true;

            }
        }
    }
    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            Die();
        }
        Debug.Log("HP: " + hp);
    }

    protected virtual void Die()
    {
        CombatManager.Instance.UnregisterPlayer(this);
        Destroy(gameObject);
        CombatManager.Instance.CheckPlayerLive();
    }
    public void Attack(EnemyBase target)
    {
        Debug.Log("target: " + target);
        CombatManager.Instance.DealDamageEnemy(target, damage);
    }
    public EnemyBase FindClosestEnemy()
    {
        EnemyBase closest = null;
        float minDist = Mathf.Infinity;

        foreach (var enemy in CombatManager.Instance.GetEnemies())
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = enemy;
            }
        }
        return closest;
    }

    public void AutoAttack()
    {
        EnemyBase target = FindClosestEnemy();
        if (target != null)
        {
            Attack(target);
        }
    }
}
