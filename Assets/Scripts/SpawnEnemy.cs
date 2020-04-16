using UnityEngine;

public class SpawnEnemy
{

    public GameObject gameObject;
    public float timer;
    public int index;
    public int count;
    public int max;
    public float delay;

    public SpawnEnemy(GameObject gameObject)
    {
        this.gameObject = gameObject;
        index = SpawnEnemyController.count;
        Enemy enemy = gameObject.GetComponent<Enemy>();
        max = enemy.max;
        delay = enemy.delay;
        count = 0;
    }

    public void Spawn()
    {
        timer = delay;
        if (count < max)
        {
            GameObject obj = Object.Instantiate(gameObject);
            obj.SetActive(true);
            obj.transform.parent = SpawnEnemyController.trans;
            obj.GetComponent<Enemy>().index = index;
            count++;
        }
    }

    public void Kill()
    {
        count--;
    }
}
