using Unity.VisualScripting;
using UnityEngine;

public class ChickenEnemy : Enemy
{
    protected bool isStoped = false;
    protected override void Update()
    {
        MovePattern();
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            Destroy(gameObject);
        }
    }
    protected override void StopMovement()
    {
        base.StopMovement();
        isStoped = true;
        Invoke("MoveDown", 15f);

    }
    protected override void MoveDown()
    {
        Debug.Log("MoveDown");
        base.MoveDown();
    }
    protected override void MovePattern()
    {
        if (isStoped) return;
        base.MovePattern();

    }
}
