using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGeneration : MonoBehaviour
{
    [Header("Ref :")]
    public GameObject worldParent;
    [Header ("Event :")]
    public RSE_MapGenerated tilemapGenerated;

    [Header("RSO :")]
    public RSO_TilemapInfo tilemapInfo;
    public RSO_BlockPrefabStorage blockStorage;


    [Header("Tilemap Info : ")]
    private Tilemap tilemap;
    private int width;
    private int height;
    private void OnEnable()
    {
        tilemapGenerated.TilemapGenerated += GenerateWorld;
    }
    private void OnDisable()
    {
        tilemapGenerated.TilemapGenerated -= GenerateWorld;
    }


    private void GenerateWorld ()
    {
        print("cool");
        tilemapInfo.tilemap = tilemap;
        tilemapInfo.Width = width;
        tilemapInfo.Height = height;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                TileBase tile = tilemap.GetTile(new Vector3Int(i, j));
                GameObject _Block = ChooseBlock(tile);
                print (tile.name);
                GameObject _Instance = Instantiate(_Block,new Vector3 (i,0.5f,j),Quaternion.Euler(new Vector3(0,0,0)), worldParent.transform);
                print(_Instance.transform.position.x);
            }
        }
    }

    private GameObject ChooseBlock (TileBase tile)
    {
        if (tile.name == "Water")
        {
            return blockStorage.m_BlockPrefabs[0];
        }
        else if (tile.name == "DeepOcean")
        {
            return blockStorage.m_BlockPrefabs[1];
        }
        else if (tile.name == "Sand")
        {
            return blockStorage.m_BlockPrefabs[2];
        }
        else if (tile.name == "Grass")
        {
            return blockStorage.m_BlockPrefabs[3];
        }
        else if (tile.name == "Forest")
        {
            return blockStorage.m_BlockPrefabs[4];
        }
        else if (tile.name == "Montain")
        {
            return blockStorage.m_BlockPrefabs[5];
        }
        else 
        {
            return blockStorage.m_BlockPrefabs[6];
        }
    }
}
