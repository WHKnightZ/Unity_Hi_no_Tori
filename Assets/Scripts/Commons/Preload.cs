using UnityEngine;

public class Preload : MonoBehaviour
{
    public delegate bool BoolDelegate();

    public static bool isInit = false;

    void Awake()
    {
        if (!isInit)
        {
            isInit = true;

            // PlayerController
            Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/Player/Player");
            PlayerController.spriteMove = new Sprite[4];
            PlayerController.spriteMove[0] = sprites[8];
            PlayerController.spriteMove[1] = sprites[9];
            PlayerController.spriteMove[2] = sprites[10];
            PlayerController.spriteMove[3] = sprites[11];
            PlayerController.spriteJump = sprites[7];
            PlayerController.spriteShoot = sprites[14];
            PlayerController.spriteCrouch = sprites[2];
            PlayerController.bullet = Resources.Load<GameObject>("Prefabs/Player/Bullet");
            PlayerController.bulletUp = Resources.Load<GameObject>("Prefabs/Player/BulletUp");
            PlayerController.layerGround = LayerMask.GetMask("Ground");
            PlayerController.layerEnemy = LayerMask.GetMask("Enemy");
            PlayerController.playerDead = Resources.Load<GameObject>("Prefabs/Player/PlayerDead");
            PlayerController.playerPortal = Resources.Load<GameObject>("Prefabs/Player/PlayerPortal");
            PlayerController.cameraBoss = Resources.Load<GameObject>("Prefabs/Bosses/CameraBoss");

            // Unstable
            Unstable.tileExplode = Resources.Load<GameObject>("Prefabs/Others/TileExplode");

            // BulletController
            BulletController.bulletExplode = Resources.Load<GameObject>("Prefabs/Player/BulletExplode");
            BulletController.stone = Resources.Load<GameObject>("Prefabs/Items/Stone");
            BulletController.tagEnemy = "Enemy";

            // Score, StoneManager, StageText
            Sprite[] nums = Resources.LoadAll<Sprite>("Sprites/Others/Nums");
            ScoreManager.nums = nums;
            StoneManager.nums = nums;
            StageText.nums = nums;

            // Item
            Item.tagPlayer = ItemStone.tagPlayer = "Player";
            Item.tagBullet = ItemStone.tagBullet = "Bullet";

            // Enemy
            Enemy.layerGround = PlayerController.layerGround;

            // EnemyDinoJump
            EnemyDinoJump.sprites = Resources.LoadAll<Sprite>("Sprites/Enemies/DinoJump");

            // EnemyFox
            EnemyFox.sprites = Resources.LoadAll<Sprite>("Sprites/Enemies/Fox");

            // EnemyDinoFire
            EnemyDinoFire.bullet = Resources.Load<GameObject>("Prefabs/Enemies/BulletDinoFire");

            // Boss
            Boss.piece = Resources.Load<GameObject>("Prefabs/Others/Piece");
            Boss.tagBullet = Item.tagBullet;
            Boss.layerGround = Enemy.layerGround;

            // Boss01
            Boss01.bullet = Resources.Load<GameObject>("Prefabs/Bosses/BulletBoss01");

            // Boss13
            Boss13.bullet = Resources.Load<GameObject>("Prefabs/Bosses/BulletBoss13");
            sprites = Resources.LoadAll<Sprite>("Sprites/Bosses/Boss13");
            Boss13.sprites = new Sprite[4];
            Boss13.sprites[0] = sprites[0];
            Boss13.sprites[1] = sprites[1];
            Boss13.sprites[2] = sprites[2];
            Boss13.sprites[3] = sprites[1];

        }

        Destroy(gameObject);
    }

}
