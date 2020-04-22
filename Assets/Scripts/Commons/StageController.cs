using UnityEngine;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{
    [SerializeField] private int music;
    [SerializeField] private int stage;
    [SerializeField] private int stageNext;

    void Awake()
    {
        if (!Preload.isInit)
        {
            StageManager.stage = stage;
            SceneManager.LoadScene("Prepare");
            return;
        }
    }

    void Start()
    {
        SoundBackground.PlayBGMStage(music);
        StageManager.stageNext = stageNext;
        Destroy(gameObject);
    }

}
