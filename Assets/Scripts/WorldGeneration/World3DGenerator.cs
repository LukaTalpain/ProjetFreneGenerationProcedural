using UnityEngine;
using UnityEngine.Tilemaps;

public class World3DGenerator : MonoBehaviour
{
    [SerializeField] private RSE_EndWorldGeneration2D generation2D;
    [SerializeField] private RSO_Settings settings;
    [SerializeField] private RSO_BlockStorage blockStorage;
    [SerializeField] private Tilemap tilemap;


    private void OnEnable()
    {
        generation2D.BaseWorldGeneration2DEnded += Generate3DWorld;
    }
    private void OnDisable()
    {
        generation2D.BaseWorldGeneration2DEnded -= Generate3DWorld;
    }





    private void Generate3DWorld ()
    {
        for (int  x= 0; x < settings.mapSize; x++)
        {
            for (int y = 0; y < settings.mapSize; y++)
            {
                TileBase tile = tilemap.GetTile(new Vector3Int(x, y, 0));
                for (int i = 0; i < blockStorage.blocks.Count; i++)
                {
                    if (blockStorage.blocks[i].tileBase == tile)
                    {
                        GameObject go = Instantiate(blockStorage.blocks[i].gameobject,new Vector3 (x,i, -y),Quaternion.Euler(new Vector3 (0,0,0)), transform);
                    }
                }
            }
        }
    }
}
