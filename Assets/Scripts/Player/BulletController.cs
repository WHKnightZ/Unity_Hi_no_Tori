using UnityEngine;

public class BulletController : MonoBehaviour
{
    public static GameObject bulletExplode;
    public static GameObject stone;
    public static string tagEnemy;

    protected static float v = 12f;

    protected float timeLife = 0.25f;
    public Vector3 direction;
    protected bool isAlive;

    void Start()
    {
        isAlive = true;
    }

    void Update()
    {
        transform.Translate(direction * v * Time.deltaTime);
        var position = transform.position + Vector3.up * 0.1f + direction * 0.5f;
        var position2 = transform.position - Vector3.up * 0.1f + direction * 0.5f;
        var pos = Permanent.tilemap.WorldToCell(position);
        if (Permanent.tilemap.GetTile(pos) != null)
        {
            Destroy(gameObject);
            return;
        }
        pos = Permanent.tilemap.WorldToCell(position2);
        if (Permanent.tilemap.GetTile(pos) != null)
        {
            Destroy(gameObject);
            return;
        }
        pos = Unstable.tilemap.WorldToCell(position);
        if (Unstable.tilemap.GetTile(pos) != null)
        {
            Unstable.DestroyTile(pos);
            Destroy(gameObject);
            return;
        }
        pos = Unstable.tilemap.WorldToCell(position2);
        if (Unstable.tilemap.GetTile(pos) != null)
        {
            Unstable.DestroyTile(pos);
            Destroy(gameObject);
            return;
        }
        timeLife -= Time.deltaTime;
        if (timeLife < 0f)
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GameObject obj = Instantiate(bulletExplode, transform.position + direction * 0.4f, Quaternion.identity);
        Destroy(obj, 0.15f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag(tagEnemy) && isAlive)
        {
            ScoreManager.IncreaseScore(3);
            GameObject parent = collision.gameObject.transform.parent.gameObject;
            SpawnEnemyController.spawnEnemies[parent.GetComponent<Enemy>().index].Kill();
            Instantiate(stone, collision.gameObject.transform.position + Vector3.up * 0.2f, Quaternion.identity);
            Destroy(parent);
            Destroy(gameObject);
            SoundEffect.PlaySpawnStone();
            isAlive = false;
        }
    }
}
