using System.Collections.Generic;
using UnityEngine;

public class SpawnItemController : MonoBehaviour
{
    public static Sprite spriteChest;
    public static bool isInit = false;
    public static Transform trans;

    private Camera cam;
    private List<GameObject> items;

    void Awake()
    {
        if (!isInit)
        {
            isInit = true;
            Sprite[] allSprites = Resources.LoadAll<Sprite>("Sprites/Items");
            spriteChest = allSprites[3];
        }
    }

    void Start()
    {
        cam = MainCamera.cam;
        trans = transform;
        items = new List<GameObject>(GameObject.FindGameObjectsWithTag("Item"));
        foreach (GameObject obj in items)
            obj.SetActive(false);
    }

    void Update()
    {
        for (int i = items.Count - 1; i >= 0; i--)
        {
            GameObject obj = items[i];
            if (cam.transform.position.x + 8f > obj.transform.position.x)
            {
                obj.SetActive(true);
                items.RemoveAt(i);
            }
        }
    }
}
