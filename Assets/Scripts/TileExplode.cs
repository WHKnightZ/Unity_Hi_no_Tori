using UnityEngine;

public class TileExplode : MonoBehaviour
{
    public Vector3 direction;
    private Color color = new Color(1f, 1f, 1f, 1f);
    private float timeLife = 0.15f;
    private float velocity = 4f;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.Translate(direction * velocity * Time.deltaTime);
        color.a = timeLife / 0.2f;
        spriteRenderer.color = color;

        timeLife -= Time.deltaTime;
        if (timeLife < 0f)
            Destroy(gameObject);
    }
}
