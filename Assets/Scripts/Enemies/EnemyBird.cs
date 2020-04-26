using UnityEngine;

public class EnemyBird : Enemy
{
    private static float[] velocityHon = { -3f, 3f };
    private static Vector2[] offset = { new Vector2(-0.6f, 0f), new Vector2(0.6f, 0f) };
    private static Vector2 size = new Vector3(0.4f, 0.4f);

    private int drtHon, drtVer;
    private Rigidbody2D rb;
    private float velocityVer = 0f;
    private float acceleration = -6f;

    void Start()
    {
        drtHon = gameObject.transform.localScale.x > 0f ? 1 : 0;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(velocityHon[drtHon], 0f);
        drtVer = 0;
    }

    void Update()
    {
        Vector2 pos = (Vector2)transform.position;
        if (Physics2D.OverlapBox(pos + offset[drtHon], size, 0f, layerGround) != null)
        {
            drtHon = 1 - drtHon;
            transform.localScale = vectorScale[drtHon];
        }

        velocityVer += acceleration * Time.deltaTime;
        if ((drtVer == 0 && velocityVer < -4f) || (drtVer == 1 && velocityVer > 4f))
        {
            drtVer = 1 - drtVer;
            acceleration = -acceleration;
        }

        rb.velocity = new Vector2(velocityHon[drtHon], velocityVer);
        CheckDead();
    }

    public override void CheckDead()
    {
        float camLeft = MainCamera.cam.transform.position.x - 12f;
        float camRight = camLeft + 33f;
        if (camLeft > transform.position.x || transform.position.x > camRight)
        {
            SpawnEnemyController.spawnEnemies[index].Kill();
            Destroy(gameObject);
        }
    }
}
