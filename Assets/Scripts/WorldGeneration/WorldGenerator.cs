using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private RSE_GenerateWorld generateWorldEvent;
    [SerializeField] private RSE_EndWorldGeneration2D endWorldGenerationEvent;
    [SerializeField] private RSO_Settings settings;
    [SerializeField] private RSO_BlockStorage blockStorage;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private RSO_MapEventStorage mapEventStorage;

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
            print("world generated = 1");
        }
        else
        {
            print("world generated = 1+");
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
        int x;
        int y;  
        //ennemi generator
        int ennemiNbr = SeedManager.MyRandom(3,15, randomState);
        
        for (int i = 0; i < ennemiNbr; i++)
        {
            x = SeedManager.MyRandom(0,settings.mapSize,randomState);
            y = SeedManager.MyRandom(0, settings.mapSize, randomState);
            tilemap.SetTile(new Vector3Int(x, y), blockStorage.ennemi.tileBase);
            mapEventStorage.ListEnnemiPos.Add(new Vector2Int(x, y));

        }

        //generate Village

        x = SeedManager.MyRandom(0, settings.mapSize, randomState);
        y = SeedManager.MyRandom(0, settings.mapSize, randomState);
        tilemap.SetTile(new Vector3Int(x, y), blockStorage.ennemi.tileBase);
        mapEventStorage.ListHousePos.Add(new Vector2Int(x, y));

        //generate Tower 


    }

    private Vector2Int ChooseOppositeZoneFromPos(Vector2Int pos)
    {
        //choose left or right 



        return Vector2Int.zero;
    }


}

