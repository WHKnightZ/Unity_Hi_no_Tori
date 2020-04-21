using UnityEngine;

public class ItemStones : Item
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isVisible)
        {
            if (collision.CompareTag(tagPlayer))
            {
                Destroy(parent);
                StoneManager.ReloadStone(20);
                SoundEffect.PlayEatStones();
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
