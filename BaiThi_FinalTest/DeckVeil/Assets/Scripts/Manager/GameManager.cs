using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
public class GameManager : MonoBehaviour
{
    [Header("Deck Settings")]
    public GameObject[] cardPrefabs;
    public Transform handPanel; // nơi chứa card trên tay
    public int startHandSize = 2; // số card khi bắt đầu
    public int maxHandSize = 5; // số card tối đa trên tay
    [Header("Running Data")]
    [SerializeField] private List<GameObject> handCards = new List<GameObject>(); // số card hiện trên tay
    [Header("UI Controls")]
    [SerializeField] private Button playCard;
    [SerializeField] private Button withdrawCard;
    [SerializeField] private LevelMapManager gateLevel;
    [Header("Prefabs")]
    [SerializeField] private GameObject mainPlayer;
    [SerializeField] private GameObject enemyNormal;
    [SerializeField] private GameObject eliteEnemy;
    [Header("Container")]
    [SerializeField] private Transform heart;
    [SerializeField] private Transform mana;
    [Header("class add")]
    [SerializeField] private GridController gridCL;

    private GameObject playerInstance;
    private GameObject enemyInstance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        gateLevel.LoadLevel(0);

    }
    void Start()
    {
        SpawnPlayerAndEnemy();
        DrawStartingHand();
        playCard.onClick.AddListener(PlayCard);
        withdrawCard.onClick.AddListener(WithdrawCard);
    }

    // Update is called once per frame
    void Update()
    {
        checkTurnEnemy();
    }
    public void SpawnPlayerAndEnemy()
    {
        // Lấy bounds tổng quát của tilemap
        BoundsInt bounds = gridCL.GroundMap.cellBounds;

        // Biến lưu tọa độ thực sự có tile
        Vector3Int bottomLeftCell = Vector3Int.zero;
        Vector3Int topRightCell = Vector3Int.zero;

        bool foundBottomLeft = false;
        bool foundTopRight = false;

        // Duyệt toàn bộ bounds, tìm ô có tile
        foreach (var pos in bounds.allPositionsWithin)
        {
            if (gridCL.GroundMap.HasTile(pos))
            {
                // Lưu ô trái dưới
                if (!foundBottomLeft || pos.x <= bottomLeftCell.x && pos.y <= bottomLeftCell.y)
                {
                    bottomLeftCell = pos;
                    foundBottomLeft = true;
                }

                // Lưu ô phải trên
                if (!foundTopRight || pos.x >= topRightCell.x && pos.y >= topRightCell.y)
                {
                    topRightCell = pos;
                    foundTopRight = true;
                }
            }
        }

        // Convert ra world position
        Vector3 bottomLeftWorld = gridCL.GroundMap.GetCellCenterWorld(bottomLeftCell);
        Vector3 topRightWorld = gridCL.GroundMap.GetCellCenterWorld(topRightCell);

        // Spawn player tại góc dưới trái
        if (playerInstance == null)
            playerInstance = Instantiate(mainPlayer, bottomLeftWorld, Quaternion.identity);
        var character = playerInstance.GetComponent<CharacterBase>();
        if (character != null)
        {
            character.grilCtl = gridCL;
            character.PlayerTurn = true;
        }
        // Spawn enemy tại góc trên phải
        if (enemyInstance == null)
            enemyInstance = Instantiate(enemyNormal, topRightWorld, Quaternion.identity);
        var enemy = enemyInstance.GetComponent<EnemyBase>();
        if (enemy != null) 
        { 
            enemy.gridCL = gridCL;
        }
    }

    public void DrawStartingHand()
    {
        for(int i = 0;i < startHandSize;i++)
        {
            DrawCard(i);
        }
    }
    public void DrawCard(int index)
    {
        int rand = Random.Range(0,cardPrefabs.Length);
        GameObject card = Instantiate(cardPrefabs[rand], handPanel);
        handCards.Add(card);
    }
    public void WithdrawCard()
    {
        if (handCards.Count >= maxHandSize)
        {
            Debug.Log("[WithdrawCard] vượt quá giới hạn bài trên tay");
            return;
        }

        int rand = Random.Range(0, cardPrefabs.Length);
        GameObject card = Instantiate(cardPrefabs[rand], handPanel);
        handCards.Add(card);
    }
    void PlayCard()
    {
        OnCardSelected(handCards);
    }
    void OnCardSelected(List<GameObject> handcard)
    {
        if(handCards.Count == 0)
        {
            Debug.Log("Không còn bài trên tay");
            return;
        }
        List<GameObject> listRemove = new List<GameObject>();
        foreach(GameObject card in handcard)
        {
            if (card != null)
            {
                CardBase cardbase = card.GetComponent<CardBase>();
                if (cardbase.hasChosen)
                {
                    listRemove.Add(card);
                    cardbase.ActivateEffectImmeately();

                }
                
            }
        }
        foreach(GameObject card in listRemove)
        {
            handcard.Remove(card);
        }
    }
    public void EndTurn()
    {
        if (enemyInstance != null && enemyInstance != null)
        {
            CharacterBase player = playerInstance.GetComponent<CharacterBase>();
            EnemyBase enemy = enemyInstance.GetComponent<EnemyBase>();
            if (!enemy.enemyTurn && player.PlayerTurn)
            {
                player.PlayerTurn = false;

                enemy.enemyTurn = true;
                enemy.canMove = true;
                enemy.attack = false;

            }
        }
    }
    void checkTurnEnemy()
    {
        EnemyBase enemy;
        CharacterBase player;
        if (enemyInstance != null && enemyInstance != null)
        {     
            player = playerInstance.GetComponent<CharacterBase>();
            enemy = enemyInstance.GetComponent<EnemyBase>();
            if (!enemy.enemyTurn)
            {
                player.PlayerTurn = true;
                player.canMove = true;
                player.canAttack = false;
                enemy.enemyTurn = false;
            }
        }

    }
}
