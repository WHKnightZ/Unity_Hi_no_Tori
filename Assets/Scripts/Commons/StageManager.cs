using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static int stage = 1;
    public static int stageNext;
    public static Vector3 position;
    public static bool isLocated = false;

    private float delay;

    void Start()
    {
        MainCamera.cam.transform.position = new Vector3(0f, 0f, -10f);
        HpManager.Init();
        if (StoneManager.stone < 20)
        {
            StoneManager.stone = 20;
            StoneManager.ReloadStone(0);
        }
        delay = 2f;
        SoundBackground.Stop();
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
