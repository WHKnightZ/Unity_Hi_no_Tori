using UnityEngine;
using UnityEngine.Tilemaps;

public class Unstable : MonoBehaviour
{
    public static Tilemap tilemap;
    public static TileBase tileStone;
    public static GameObject tileExplode;

    public TileBase stone;

    private static Vector3[] explodeDirections = { new Vector3(-1f, 1f, 0f), new Vector3(1f, 1f, 0f), new Vector3(1f, -1f, 0f), new Vector3(-1f, -1f, 0f) };

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        tileStone = stone;
    }

    public static void DestroyTile(Vector3Int pos)
    {
        if (tilemap.GetTile(pos) == null)
            return;
        GameObject go;
        for (int i = 0; i < 4; i++)
        {
            go = Instantiate(tileExplode, tilemap.GetCellCenterWorld(pos), Quaternion.identity);
            go.GetComponent<TileExplode>().direction = explodeDirections[i];
        }
        tilemap.SetTile(pos, null);
        SoundEffect.PlayTileExplode();
    }

    public static bool CreateStone(Vector3 pos)
    {
        if (Permanent.tilemap.GetTile(Permanent.tilemap.WorldToCell(pos)) != null)
            return false;
        var pos2 = tilemap.WorldToCell(pos);
        if (tilemap.GetTile(pos2) == null)
        {
            tilemap.SetTile(pos2, tileStone);
            SoundEffect.PlayCreateStone();
            return true;
        }
        return false;
    }
}
