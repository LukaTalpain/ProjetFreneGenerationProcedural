using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGeneration : MonoBehaviour
{
    [Header("Ref :")]
    public GameObject worldParent;
    public List<GameObject> worldObjects = new List<GameObject>();
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
        tilemapGenerated.TilemapGenerated += TakeEvent;
    }
    private void OnDisable()
    {
        tilemapGenerated.TilemapGenerated -= TakeEvent;
    }

    private void TakeEvent ()
    {
        EraseCurrentWorld();
        StartCoroutine(GenerateWorld());
    }
    private void EraseCurrentWorld ()
    {
        for (int i = 0; i < worldObjects.Count; i++)
        {
            Destroy(worldObjects[i]);
        }
        worldObjects.Clear();
    }
    private IEnumerator GenerateWorld ()
    {
        print("cool");
        tilemap = tilemapInfo.tilemap;
        width = tilemapInfo.Width ;
        height = tilemapInfo.Height;

        for (int i = 0; i < width; i++)
        {
            yield return new WaitForSeconds(0.01f);
            for (int j = 0; j < height; j++)
            {
                TileBase tile = tilemap.GetTile(new Vector3Int(i, j));
                GameObject _Block = ChooseBlock(tile);
                print(tile.name);
                GameObject _Instance = Instantiate(_Block, new Vector3(i, 0.5f, j), Quaternion.Euler(new Vector3(0, 0, 0)), worldParent.transform);
                worldObjects.Add(_Instance);
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
        else if (tile.name == "MidGrass")
        {
            return blockStorage.m_BlockPrefabs[4];
        }
        else if (tile.name == "HighGrass")
        {
            return blockStorage.m_BlockPrefabs[5];
        }
        else if (tile.name == "Montain")
        {
            return blockStorage.m_BlockPrefabs[6];
        }
        else if (tile.name == "MidMontain")
        {
            return blockStorage.m_BlockPrefabs[7];
        }
        else if (tile.name == "HighMontain")
        {
            return blockStorage.m_BlockPrefabs[8];
        }
        else 
        {
            return blockStorage.m_BlockPrefabs[9];
        }
    }
}
