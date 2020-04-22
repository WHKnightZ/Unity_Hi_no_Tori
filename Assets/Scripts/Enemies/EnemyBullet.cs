using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public static Vector3[] vectorScale = { new Vector3(-1f, 1f, 1f), new Vector3(1f, 1f, 1f) };

    public virtual void CheckDead()
    {
    }
}
