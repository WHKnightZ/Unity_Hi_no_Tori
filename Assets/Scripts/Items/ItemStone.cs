using UnityEngine;

public class ItemStone : MonoBehaviour
{
    private static Vector2 velocity = new Vector2(-3.5f, 0f);
    private static Vector2 vzero = new Vector2(0f, -0.002f);
    public static string tagPlayer;
    public static string tagBullet;

    private GameObject parent;
    private Rigidbody2D rb;
    private bool isMove;

    void Start()
    {
        parent = transform.parent.gameObject;
        parent.transform.parent = SpawnItemController.trans;
        rb = parent.GetComponent<Rigidbody2D>();
        rb.velocity = vzero;
        isMove = false;
        velocity = -velocity;
    }

    void Update()
    {
        if (!isMove)
        {
            if (IsGrounded())
            {
                isMove = true;
                rb.velocity = velocity;
            }
        }
        else
        {
            if (IsGrounded() && CantMove())
            {
                Unstable.CreateStoneInstant(transform.position);
                Destroy(parent);
                return;
            }
        }
        if (transform.position.y < -8.5f)
            Destroy(parent);
    }

    private bool IsGrounded()
    {
        return rb.velocity.y > -0.001f && rb.velocity.y < 0.001f;
    }

    private bool CantMove()
    {
        return rb.velocity.x > -0.001f && rb.velocity.x < 0.001f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tagPlayer))
        {
            Destroy(parent);
            StoneManager.ReloadStone(1);
            SoundEffect.PlayEatStone();
        }
        if (collision.CompareTag(tagBullet))
        {
            velocity = -velocity;
            if (isMove)
            {
                rb.velocity = velocity;
            }
            Destroy(collision.gameObject);
            SoundEffect.PlayShootStone();
        }
    }
}
