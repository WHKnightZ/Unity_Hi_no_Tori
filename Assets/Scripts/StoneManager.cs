using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneManager : MonoBehaviour
{
    private static int MAX = 2;
    private static int current;

    public static int stone = 20;

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
        }
    }

    public static void ReloadStone(int _stone)
    {
        stone += _stone;
        if (stone > 99)
            stone = 00;
        spriteRenderer[0].sprite = ScoreManager.nums[stone / 10];
        spriteRenderer[1].sprite = ScoreManager.nums[stone % 10];
    }
}
