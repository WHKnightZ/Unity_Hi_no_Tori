using UnityEngine;

public class SoundBackground : MonoBehaviour
{
    private static bool isInit = false;
    private static AudioSource audioSource;
    private static AudioClip[] audioBGMStage;
    private static AudioClip audioBGMBoss;

    void Start()
    {
        if (!isInit)
        {
            isInit = true;
            audioSource = GetComponent<AudioSource>();
            audioBGMStage = new AudioClip[3];
            audioBGMStage[0] = Resources.Load<AudioClip>("Sounds/BGM01");
            audioBGMStage[1] = Resources.Load<AudioClip>("Sounds/BGM02");
            audioBGMStage[2] = Resources.Load<AudioClip>("Sounds/BGM03");
            audioBGMBoss = Resources.Load<AudioClip>("Sounds/BGMBoss");
        }
    }

    public static void PlayBGMStage(int n)
    {
        audioSource.clip = audioBGMStage[n];
        audioSource.Play();
    }

    public static void PlayBGMBoss()
    {
        audioSource.clip = audioBGMBoss;
        audioSource.Play();
    }

    public static void Stop()
    {
        audioSource.Stop();
    }
}
