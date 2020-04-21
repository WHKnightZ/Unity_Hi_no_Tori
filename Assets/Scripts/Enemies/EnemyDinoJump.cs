using UnityEngine;

public class EnemyDinoJump : Enemy
{
    private static float[] velocity = { -4.8f, 4.8f };
    private static Vector2 jumpForce = new Vector2(0f, 820f);
    private static Vector2[] offset = { new Vector2(-0.6f, 0f), new Vector2(0.6f, 0f) };
    private static Vector2 size = new Vector3(0.4f, 0.4f);
    private static Vector2 size2 = new Vector3(0.2f, 1.42f);

    public static Sprite[] sprites;

    private int drt;
    private Rigidbody2D rb;
    private BoxCollider2D box;
    private SpriteRenderer spriteRenderer;
    private float delayJump = 0f;
    private bool isFall = true;

    void Start()
    {
        drt = gameObject.transform.localScale.x > 0f ? 1 : 0;
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb.velocity = new Vector2(velocity[drt], 0f);
    }

    void Update()
    {
        delayJump -= Time.deltaTime;
        if (IsGrounded())
        {
            if (delayJump < 0f)
            {
                isFall = false;
                spriteRenderer.sprite = sprites[1];
                delayJump = 0.2f;
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(jumpForce);
            }
        }
        else if (rb.velocity.y < 0f && !isFall)
        {
            isFall = true;
            spriteRenderer.sprite = sprites[0];
        }
        Vector2 pos = (Vector2)transform.position;
        if (Physics2D.OverlapBox(pos + offset[drt], size, 0f, layerGround) != null)
        {
            drt = 1 - drt;
            transform.localScale = vectorScale[drt];
        }
        rb.velocity = new Vector2(velocity[drt], rb.velocity.y);
        CheckDead();
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapBox(box.bounds.center, size2, 0f, layerGround) != null;
    }
}
