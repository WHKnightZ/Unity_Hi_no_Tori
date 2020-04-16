using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static int stage = 1;
    public static Vector3 position;
    public static bool isLocated = false;

    private float delay;

    void Start()
    {
        delay = 2f;
    }

    void Update()
    {
        delay -= Time.deltaTime;
        if (delay < 0)
        {
            SceneManager.LoadScene("Stage" + stage.ToString("00"));
        }
    }
}
