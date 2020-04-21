using UnityEngine;

public class Item : MonoBehaviour
{
    public static string tagPlayer;
    public static string tagBullet;

    public bool isVisible;
    public SpriteRenderer spriteRenderer;
    public Sprite sprite;
    public GameObject parent;

    void Start()
    {
        parent = transform.parent.gameObject;
        parent.transform.parent = SpawnItemController.trans;
        spriteRenderer = parent.GetComponent<SpriteRenderer>();
        isVisible = false;
        sprite = spriteRenderer.sprite;
        spriteRenderer.sprite = SpawnItemController.spriteChest;
    }

    void Update()
    {
        if (transform.position.y < -8.5f)
            Destroy(parent);
    }

}
