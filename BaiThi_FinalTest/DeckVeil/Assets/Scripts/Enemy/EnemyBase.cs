using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("Status")]
    public float speed = 5;
    public int heath = 100;
    public int dame = 5;
    public bool attack; // set tấn công trong lượt
    public int detectionRange = 3; // biến tùy chỉnh phạm vi zone
    [Header("Enemy Type")]
    [SerializeField] protected bool normalEnemy;
    [SerializeField] protected bool eliteEnemy;
    [SerializeField] protected bool bossEnemy;
    [Header("Running status")]
    public bool hasMoving;
    public bool enemyTurn;
    [Header("Add")]
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected BoxCollider2D bx;
    [Header("Class add")]
    [SerializeField] protected GridController gridCL; //script
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void MoveRandom()
    {
    }
    public virtual void ChasePlayer(Vector3Int player)
    {

    }
    public virtual void Attack()
    {

    }
    public virtual void ZoneCheck()
    {

    }
    public virtual void TakeDame(int dame)
    {

    }
    public virtual void CheckHP()
    {

    }
}
