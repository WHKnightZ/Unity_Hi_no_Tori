using UnityEngine;

public class SpawnEnemy
{
    // Max 2 Enemies spawn at a time => use "count" as counter

    public GameObject gameObject;
    public float timer;
    public int index;
    public int count;

    public SpawnEnemy(GameObject gameObject)
    {
        this.gameObject = gameObject;
        index = SpawnEnemyController.count;
        count = 0;
    }

    public void Spawn()
    {
        timer = 1f;
        if (count < 2)
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
