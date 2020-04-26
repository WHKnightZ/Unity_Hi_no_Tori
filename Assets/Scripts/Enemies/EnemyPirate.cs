using UnityEngine;

public class EnemyPirate : Enemy
{
    private static Vector2[] velocity = { new Vector2(-100f, 0f), new Vector2(100f, 0f) };
    private static Vector2[] offset1 = { new Vector2(-0.55f, 0f), new Vector2(0.55f, 0f) };
    private static Vector2[] offset2 = { new Vector2(-0.6f, -1.1f), new Vector2(0.6f, -1.1f) };
    private static Vector2 size = new Vector3(0.4f, 0.4f);
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
                rb.AddForce(velocity[drt]);
            }
        }
        else
        {
            if (IsGrounded())
            {
                Vector2 pos = (Vector2)transform.position;
                if (Physics2D.OverlapBox(pos + offset1[drt], size, 0f, layerGround) != null || Physics2D.OverlapBox(pos + offset2[drt], size, 0f, layerGround) == null)
                {
                    drt = 1 - drt;
                    rb.velocity = Vector2.zero;
                    rb.AddForce(velocity[drt]);
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
