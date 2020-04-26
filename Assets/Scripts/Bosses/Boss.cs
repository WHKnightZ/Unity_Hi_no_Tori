using UnityEngine;

public class Boss : MonoBehaviour
{
    public static Vector3[] vectorScale = { new Vector3(-1f, 1f, 1f), new Vector3(1f, 1f, 1f) };
    public static GameObject boss;
    public static GameObject piece;
    public static string tagBullet;
    public static LayerMask layerGround;

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
                SoundEffect.PlaySpawnPiece();
                Instantiate(piece, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    public virtual void Enable()
    {
        enabled = true;
    }

}
