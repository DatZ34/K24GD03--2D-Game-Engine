using UnityEngine;
using UnityEngine.UI;

public class CardBase : MonoBehaviour
{
    public string cardName;
    public string description;
    public int manaCost;
    public int effectValue;
    public Sprite cardImage;
    public bool immediately;
    public bool hasChosen;
    [Header("UI Controls")]
    [SerializeField] protected Button card_btn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        card_btn.onClick.AddListener(HasChosen);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void ActivateEffect(GameObject target)
    {
        
        Debug.Log(cardName + " activated on " + target.name);
    }
    public virtual void ActivateEffectImmeately()
    {
        
        Debug.Log(cardName + " activated now ");
        Destroy(gameObject);
    }
    public virtual void HasChosen()
    {
        if (!hasChosen)
        {
            Debug.Log("Card Selected: " + cardName);

            hasChosen = !hasChosen;
        }
        else
        {
            Debug.Log("Card UnSelect: " + cardName);
            hasChosen = !hasChosen;
        }
    }
}
