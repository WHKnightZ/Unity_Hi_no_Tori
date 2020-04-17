using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static bool isInit = false;
    private static Vector3[] localScale = { new Vector3(-1f, 1f, 1f), new Vector3(1f, 1f, 1f) };
    private static Vector3[] offsetHorizontal = { Vector3.left, Vector3.right };
    private static Vector3[] jumpForce = { new Vector3(0f, 700f, 0f), new Vector3(0f, 220f, 0f) };

    private GameObject background;
    private Rigidbody2D rb;
    private BoxCollider2D box;
    private Camera cam;
    private float minCam, maxCam;

    [SerializeField] private Sprite[] spriteMove;
    [SerializeField] private Sprite spriteJump;
    [SerializeField] private Sprite spriteShoot;
    [SerializeField] private Sprite spriteCrouch;
    [SerializeField] private LayerMask layerEnemy;
    [SerializeField] private GameObject playerDead;
    [SerializeField] private GameObject playerPortal;

    private static GameObject bullet;
    private static GameObject bulletUp;

    private Sprite sprite;
    private SpriteRenderer spriteRenderer;
    private GameObject boundaryStart;

    private float speed = 1800f;
    private int jumpState;
    private int jumpLow;
    private int currentTileX = -99, currentTileY = -99;
    private float delayJump = 0f;
    private float delayShoot = 0f;
    private int drt = 1;
    private float delayAnimationMove = 0f;
    private float delayAnimationShoot = 0f;
    private int stateMove = 0;

    private bool isUp;
    private bool isCrouch;
    private bool isShoot;
    private bool isJump;
    private bool isFall = false;
    private bool isImmune = false;
    private bool isShowSprite = true;
    private float timerImmune;

    private int stone = 20;


    void Start()
    {
        if (!isInit)
        {
            isInit = true;
            bullet = Resources.Load<GameObject>("Prefabs/Bullet");
            bulletUp = Resources.Load<GameObject>("Prefabs/BulletUp");
        }

        if (StageManager.isLocated)
        {
            transform.position = StageManager.position;
        }

        cam = MainCamera.cam;
        boundaryStart = GameObject.Find("BoundaryStart");

        minCam = 0f;
        maxCam = GameObject.Find("BoundaryEnd").transform.position.x - 10f;

        spriteRenderer = GetComponent<SpriteRenderer>();

        background = GameObject.Find("Background");
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
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

        isUp = Input.GetKey(KeyCode.W);
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
        jumpState = -1;
        if (Input.GetKey(KeyCode.J))
            jumpState = 0;
        else if (Input.GetKey(KeyCode.L))
            jumpState = 1;
        if (IsGrounded())
        {
            if (isUp && PortalManager.IsInPortal(transform.position))
            {
                Instantiate(playerPortal, transform.position, Quaternion.identity);
                Destroy(gameObject);
                SoundEffect.PlayPortal();
                return;
            }
            isJump = false;
            var pos = Unstable.tilemap.WorldToCell(transform.position - Vector3.up * 0.3f);
            pos.y -= 1;
            if (jumpState == 1 && isFall && isCrouch)
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
            if (delayJump < 0f && jumpState > -1)
            {
                isJump = true;
                delayJump = 0.1f;
                rb.AddForce(jumpForce[jumpState]);
                SoundEffect.PlayJump();
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
            if (isCrouch && StoneManager.stone > 0)
            {
                StoneManager.ReloadStone(-1);
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
                if (isUp)
                {
                    Instantiate(bulletUp, transform.position + Vector3.up * 0.6f + offsetHorizontal[drt] * 0.4f, Quaternion.identity);
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

        if (!isImmune)
        {
            if (Physics2D.OverlapBox(box.bounds.center, box.bounds.size, 0f, layerEnemy) != null)
                LostHp(1);
        }
        else
        {
            isShowSprite = !isShowSprite;
            if (!isShowSprite)
                spriteRenderer.sprite = null;
            timerImmune -= Time.deltaTime;
            if (timerImmune < 0f)
                isImmune = false;
        }

        if (transform.position.y < -8.5f)
            LostHp(HpManager.hp);

        ReloadCamera();
    }

    bool IsGrounded()
    {
        return rb.velocity.y > -0.002f && rb.velocity.y < 0.002f;
    }

    void ReloadCamera()
    {
        float camPos = Mathf.Clamp(transform.position.x, minCam, maxCam);
        cam.transform.position = new Vector3(camPos, 0f, -10f);
        minCam = camPos;
        boundaryStart.transform.position = new Vector3(camPos - 10f, 0f, 0f);

        float bgPos = camPos / 1.15f;
        if (bgPos + 44f < camPos + 20f)
            bgPos += 22f * (int)((camPos + 20f - bgPos - 44f) / 22f);
        else if (bgPos > camPos)
            bgPos -= 22f * (int)((bgPos - camPos) / 22f + 1);
        background.transform.position = new Vector3(bgPos, 0f, 0f);
    }

    void LostHp(int n)
    {
        if (HpManager.LostHp(n))
        {
            Dead();
            return;
        }
        isImmune = true;
        timerImmune = 1f;
        SoundEffect.PlayLostHp();
    }

    void Dead()
    {
        // Create a new Death Player at this position, has rigid body but not collision
        Instantiate(playerDead, transform.position, Quaternion.identity);
        SoundEffect.PlayDead();
        Destroy(gameObject);
    }

}
