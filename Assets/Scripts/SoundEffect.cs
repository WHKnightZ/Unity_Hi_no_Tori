using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public static AudioSource audioSource;

    private static bool isInit = false;
    private static AudioClip audioJump;
    private static AudioClip audioShoot;
    private static AudioClip audioCreateStone;
    private static AudioClip audioEatMoney;
    private static AudioClip audioEatRice;
    private static AudioClip audioEatStone;
    private static AudioClip audioEatStones;
    private static AudioClip audioSpawnStone;
    private static AudioClip audioItemShow;
    private static AudioClip audioShootStone;
    private static AudioClip audioTileExplode;
    private static AudioClip audioLostHp;
    private static AudioClip audioDead;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (!isInit)
        {
            isInit = true;
            audioJump = Resources.Load<AudioClip>("Sounds/Jump");
            audioShoot = Resources.Load<AudioClip>("Sounds/Shoot");
            audioCreateStone = Resources.Load<AudioClip>("Sounds/CreateStone");
            audioEatMoney = Resources.Load<AudioClip>("Sounds/EatMoney");
            audioEatRice = Resources.Load<AudioClip>("Sounds/EatRice");
            audioEatStone = Resources.Load<AudioClip>("Sounds/EatStone");
            audioEatStones = Resources.Load<AudioClip>("Sounds/EatStones");
            audioSpawnStone = Resources.Load<AudioClip>("Sounds/SpawnStone");
            audioItemShow = Resources.Load<AudioClip>("Sounds/ItemShow");
            audioShootStone = Resources.Load<AudioClip>("Sounds/ShootStone");
            audioTileExplode = Resources.Load<AudioClip>("Sounds/TileExplode");
            audioLostHp = Resources.Load<AudioClip>("Sounds/LostHp");
            audioDead = Resources.Load<AudioClip>("Sounds/Dead");
        }
    }

    public static void PlayJump()
    {
        audioSource.PlayOneShot(audioJump);
    }

    public static void PlayShoot()
    {
        audioSource.PlayOneShot(audioShoot);
    }

    public static void PlayCreateStone()
    {
        audioSource.PlayOneShot(audioCreateStone);
    }

    public static void PlayEatMoney()
    {
        audioSource.PlayOneShot(audioEatMoney);
    }

    public static void PlayEatRice()
    {
        audioSource.PlayOneShot(audioEatRice);
    }

    public static void PlayEatStone()
    {
        audioSource.PlayOneShot(audioEatStone);
    }

    public static void PlayEatStones()
    {
        audioSource.PlayOneShot(audioEatStones);
    }

    public static void PlaySpawnStone()
    {
        audioSource.PlayOneShot(audioSpawnStone);
    }

    public static void PlayItemShow()
    {
        audioSource.PlayOneShot(audioItemShow);
    }

    public static void PlayShootStone()
    {
        audioSource.PlayOneShot(audioShootStone);
    }

    public static void PlayTileExplode()
    {
        audioSource.PlayOneShot(audioTileExplode);
    }

    public static void PlayLostHp()
    {
        audioSource.PlayOneShot(audioLostHp);
    }

    public static void PlayDead()
    {
        audioSource.PlayOneShot(audioDead);
    }
}
