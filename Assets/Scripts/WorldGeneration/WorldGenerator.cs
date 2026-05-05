using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private RSE_GenerateWorld generateWorldEvent;
    [SerializeField] private RSE_EndWorldGeneration2D endWorldGenerationEvent;
    [SerializeField] private RSO_Settings settings;
    [SerializeField] private RSO_BlockStorage blockStorage;
    [SerializeField] private Tilemap tilemap;

    private int WorldGenerated = 0;
    private void OnEnable()
    {
        generateWorldEvent.GenerateWorldEvent += GenerateWorld;
    }
    private void OnDisable()
    {
        generateWorldEvent.GenerateWorldEvent -= GenerateWorld;
    }

    private void GenerateWorld()
    {
        if (WorldGenerated == 0)
        {
            GenerateWorld2D(MyRandomState.FirstWorldGeneration);
        }
        else if (WorldGenerated == 1)
        {

        }
        else
        {

        }
    }



    private void GenerateWorld2D ( MyRandomState randomState)
    {
        tilemap.ClearAllTiles();    
        for (int i = 0; i < settings.mapSize; i++)
        {
            for (int j = 0; j < settings.mapSize; j++)
            {
                float height = NoiseGeneration.GetPerlinHeight(i, j, settings.mapScale);
                SetTilemapTile(new Vector3Int(i,j), height);
            }
        }
        WorldGenerated++;
        endWorldGenerationEvent.InvokeBaseWorldGeneration2DEnded();
        GenerateMapEventFromRandomState(randomState);

    }


    private void SetTilemapTile(Vector3Int coord, float height)
    {
        
        TileBase tile = GetTileForHeight(height);
        tilemap.SetTile(coord, tile );

    }
    private TileBase GetTileForHeight(float y)
    {
        y = y * 100;
        int mapHeight = 100;
        float zoneSize = (float)mapHeight / blockStorage.blocks.Count;
        int index = Mathf.Clamp((int)(y / zoneSize), 0, blockStorage.blocks.Count - 1);
        return blockStorage.blocks[index].tileBase;
    }



    private void GenerateMapEventFromRandomState (MyRandomState randomState)
    {
        int ennemiNbr = SeedManager.MyRandom(3,15, randomState);
        
        for (int i = 0; i < ennemiNbr; i++)
        {
            for (int x= 0; x < SeedManager.MyRandom(0,settings.mapSize,randomState); x++)
            {
                for (int y = 0; y < SeedManager.MyRandom(0, settings.mapSize, randomState); y++)
                {
                    tilemap.SetTile(new Vector3Int(x, y), blockStorage.ennemi.tileBase);
                }
            }
        }

    }

}

