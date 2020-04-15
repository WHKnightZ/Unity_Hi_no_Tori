using UnityEngine;
using UnityEngine.Tilemaps;

public class Permanent : MonoBehaviour
{
    public static Tilemap tilemap;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }
}
