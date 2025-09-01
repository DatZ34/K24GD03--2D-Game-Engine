using UnityEngine;
using UnityEngine.UI;
public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] private Material materialBackground;
    [SerializeField] private float scrollSpeed = 0.1f;

    [SerializeField] private float offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        offset += Time.deltaTime * scrollSpeed;
        materialBackground.SetTextureOffset("_MainTex", new Vector2(0f, offset));
    }
}
