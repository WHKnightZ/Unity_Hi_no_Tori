using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public static Camera cam;

    private static GameObject instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = gameObject;
            GameObject.DontDestroyOnLoad(gameObject);
            cam = GetComponent<Camera>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
