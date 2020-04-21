using UnityEngine;

public class ItemRice : Item
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isVisible)
        {
            if (collision.CompareTag(tagPlayer))
            {
                Destroy(parent);
                HpManager.FullHp();
                SoundEffect.PlayEatRice();
            }
        }
        else
        {
            if (collision.CompareTag(tagBullet))
            {
                isVisible = true;
                spriteRenderer.sprite = sprite;
                Destroy(collision.gameObject);
                SoundEffect.PlayItemShow();
            }
        }
    }
}
