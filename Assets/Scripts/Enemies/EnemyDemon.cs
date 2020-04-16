using UnityEngine;

public class EnemyDemon : Enemy
{
    private static Vector2 force = new Vector2(-250f, 0f);

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(force);
    }

    void Update()
    {
        CheckDead();
    }

}
