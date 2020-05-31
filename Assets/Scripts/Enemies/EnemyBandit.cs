using UnityEngine;

public class EnemyBandit : Enemy
{
    public static GameObject bullet;

    private static float[] velocity = { -2.2f, 2.2f };

    private static Vector2[] offset = { new Vector2(-0.6f, 0f), new Vector2(0.6f, 0f) };
    private static Vector3[] offsetBullet = { new Vector3(-1f, 0.6f), new Vector3(1f, 0.6f) };
    private static Vector2 size = new Vector3(0.4f, 0.4f);

    private int drt;
    private Rigidbody2D rb;
    private Animator animator;
    private float delayShoot = 2.5f;
    private float delayAnimationShoot = 0f;

    void Start()
    {
        drt = gameObject.transform.localScale.x > 0f ? 1 : 0;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (delayAnimationShoot > 0f)
        {
            delayAnimationShoot -= Time.deltaTime;
            if (delayAnimationShoot <= 0f)
                animator.Play("BanditMove");
        }
        else
        {
            delayShoot -= Time.deltaTime;
            if (delayShoot < 0f)
            {
                delayShoot = 2.5f;
                rb.velocity = new Vector2(0f, rb.velocity.y);
                animator.Play("BanditShoot");
                delayAnimationShoot = 0.5f;
                //GameObject obj = Instantiate(bullet, transform.position + offsetBullet[drt], Quaternion.identity);
                //obj.GetComponent<BulletDinoFire>().Setup(drt);
                return;
            }
            if (PlayerController.player != null)
            {
                float distance = PlayerController.player.transform.position.x - transform.position.x;
                if (distance > 0f)
                {
                    if (distance < 0.1f)
                        rb.velocity = new Vector2(0f, rb.velocity.y);
                    else
                    {
                        drt = 1;
                        if (CantMove())
                            rb.velocity = new Vector2(velocity[drt], 15f);
                        else
                            rb.velocity = new Vector2(velocity[drt], rb.velocity.y);
                    }
                }
                else
                {
                    if (distance > -0.1f)
                        rb.velocity = new Vector2(0f, rb.velocity.y);
                    else
                    {
                        drt = 0;
                        if (CantMove())
                            rb.velocity = new Vector2(velocity[drt], 15f);
                        else
                            rb.velocity = new Vector2(velocity[drt], rb.velocity.y);
                    }
                }
                transform.localScale = vectorScale[drt];
            }
            CheckDead();
        }
    }

    public bool CantMove()
    {
        if (!(rb.velocity.y > -0.01f && rb.velocity.y < 0.01f))
            return false;
        Vector2 pos = (Vector2)transform.position;
        if (Physics2D.OverlapBox(pos + offset[drt], size, 0f, layerGround) == null)
            return false;
        return true;
    }
}
