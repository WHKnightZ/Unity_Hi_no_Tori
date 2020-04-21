using UnityEngine;

public class EnemySnake : Enemy
{
    private static Vector2[] velocity = { new Vector2(-2.2f, 0f), new Vector2(2.2f, 0f) };
    private static Vector2 vzero = new Vector2(0f, -0.002f);

    private int drt;
    private bool isMove;
    private Rigidbody2D rb;

    void Start()
    {
        drt = gameObject.transform.localScale.x > 0f ? 1 : 0;
        isMove = false;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = vzero;
    }

    void Update()
    {
        if (!isMove)
        {
            if (IsGrounded())
            {
                isMove = true;
                rb.velocity = velocity[drt];
            }
        }
        else
        {
            if (IsGrounded())
            {
                if (PlayerController.player != null)
                {
                    float distance = PlayerController.player.transform.position.x - transform.position.x;
                    if (distance > 0f)
                    {
                        if (distance < 0.1f)
                            rb.velocity = Vector2.zero;
                        else
                        {
                            drt = 1;
                            rb.velocity = velocity[drt];
                        }
                    }
                    else
                    {
                        if (distance > -0.1f)
                            rb.velocity = Vector2.zero;
                        else
                        {
                            drt = 0;
                            rb.velocity = velocity[drt];
                        }
                    }
                    transform.localScale = vectorScale[drt];
                }

            }
        }
        CheckDead();
    }

    public bool IsGrounded()
    {
        return rb.velocity.y > -0.001f && rb.velocity.y < 0.001f;
    }
}
