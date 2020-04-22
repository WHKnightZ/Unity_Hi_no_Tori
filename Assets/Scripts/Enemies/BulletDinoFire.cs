using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDinoFire : EnemyBullet
{
    private Vector3[] velocityBase = { new Vector3(-6f, 0f, 0f), new Vector3(6f, 0f, 0f) };
    private Vector3 velocity;

    public void Setup(int drt)
    {
        transform.localScale = vectorScale[drt];
        velocity = velocityBase[drt];
    }

    void Update()
    {
        transform.Translate(velocity * Time.deltaTime);
        CheckDead();
    }

    public override void CheckDead()
    {
        float camLeft = MainCamera.cam.transform.position.x - 12f;
        float camRight = camLeft + 24f;
        if (camLeft > transform.position.x || transform.position.x > camRight)
            Destroy(gameObject);
    }
}
