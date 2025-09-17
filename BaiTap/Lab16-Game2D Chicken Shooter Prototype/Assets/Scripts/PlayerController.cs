using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Running game")]
    public bool isAlive = true;
    public int maxEnergy = 20;
    public int energyShoot = 20;
    public float speed = 5f;
    public int point;
    [Header("Variable")]
    [SerializeField] private Transform bulletContainer;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] bulletsA;
    [SerializeField] private GameObject[] bulletsB;
    [SerializeField] private int bulletTypes;
    [SerializeField] private int bulletIndext = 0;
    [SerializeField] private int bulletRayCount = 1;
    [SerializeField] private int currentBullet;
    [SerializeField] private bool canShoot = true;
    [Header("Hiệu ứng")]
    [SerializeField] private GameObject shootEffectPrefab;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip exploSound;
    [Header("Thành phần Unity gắn thêm")]
    private Rigidbody2D rb;
    private Animator anim;
    private AudioSource source;
    [SerializeField]private float h;
    [SerializeField]private float v;
    public static PlayerController instance;
    private void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletContainer.transform.parent = null;
        bulletContainer.localScale = Vector3.one;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        source.volume = AudioManager.instance.currentValueSoundEFX;
    }
    private void FixedUpdate()
    {
        h = Input.GetAxis("Horizontal");

        v = Input.GetAxis("Vertical");
#if UNITY_STANDALONE_WIN || UNITY_WEBGL
        Move();
#endif
        LimitBullet(maxEnergy);
    }
    // Update is called once per frame
    void Update()
    {
#if UNITY_STANDALONE_WIN || UNITY_WEBGL

        switch (bulletTypes)
        {
            case 1:
                Shoot(bulletIndext, bulletRayCount, bulletsB);
                break;
            default:
                Shoot(bulletIndext, bulletRayCount, bulletsA);
                break;

        }
#endif
    }
    void Move()
    {
        rb.linearVelocity = new Vector2 (h* speed, v* speed);
        float targetZ = 0;
        float targetY = 0;
        if(h > 0)
        {
            targetY = 30;
            targetZ = -10;
        }
        if(h < 0)
        {
            targetY = -30;
            targetZ = 10;
        }
        Quaternion targetRotation = Quaternion.Euler(0f, targetY, targetZ);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * speed);
    }
    public void Move4direction(int H, int V)
    {
        Debug.Log($"h : {H} / v : {V}");
        rb.linearVelocity = new Vector2(H * speed, V * speed);
        float targetZ = 0;
        float targetY = 0;
        if (H > 0)
        {
            targetY = 30;
            targetZ = -10;
        }
        if (H < 0)
        {
            targetY = -30;
            targetZ = 10;
        }
        Quaternion targetRotation = Quaternion.Euler(0f, targetY, targetZ);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * speed);
    }
    void Shoot(int index, int BulletRayCount, GameObject[] bullets)
    {
        if (Input.GetMouseButtonDown(1) && canShoot && energyShoot > 0)
        {
            source.clip = shootSound;
            source.Play();
            if (BulletRayCount == 1)
            {
                GameObject bullet = Instantiate(bullets[index], firePoint.position, Quaternion.identity, bulletContainer);
                energyShoot--;
            }
            else if(BulletRayCount == 2)
            {
                Vector2 targetBullet = firePoint.position;
                Vector3 leftPos = new Vector3(targetBullet.x - 0.1f, targetBullet.y, 0f);
                Vector3 rightPos = new Vector3(targetBullet.x + 0.1f, targetBullet.y, 0f);
                GameObject bullet = Instantiate(bullets[index], leftPos, Quaternion.identity,bulletContainer);
                GameObject bullet2 = Instantiate(bullets[index], rightPos, Quaternion.identity,bulletContainer);
            }else if(BulletRayCount >= 3)
            {
                float[] angles = { -15f, 0f, 15f };
                foreach(float angle in angles)
                {
                    Quaternion rotationZ = Quaternion.Euler(0f, 0f, firePoint.eulerAngles.z + angle);
                    GameObject bullet = Instantiate(bullets[index], firePoint.position, rotationZ, bulletContainer);
                }
            }
        }
    }
    public void ShootBTN(int index, int BulletRayCount, GameObject[] bullets)
    {
        if (canShoot && energyShoot > 0)
        {
            source.clip = shootSound;
            source.Play();
            if (BulletRayCount == 1)
            {
                GameObject bullet = Instantiate(bullets[index], firePoint.position, Quaternion.identity, bulletContainer);
                energyShoot--;
            }
            else if(BulletRayCount == 2)
            {
                Vector2 targetBullet = firePoint.position;
                Vector3 leftPos = new Vector3(targetBullet.x - 0.1f, targetBullet.y, 0f);
                Vector3 rightPos = new Vector3(targetBullet.x + 0.1f, targetBullet.y, 0f);
                GameObject bullet = Instantiate(bullets[index], leftPos, Quaternion.identity,bulletContainer);
                GameObject bullet2 = Instantiate(bullets[index], rightPos, Quaternion.identity,bulletContainer);
            }else if(BulletRayCount >= 3)
            {
                float[] angles = { -15f, 0f, 15f };
                foreach(float angle in angles)
                {
                    Quaternion rotationZ = Quaternion.Euler(0f, 0f, firePoint.eulerAngles.z + angle);
                    GameObject bullet = Instantiate(bullets[index], firePoint.position, rotationZ, bulletContainer);
                }
            }
        }
    }
    public void ClickShootBTN()
    {
        switch (bulletTypes)
        {
            case 1:
                ShootBTN(bulletIndext, bulletRayCount, bulletsB);
                break;
            default:
                ShootBTN(bulletIndext, bulletRayCount, bulletsA);
                break;

        }
    }
    void LimitBullet(int MaxBullet)
    {
        int currentBulletCount = bulletContainer.transform.childCount;
        currentBullet = Mathf.Clamp(currentBulletCount, 0, MaxBullet);
        if(currentBullet >= 0 && currentBullet <= MaxBullet * 7 / 10)
        {
            canShoot = true;
        }
        else if(currentBullet == MaxBullet)
        {
            canShoot = false; 
        }
    }
    public void GetPoint(int Point)
    {
        point += Point;
    }
    private void OnDestroy()
    {
        isAlive = false;
    }
    public void ResetStats()
    {
        isAlive = true;
        point = 0;
        bulletIndext = 0;
        bulletRayCount = 1;
    }
}
