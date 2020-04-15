using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Vector3[] vectorScale = { new Vector3(-1f, 1f, 1f), new Vector3(1f, 1f, 1f) };
    public static LayerMask layerGround;

    public int index;

    public void CheckDead()
    {
        if (transform.position.y < -9f || MainCamera.camera.transform.position.x - 12f > transform.position.x)
        {
            SpawnEnemyController.spawnEnemies[index].Kill();
            Destroy(gameObject);
        }
    }

}
