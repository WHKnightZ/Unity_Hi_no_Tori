using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public static GameObject[] portals;
    public static int count;

    void Start()
    {
        portals = new GameObject[5];
        count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            GameObject obj = transform.GetChild(i).gameObject;
            portals[i] = obj;
        }
    }

    public static bool IsInPortal(Vector3 pos)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 portalPos = portals[i].transform.position;
            if (pos.x > portalPos.x - 0.2f && pos.x < portalPos.x + 0.2f && pos.y > portalPos.y - 0.2f && pos.y < portalPos.y + 0.2f)
            {
                StageManager.isLocated = true;
                Portal portal = portals[i].GetComponent<Portal>();
                StageManager.stage = portal.stage;
                StageManager.position = portal.position;
                return true;
            }
        }
        return false;
    }
}
