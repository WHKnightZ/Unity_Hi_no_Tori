using UnityEngine;

public class HpManager : MonoBehaviour
{
    private static int MAX = 8;

    private static GameObject[] hpBar;
    private static SpriteRenderer[] spriteRenderer;
    private static Sprite hpOn;
    private static Sprite hpOff;
    private static bool isInit = false;
    private static bool isReload = true;

    public static int hp;
    public static int maxHp;

    void Awake()
    {
        if (!isInit)
        {
            isInit = true;
            hpBar = new GameObject[MAX];
            spriteRenderer = new SpriteRenderer[MAX];
            for (int i = 0; i < MAX; i++)
            {
                GameObject obj = transform.GetChild(i).gameObject;
                hpBar[i] = obj;
                spriteRenderer[i] = obj.GetComponent<SpriteRenderer>();
            }
            Sprite[] allSprites = Resources.LoadAll<Sprite>("Sprites/Others/Hp");
            hpOn = allSprites[1];
            hpOff = allSprites[0];
        }
    }

    public static void ResetReload()
    {
        isReload = true;
    }

    public static void Init()
    {
        if (isReload)
        {
            isReload = false;
            hp = maxHp = 5;
            for (int i = 0; i < hp; i++)
            {
                hpBar[i].SetActive(true);
                spriteRenderer[i].sprite = hpOn;
            }
            for (int i = hp; i < maxHp; i++)
            {
                hpBar[i].SetActive(true);
                spriteRenderer[i].sprite = hpOff;
            }
            for (int i = maxHp; i < MAX; i++)
                hpBar[i].SetActive(false);
        }
    }

    public static bool LostHp(int _hp)
    {
        int oldHp = hp - 1;
        hp -= _hp;
        for (int i = oldHp; i >= hp; i--)
            spriteRenderer[i].sprite = hpOff;
        return hp == 0;
    }

    public static void FullHp()
    {
        for (int i = hp; i < maxHp; i++)
            spriteRenderer[i].sprite = hpOn;
        hp = maxHp;
    }

    public static void IncreaseMaxHp()
    {
        if (maxHp < MAX)
        {
            hpBar[maxHp].SetActive(true);
            spriteRenderer[maxHp].sprite = hpOff;
            maxHp++;
        }
    }

}
