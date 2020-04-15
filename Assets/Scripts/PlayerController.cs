using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static bool isInit = false;
    private static Vector3[] localScale = { new Vector3(-1f, 1f, 1f), new Vector3(1f, 1f, 1f) };
    private static Vector3[] offsetHorizontal = { Vector3.left, Vector3.right };

    private GameObject background;
    private Rigidbody2D rb;
    private Camera camera;
    private float minCam, maxCam;

    [SerializeField] private Sprite[] spriteMove;
    [SerializeField] private Sprite spriteJump;
    [SerializeField] private Sprite spriteShoot;
    [SerializeField] private Sprite spriteCrouch;

    private GameObject bullet;
    private GameObject bulletUp;

    private Sprite sprite;
    private SpriteRenderer spriteRenderer;
    private float speed = 1800f;
    private int jumpState;
    private int jumpLow;
    private int currentTileX = -99, currentTileY = -99;
    private float delayJump = 0f;
    private bool isCrouch;
    private bool isShoot;
    private bool isJump;
    private bool isFall = false;
    private float delayShoot = 0f;
    private int drt = 1;
    private float delayAnimationMove = 0f;
    private float delayAnimationShoot = 0f;
    private int stateMove = 0;

    private int life = 3;
    private int maxHp = 5;
    private int hp = 5;
    private int stone = 20;


    void Start()
    {
        if (!isInit)
        {
            isInit = true;
            bullet = Resources.Load<GameObject>("Prefabs/Bullet");
            bulletUp = Resources.Load<GameObject>("Prefabs/BulletUp");
        }

        camera = MainCamera.camera;
        minCam = 0f;
        maxCam = GameObject.Find("Boundary").transform.position.x - 10f;

        spriteRenderer = GetComponent<SpriteRenderer>();

        background = GameObject.Find("Background");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        delayAnimationMove -= Time.deltaTime;
        delayAnimationShoot -= Time.deltaTime;

        if (delayAnimationMove < 0f && delayAnimationShoot < 0f)
            sprite = spriteMove[0];

        delayJump -= Time.deltaTime;
        delayShoot -= Time.deltaTime;

        float offset = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            offset = -speed * Time.deltaTime;
            drt = 0;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            offset = speed * Time.deltaTime;
            drt = 1;
        }

        rb.AddForce(Vector2.right * offset);
        float velocity = Mathf.Clamp(rb.velocity.x * 0.9f, -6f, 6f);
        rb.velocity = new Vector2(velocity, rb.velocity.y);

        if (velocity > 0.5f || velocity < -0.5f)
        {
            if (delayAnimationMove < 0f && delayAnimationShoot < 0f)
            {
                delayAnimationMove = 0.1f;
                stateMove++;
                if (stateMove == 4)
                    stateMove = 0;
                sprite = spriteMove[stateMove];
            }
        }
        transform.localScale = localScale[drt];

        isCrouch = false;
        if (Input.GetKey(KeyCode.S))
            isCrouch = true;
        if (isJump && rb.velocity.y < -0.1f)
            isFall = true;

        isShoot = false;
        if (Input.GetKey(KeyCode.K) && delayShoot < 0f)
            isShoot = true;
        jumpState = 0;
        if (Input.GetKey(KeyCode.J))
            jumpState = 1;
        else if (Input.GetKey(KeyCode.L))
            jumpState = 2;
        if (IsGrounded())
        {
            isJump = false;
            var pos = Unstable.tilemap.WorldToCell(transform.position - Vector3.up * 0.3f);
            pos.y -= 1;
            if (jumpState == 2 && isFall && isCrouch)
            {
                if (pos.x != currentTileX || pos.y != currentTileY)
                {
                    currentTileX = pos.x;
                    currentTileY = pos.y;
                    jumpLow = 3;
                }
                else
                {
                    jumpLow--;
                    if (jumpLow == 0)
                    {
                        currentTileY = -99;
                        Unstable.DestroyTile(pos);
                    }
                }
            }
            if (delayJump < 0f)
            {
                if (jumpState == 2)
                {
                    isJump = true;
                    delayJump = 0.1f;
                    rb.AddForce(Vector2.up * 250f);
                    SoundEffect.PlayJump();
                }
                else if (jumpState == 1)
                {
                    isJump = true;
                    delayJump = 0.1f;
                    rb.AddForce(Vector2.up * 700f);
                    SoundEffect.PlayJump();
                }
            }
            isFall = false;
        }
        else
            isJump = true;

        if (isJump && delayAnimationShoot < 0f)
        {
            sprite = spriteJump;
            delayAnimationMove = 0.1f;
        }

        if (isCrouch)
            sprite = spriteCrouch;

        if (isShoot)
        {
            if (isCrouch)
            {
                delayShoot = 0.3f;
                sprite = spriteCrouch;
                Vector3 pos;
                if (isJump)
                    pos = transform.position - Vector3.up * 1.5f;
                else
                    pos = transform.position - Vector3.up * 0.5f + offsetHorizontal[drt];
                Unstable.CreateStone(pos);
            }
            else
            {
                delayShoot = 0.4f;
                sprite = spriteShoot;
                if (Input.GetKey(KeyCode.W))
                {
                    Instantiate(bulletUp, transform.position + Vector3.up * 0.6f + offsetHorizontal[drt] * 0.43f, Quaternion.identity);
                }
                else
                {
                    GameObject obj = Instantiate(bullet, transform.position + Vector3.up * 0.34f + offsetHorizontal[drt] * 0.43f, Quaternion.identity);
                    obj.transform.localScale = localScale[drt];
                    obj.GetComponent<BulletController>().direction = offsetHorizontal[drt];
                }
                SoundEffect.PlayShoot();
            }
            delayAnimationShoot = 0.2f;
        }
        spriteRenderer.sprite = sprite;
        ReloadCamera();
    }

    bool IsGrounded()
    {
        return rb.velocity.y > -0.001f && rb.velocity.y < 0.001f;
    }

    void ReloadCamera()
    {
        float camPos = Mathf.Clamp(transform.position.x, minCam, maxCam);
        camera.transform.position = new Vector3(camPos, 0f, -10f);

        float bgPos = camPos / 1.1f;
        if (bgPos + 44f < camPos + 20f)
            bgPos += 22f * (int)((camPos + 20f - bgPos - 44f) / 22f);
        else if (bgPos > camPos)
            bgPos -= 22f * (int)((bgPos - camPos) / 22f + 1);
        background.transform.position = new Vector3(bgPos, 0f, 0f);
    }

}
