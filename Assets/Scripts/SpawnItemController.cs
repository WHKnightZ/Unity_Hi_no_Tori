using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemController : MonoBehaviour
{
    public static Sprite spriteChest;
    public static bool isInit = false;
    public static Transform trans;

    private Camera camera;
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
        camera = MainCamera.camera;
        trans = transform;
        items = new List<GameObject>(GameObject.FindGameObjectsWithTag("Item"));
        foreach (GameObject obj in items)
            obj.SetActive(false);
    }

    void Update()
    {
        foreach (GameObject obj in items)
        {
            if (obj == null)
            { }
            else
            if (camera.transform.position.x + 8f > obj.transform.position.x)
            {
                obj.SetActive(true);
                //items.Remove(obj);
            }
        }
    }
}
