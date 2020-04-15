using UnityEngine;

public class SpawnEnemyController : MonoBehaviour
{
    public static int maxEnemy = 50;
    public static SpawnEnemy[] spawnEnemies;
    public static int count;
    public static Transform trans;

    private Camera camera;
    private float timer = .2f;

    void Start()
    {
        Enemy.layerGround = LayerMask.GetMask("Ground");
        camera = MainCamera.camera;
        spawnEnemies = new SpawnEnemy[maxEnemy];
        count = 0;
        trans = transform;
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in allEnemies)
        {
            Add(new SpawnEnemy(enemy));
            enemy.SetActive(false);
        }
    }

    void Update()
    {
        float posLeft = camera.transform.position.x + 10f;
        float posRight = posLeft + 8f;
        for (int i = 0; i < count; i++)
        {
            float pos = spawnEnemies[i].gameObject.transform.position.x;
            if (posLeft < pos && pos < posRight)
            {
                if (spawnEnemies[i].count == 0)
                    spawnEnemies[i].Spawn();
                else
                {
                    spawnEnemies[i].timer -= Time.deltaTime;
                    if (spawnEnemies[i].timer < 0f)
                        spawnEnemies[i].Spawn();
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
