using UnityEngine;

public class ItemRice : Item
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isVisible)
        {
            if (collision.tag == "Player")
            {
                Destroy(parent);
                // Full hp
                SoundEffect.PlayEatRice();
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
