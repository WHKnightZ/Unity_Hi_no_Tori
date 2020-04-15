using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPhoenix : Item
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isVisible)
        {
            if (collision.tag == "Player")
            {
                Destroy(parent);
                // + 1 life
            }
        }
        else
        {
            if (collision.tag == "Bullet")
            {
                isVisible = true;
                spriteRenderer.sprite = sprite;
                Destroy(collision.gameObject);
                SoundEffect.PlayItemShow();
            }
        }
    }
}
