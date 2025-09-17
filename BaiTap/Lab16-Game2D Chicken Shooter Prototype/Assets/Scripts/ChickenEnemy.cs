using UnityEngine;

public class ChickenEnemy : Enemy
{
    protected bool isStoped = false;
    protected override void Update()
    {
        MovePattern();
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        //Debug.Log("Va chạm với: " + collision.gameObject.name);

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
    public  override void Shoot()
    {
        GameObject egg = Instantiate(eggPrefab, transform.position, Quaternion.identity, gameObject.transform);
        egg.transform.localScale = new Vector2(3f, 3f);
        HasShoot = true;
    }
    public override void DropLeg()
    {
        int randomIndex = Random.Range(1, 2);
        for (int i = 0; i < randomIndex; i++)
        {
            GameObject leg = Instantiate(legPrefab, transform.position, Quaternion.identity);
        }

    }
    protected override void Die()
    {
        DropLeg();
        base.Die();

    }
}
