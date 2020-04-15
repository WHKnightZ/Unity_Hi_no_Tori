using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public static Camera camera;

    void Awake()
    {
        camera = GetComponent<Camera>();
    }

}
