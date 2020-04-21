using UnityEngine;

public class ItemPhoenix : Item
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isVisible)
        {
            if (collision.CompareTag(tagPlayer))
            {
                HpManager.IncreaseMaxHp();
                Destroy(parent);
                SoundEffect.PlayEatPhoenix();
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
