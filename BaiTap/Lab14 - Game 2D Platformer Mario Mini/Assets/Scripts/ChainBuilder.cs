using UnityEngine;

public class ChainBuilder : MonoBehaviour
{
    public GameObject chainLinkPrefab;
    public int chainLength = 10;
    public Transform anchorPoint;
    public GameObject Platform_Brown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject previousLink = null;
        for(int i = 0; i < chainLength; i++)
        {
            GameObject link = Instantiate(chainLinkPrefab, anchorPoint.position + new Vector3(0, -i * 0.3f, 0), Quaternion.identity);
            Rigidbody2D rb = link.GetComponent<Rigidbody2D>();

            HingeJoint2D joint = link.GetComponent<HingeJoint2D>();
            if(i == 0)
            {
                joint.connectedBody = null;
                joint.connectedAnchor = anchorPoint.position;
            }else if(i == chainLength - 1)
            {
                joint.connectedBody = Platform_Brown.GetComponent<Rigidbody2D>();
            }
            else
            {
                joint.connectedBody = previousLink.GetComponent<Rigidbody2D>();
            }
            previousLink = link;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
