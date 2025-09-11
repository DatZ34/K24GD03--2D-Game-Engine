using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DrawStartingHand();
        playCard.onClick.AddListener(PlayCard);
        withdrawCard.onClick.AddListener(WithdrawCard);
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
