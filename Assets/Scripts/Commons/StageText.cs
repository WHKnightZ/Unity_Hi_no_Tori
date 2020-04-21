using UnityEngine;

public class StageText : MonoBehaviour
{
    public static Sprite[] nums;

    void Start()
    {
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = nums[StageManager.stage / 10];
        transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = nums[StageManager.stage % 10];
    }
}
