using UnityEngine;
using UnityEngine.SceneManagement;

public class EatPiece : MonoBehaviour
{
    public static string tagPlayer = "Player";

    private bool isEaten = false;
    private float delay;

    void Update()
    {
        if (isEaten)
        {
            delay -= Time.deltaTime;
            if (delay < 0f)
            {
                StageManager.stage = StageManager.stageNext;
                StageManager.isLocated = false;
                SceneManager.LoadScene("Prepare");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tagPlayer))
        {
            GameObject player = collision.gameObject;
            player.GetComponent<Rigidbody2D>().simulated = false;
            player.GetComponent<PlayerController>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
            isEaten = true;
            delay = 1.5f;
            SoundEffect.PlayEatPiece();
        }
    }
}
