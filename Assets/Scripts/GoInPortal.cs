using UnityEngine;
using UnityEngine.SceneManagement;

public class GoInPortal : MonoBehaviour
{
    private float delay = 2f;

    void Update()
    {
        delay -= Time.deltaTime;
        if (delay < 0f)
        {
            SceneManager.LoadScene("Prepare");
        }
    }
}
