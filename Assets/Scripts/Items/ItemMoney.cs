using UnityEngine;

public class ItemMoney : Item
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isVisible)
        {
            if (collision.CompareTag(tagPlayer))
            {
                Destroy(parent);
                ScoreManager.IncreaseScore(10);
                SoundEffect.PlayEatMoney();
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
