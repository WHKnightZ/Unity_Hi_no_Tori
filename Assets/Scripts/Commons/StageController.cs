using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] private int music;

    void Start()
    {
        SoundBackground.PlayBGMStage(music);
        Destroy(gameObject);
    }

}
