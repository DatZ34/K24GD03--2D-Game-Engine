using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Thông số cơ bản")]
    [SerializeField] protected int health;
    [SerializeField] protected float moveSpeed = 3f; // tốc độ di chuyển
    [SerializeField] protected float attackRate = 0.8f; // tốc độ bắn
    [SerializeField] protected int damage;
    [SerializeField] public int ScoreValue;
    public bool HasShoot;
    [SerializeField] protected bool isBoss;
    [SerializeField] protected int flyPattern; // các kiểu bay của gà dc đánh số theo 1 , 2, 3 ,..
    [SerializeField] protected float dropRate; // phần trăm rơi đồ
    [SerializeField] protected float dieDelayTime = 1f;

    [Header("Hiệu ứng hình ảnh")]
    [SerializeField] protected GameObject hitEffectPrefab; // hiệu ứng nổ
    [SerializeField] protected GameObject deathEffectPrefab; // hiệu ứng die
    [SerializeField] protected GameObject eggPrefab;
    [SerializeField] protected GameObject legPrefab;
    [SerializeField] protected GameObject presentPrefab;
    [Header("Tùy chỉnh Hiệu ứng")]
    [SerializeField] protected Vector3 hitScale;
    [SerializeField] protected Vector3 deathScale;
    [SerializeField] protected Vector3 eggScale;
    [SerializeField] protected Vector3 legScale;
    [Header("Thành phần Unity gắn kèm")]
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Animator anim;
    [SerializeField] protected AudioSource source;

    [Header("Âm thanh")]
    [SerializeField] protected AudioClip[] hitSound;
    [SerializeField] protected AudioClip[] deathSound;

    protected Vector2 hitpoint;
    protected Vector3 stopPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        source.volume = AudioManager.instance.currentValueSoundEFX;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
    #region Move
    protected virtual void MoveDown()
    {
        rb.linearVelocity = Vector2.down *moveSpeed;
    }
    protected virtual void MovePattern()
    {

        if (Vector3.Distance(transform.position,stopPoint) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position,stopPoint,moveSpeed * Time.deltaTime);
        }
        else
        {
            StopMovement();
        }

    }
    protected virtual void StopMovement()
    {
        rb.linearVelocity = Vector2.zero;
    }
    public void SetStopPoint(Vector3 point)
    {
        stopPoint = point;
    }

    #endregion
    #region attack
    public virtual void Shoot() { }
    protected virtual void StartAttack() { }
    protected virtual void StopAttack() { }
    #endregion
    #region touch
    protected virtual void OnHit(int damage) 
    {
        health -= damage;
        PlayHitEffect();
        CheckHealth();

    }
    protected virtual void OnCollisionEnter2D(Collision2D collision) { }
    protected virtual void OnCollisionExit2D(Collision2D collision) { }
    protected virtual void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            Debug.Log("hitchicken");
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if(bullet != null)
            {
                hitpoint = collision.ClosestPoint(transform.position);
                OnHit(bullet.dame);
                Destroy(collision.gameObject);
            }
        }
    }
    #endregion
    #region  Effect && music
    protected virtual void PlayHitEffect() 
    {
        int randomNum = Random.Range(0, hitSound.Length);
        source.clip = hitSound[randomNum];
        source.Play();
        GameObject hitEffect = Instantiate(hitEffectPrefab,hitpoint,Quaternion.identity);
        hitEffect.transform.localScale = hitScale;
        Destroy(hitEffect, 0.3f);
    }
    protected virtual void PlayDeathEffect() 
    {  
        int randomNum = Random.Range(0, deathSound.Length);
        source.clip = deathSound[randomNum];
        source.Play();
        GameObject deadEffect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        deadEffect.transform.localScale = deathScale;
        Destroy(deadEffect, 0.3f);
    }
    #endregion
    #region Status
    protected virtual void Die() 
    {
        Destroy(gameObject);
    }
    protected virtual void CheckHealth() 
    {
        if(health <= 0)
        {
            PlayDeathEffect();
            Invoke("Die", dieDelayTime);
        }
    }
    protected virtual void EnterRageMode() { } // nếu là boss
    #endregion
    #region Item
    public virtual void DropPresent() { }
    public virtual void DropLeg() { }
    protected virtual void AddScore() { }
    #endregion
}
