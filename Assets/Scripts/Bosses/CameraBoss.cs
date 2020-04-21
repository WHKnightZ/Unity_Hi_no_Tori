﻿using UnityEngine;

public class CameraBoss : MonoBehaviour
{
    private GameObject player;
    private Camera cam;
    private Rigidbody2D rb;
    private PlayerController playerController;
    private float offset;
    private int count = 0;
    private int max = 50;

    void Start()
    {
        Boss.boss.SetActive(true);
        player = PlayerController.player;
        cam = MainCamera.cam;
        rb = player.GetComponent<Rigidbody2D>();
        rb.simulated = false;
        playerController = player.GetComponent<PlayerController>();
        playerController.enabled = false;
        offset = (playerController.maxCam - cam.transform.position.x) / max;
        count = -20;
    }

    void Update()
    {
        if (count < 0)
        {
            count++;
        }
        else if (count < max)
        {
            count++;
            playerController.minCam += offset;
            playerController.ReloadCamera();
            if (count == max)
            {
                SoundBackground.PlayBGMBoss();
                rb.simulated = true;
                playerController.enabled = true;
                Destroy(gameObject);
            }
        }
    }
}
