using UnityEngine;
using UnityEngine.SceneManagement;

public class Dead : MonoBehaviour
{
    private float delay;

    private void Start()
    {
        HpManager.ResetInit();
        delay = 2f;
    }

    void Update()
    {
        delay -= Time.deltaTime;
        if (delay < 0f)
        {
            SceneManager.LoadScene("Prepare");
        }
    }
}
