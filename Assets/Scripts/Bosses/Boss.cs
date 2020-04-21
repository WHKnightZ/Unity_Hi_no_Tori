using UnityEngine;

public class Boss : MonoBehaviour
{
    public static GameObject boss;
    public static GameObject piece;
    public static string tagBullet;

    public int hp = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tagBullet))
        {
            Destroy(collision.gameObject);
            SoundEffect.PlayDamageBoss();
            hp--;
            if (hp == 0)
            {
                GameObject obj = Instantiate(piece, transform.position, Quaternion.identity);
                obj.GetComponent<EatPiece>().stage = 1;
                Destroy(gameObject);
            }
        }
    }

}
