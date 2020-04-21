using UnityEngine;

public class BulletBoss01 : MonoBehaviour
{
    private Vector3 velocity;

    void Start()
    {
        Vector3 playerPosition = (PlayerController.player != null ? PlayerController.player.transform.position : PlayerController.position);
        velocity = (playerPosition - transform.position).normalized * 7f;
    }

    void Update()
    {
        transform.Translate(velocity * Time.deltaTime);
    }
}
