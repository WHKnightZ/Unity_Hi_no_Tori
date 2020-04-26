using UnityEngine;

public class BulletBoss01 : MonoBehaviour
{
    private static Vector3[] baseVelocity = { new Vector3(-2.4f, -4.8f, 0f), new Vector3(2.4f, -4.8f, 0f) };

    private Vector3 velocity;

    public void Init(int drt)
    {
        velocity = baseVelocity[drt];
    }

    void Update()
    {
        transform.Translate(velocity * Time.deltaTime);
        if (transform.position.y < -8.5f)
            Destroy(gameObject);
    }
}
