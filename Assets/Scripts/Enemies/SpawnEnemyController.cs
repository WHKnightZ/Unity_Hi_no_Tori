using UnityEngine;
using UnityEditor;

public class SpawnEnemyController : MonoBehaviour
{
    public static int maxEnemy = 50;
    public static SpawnEnemy[] spawnEnemies;
    public static int count;
    public static Transform trans;

    private static float camUp = 7.8f;

    private Camera cam;

    void Start()
    {
        cam = MainCamera.cam;
        spawnEnemies = new SpawnEnemy[maxEnemy];
        count = 0;
        trans = transform;
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject obj = transform.GetChild(i).gameObject;
            Add(new SpawnEnemy(obj));
            obj.SetActive(false);
        }

    }

    void Update()
    {
        float posLeft1 = cam.transform.position.x - 8f;
        float posLeft2 = posLeft1 + 18f;
        float posRight = posLeft2 + 10f;
        for (int i = 0; i < count; i++)
        {
            SpawnEnemy spawnEnemy = spawnEnemies[i];
            Vector3 pos = spawnEnemy.gameObject.transform.position;
            float posX = pos.x;
            float posY = pos.y;
            if (posX > posLeft1)
            {
                if ((posLeft2 < posX && posX < posRight) || (posY > camUp && posX < posLeft2))
                {
                    spawnEnemy.timer -= Time.deltaTime;
                    if (spawnEnemy.timer < 0f)
                        spawnEnemy.Spawn();
                }
            }
        }
    }

    public static void Add(SpawnEnemy spawnEnemy)
    {
        spawnEnemies[count] = spawnEnemy;
        count++;
    }
}
