using UnityEngine;

public class MoveLeft : MonoBehaviour
{

    public float speed = 2f;

    private void FixedUpdate()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

}
