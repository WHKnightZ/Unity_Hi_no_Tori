using UnityEngine;

public class Boss01 : Boss
{
    private Preload.BoolDelegate[] CantMoveHon;
    private Preload.BoolDelegate[] CantMoveVer;

    public static GameObject bullet;
    private static float[] velocityHon = { -3f, 3f };
    private static Vector3 offsetSpawnBullet = new Vector3(0f, -1.6f, 0f);

    private float delayShoot = 1.8f;
    private int drtHon, drtVer = 0;
    private Rigidbody2D rb;
    private float velocityVer = 0f;
    private float acceleration = -6f;

    void Start()
    {
        CantMoveHon = new Preload.BoolDelegate[2] { new Preload.BoolDelegate(CantMoveLeft), new Preload.BoolDelegate(CantMoveRight) };
        CantMoveVer = new Preload.BoolDelegate[2] { new Preload.BoolDelegate(CantMoveDown), new Preload.BoolDelegate(CantMoveUp) };

        boss = gameObject;
        boss.SetActive(false);
        drtHon = gameObject.transform.localScale.x > 0f ? 1 : 0;
        rb = GetComponent<Rigidbody2D>();

        enabled = false;
    }

    void Update()
    {
        delayShoot -= Time.deltaTime;
        if (delayShoot < 0f)
        {
            delayShoot = 1.8f;
            GameObject obj = Instantiate(bullet, transform.position + offsetSpawnBullet, Quaternion.identity);
            obj.GetComponent<BulletBoss01>().Init(0);
            obj = Instantiate(bullet, transform.position + offsetSpawnBullet, Quaternion.identity);
            obj.GetComponent<BulletBoss01>().Init(1);
            SoundEffect.PlayBossShoot();
        }

        if (CantMoveHon[drtHon]())
        {
            drtHon = 1 - drtHon;
            transform.localScale = vectorScale[drtHon];
        }

        velocityVer += acceleration * Time.deltaTime;
        if (CantMoveVer[drtVer]())
        {
            drtVer = 1 - drtVer;
            acceleration = -acceleration;
        }

        rb.velocity = new Vector2(velocityHon[drtHon], velocityVer);
    }

    public override void Enable()
    {
        base.Enable();
        rb.velocity = new Vector2(velocityHon[drtHon], 0f);
    }

    private bool CantMoveLeft()
    {
        if (rb.velocity.x > -0.02f)
            return true;
        return transform.position.x < MainCamera.cam.transform.position.x - 7.5f;
    }

    private bool CantMoveRight()
    {
        if (rb.velocity.x < 0.02f)
            return true;
        return transform.position.x > MainCamera.cam.transform.position.x + 7.5f;
    }

    private bool CantMoveDown()
    {
        return (rb.velocity.y < -4f);
    }

    private bool CantMoveUp()
    {
        return (rb.velocity.y > 4f);
    }

}
