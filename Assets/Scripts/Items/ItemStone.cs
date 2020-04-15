using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStone : MonoBehaviour
{
    private static Vector2 vzero = new Vector2(0.002f, 0f);
    private static Vector2 vzero2 = new Vector2(0f, -0.002f);

    private Vector2 velocity;
    private GameObject parent;
    private Rigidbody2D rb;
    private bool isMove;

    void Start()
    {
        velocity = new Vector2(-250f, 0f);
        parent = transform.parent.gameObject;
        parent.transform.parent = SpawnItemController.trans;
        rb = parent.GetComponent<Rigidbody2D>();
        rb.velocity = vzero2;
        isMove = false;
    }

    void Update()
    {
        if (!isMove)
        {
            if (IsGrounded())
            {
                isMove = true;
                rb.velocity = -vzero;
                rb.AddForce(velocity);
            }
        }
        else
        {
            if (IsGrounded() && CantMove())
            {
                Unstable.CreateStone(transform.position);
                Destroy(parent);
                return;
            }
        }
        if (transform.position.y < -9f)
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
        if (collision.tag == "Player")
        {
            Destroy(parent);
            // Increase Stone
            SoundEffect.PlayEatStone();
        }
        if (collision.tag == "Bullet")
        {
            velocity = -velocity;
            if (isMove)
            {
                rb.velocity = vzero;
                rb.AddForce(velocity);
            }
            Destroy(collision.gameObject);
            SoundEffect.PlayShootStone();
        }
    }
}
