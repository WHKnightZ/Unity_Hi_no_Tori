using UnityEngine;

public class Boss13 : Boss
{
    public static GameObject bullet;
    public static Sprite[] sprites;

    private SpriteRenderer spriteRenderer;
    private Vector3 spawnBulletPoint;
    private float delayBullet = 2f;
    private int state = 0;
    private bool isShooting = false;
    private float delayAnimation;

    void Start()
    {
        boss = gameObject;
        boss.SetActive(false);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnBulletPoint = transform.position + new Vector3(-1.5f, 0.5f, 0f);
    }

    void Update()
    {
        delayBullet -= Time.deltaTime;
        if (delayBullet < 0f)
        {
            isShooting = true;
            delayBullet = 1.8f;
            delayAnimation = 0.12f;
        }
        else if (isShooting)
        {
            delayAnimation -= Time.deltaTime;
            if (delayAnimation < 0f)
            {
                delayAnimation = 0.12f;
                state++;
                if (state == 2)
                {
                    Instantiate(bullet, spawnBulletPoint, Quaternion.identity);
                    SoundEffect.PlayBulletBoss();
                }
                else if (state == 4)
                {
                    state = 0;
                    isShooting = false;
                }
                spriteRenderer.sprite = sprites[state];
            }
        }
    }
}
