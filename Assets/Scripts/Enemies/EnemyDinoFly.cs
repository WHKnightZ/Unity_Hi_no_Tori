using UnityEngine;

public class EnemyDinoFly : Enemy
{
    private static float[] velocity = { -3.8f, 3.8f };
    private static Vector2[] offset = { new Vector2(-0.6f, 0f), new Vector2(0.6f, 0f) };
    private static Vector2 size = new Vector3(0.4f, 0.4f);

    private int drt;
    private Rigidbody2D rb;
    private float velocity2 = 0f;
    private float acceleration = -8f;

    void Start()
    {
        drt = gameObject.transform.localScale.x > 0f ? 1 : 0;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(velocity[drt], 0f);
    }

    void Update()
    {
        velocity2 += acceleration * Time.deltaTime;
        if (velocity2 > 4.8f || velocity2 < -4.8f)
            acceleration = -acceleration;
        Vector2 pos = (Vector2)transform.position;
        if (Physics2D.OverlapBox(pos + offset[drt], size, 0f, layerGround) != null)
        {
            drt = 1 - drt;
            transform.localScale = vectorScale[drt];
        }
        rb.velocity = new Vector2(velocity[drt], velocity2);
        CheckDead();
    }

    public override void CheckDead()
    {
        float camLeft = MainCamera.cam.transform.position.x - 12f;
        float camRight = camLeft + 24f;
        if (camLeft > transform.position.x || transform.position.x > camRight)
        {
            SpawnEnemyController.spawnEnemies[index].Kill();
            Destroy(gameObject);
        }
    }
}
