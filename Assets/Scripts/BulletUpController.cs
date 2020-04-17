using UnityEngine;

public class BulletUpController : BulletController
{
    void Start()
    {
        if (!isInit)
        {
            isInit = true;
            bulletExplode = Resources.Load<GameObject>("Prefabs/BulletExplode");
        }
        isAlive = true;
    }

    void Update()
    {
        transform.Translate(Vector3.up * v * Time.deltaTime);
        var position = transform.position + Vector3.right * 0.1f + Vector3.up * 0.5f;
        var position2 = transform.position - Vector3.right * 0.1f + Vector3.up * 0.5f;
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
        GameObject obj = Instantiate(bulletExplode, transform.position + Vector3.up * 0.4f, Quaternion.identity);
        Destroy(obj, 0.15f);
    }
}
