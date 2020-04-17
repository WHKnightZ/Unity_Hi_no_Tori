using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static int MAX = 5;
    private static int current;

    public static Sprite[] nums;

    private static SpriteRenderer[] spriteRenderer;
    private static bool isInit = false;
    private static int[] score = { 0, 0, 0, 0, 0 };

    void Awake()
    {
        if (!isInit)
        {
            isInit = true;
            spriteRenderer = new SpriteRenderer[MAX];
            for (int i = 0; i < MAX; i++)
            {
                spriteRenderer[i] = transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>();
            }
            nums = Resources.LoadAll<Sprite>("Sprites/Others/Nums");
        }
    }

    public static void IncreaseScore(int _score)
    {
        current = MAX - 1;
        do
        {
            score[current] += _score;
            _score = score[current] / 10;
            score[current] %= 10;
            spriteRenderer[current].sprite = nums[score[current]];
            current--;
        } while (_score > 0);
    }

}
