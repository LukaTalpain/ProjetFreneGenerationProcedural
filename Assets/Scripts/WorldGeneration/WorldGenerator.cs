using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private RSE_GenerateWorld generateWorldEvent;
    [SerializeField] private RSE_EndWorldGeneration2D endWorldGenerationEvent;
    [SerializeField] private RSO_Settings settings;
    [SerializeField] private RSO_BlockStorage blockStorage;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap eventTilemap;
    [SerializeField] private RSO_MapEventStorage mapEventStorage;
    [SerializeField] private RSE_MapEventGenerated mapEventGenerated;

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
        if (WorldGenerated >=15)
        {
            Debug.LogWarning("World already generated 15 times !!");
        }
        else
        {
            MyRandomState randomstate = (MyRandomState)WorldGenerated;
            SeedManager.ResetStateToInitial(randomstate);
            GenerateWorld2D(randomstate);
        }
            
    }



    private void GenerateWorld2D ( MyRandomState randomState)
    {
        tilemap.ClearAllTiles();    
        eventTilemap.ClearAllTiles(); eventTilemap.ClearAllTiles();
        mapEventStorage.ListEnnemiPos.Clear();
        mapEventStorage.ListHousePos.Clear();
        mapEventStorage.TowerPos = Vector2Int.zero;
        int seed = SeedManager.MyRandom(0, 1000, randomState);
        for (int i = 0; i < settings.mapSize; i++)
        {
            for (int j = 0; j < settings.mapSize; j++)
            {
                
                float height = NoiseGeneration.GetPerlinHeight(i, j, settings.mapScale,seed);
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
            eventTilemap.SetTile(new Vector3Int(x, y), blockStorage.ennemi.tileBase);
            mapEventStorage.ListEnnemiPos.Add(new Vector2Int(x, y));

        }

        //generate Main house

        Vector2Int newPos = CheckForAnEmptySlot(randomState, 0, settings.mapSize, 0,settings.mapSize);
        eventTilemap.SetTile(new Vector3Int(newPos.x, newPos.y), blockStorage.mainHouse.tileBase);
        mapEventStorage.ListHousePos.Add(new Vector2Int(newPos.x, newPos.y));
        //generate village around 
        GenerateVillageAroundMainHouse();


        //generate Tower 
        Vector2Int[] towerSpawnDelimitation = ChooseOppositeZoneFromPos(mapEventStorage.ListHousePos[0]);
        newPos = CheckForAnEmptySlot(randomState, towerSpawnDelimitation[0].x, towerSpawnDelimitation[0].y, towerSpawnDelimitation[1].x, towerSpawnDelimitation[1].y);
        eventTilemap.SetTile(new Vector3Int(newPos.x, newPos.y), blockStorage.tower.tileBase);
        mapEventStorage.TowerPos = (new Vector2Int(newPos.x, newPos.y));


        mapEventGenerated.InvokeMapEventGenerated();
    }

    private Vector2Int CheckForAnEmptySlot(MyRandomState randomState, int minX, int maxX, int minY, int maxY)
    {
        int x = SeedManager.MyRandom(minX, maxX, randomState);
        int y = SeedManager.MyRandom(minY, maxY, randomState);
        bool empty = true;
        for (int i = 0; i < mapEventStorage.ListEnnemiPos.Count; i++)
        {
            if (new Vector2Int (x,y) == mapEventStorage.ListEnnemiPos[i])
            {
                empty = false;
            }
        }
        for (int i = 0; i < mapEventStorage.ListHousePos.Count; i++)
        {
            if (new Vector2Int(x, y) == mapEventStorage.ListHousePos[i])
            {
                empty = false;
            }
        }
        if (tilemap.GetTile(new Vector3Int(x, y)) == blockStorage.blocks[0].tileBase)
        {
            empty = false;
        }
        if (tilemap.GetTile(new Vector3Int(x, y)) == blockStorage.blocks[1].tileBase)
        {
            empty = false;
        }

        if (empty)
        {
            return new Vector2Int(x, y);
        }
        else
        {
            return CheckForAnEmptySlot(randomState, minX, maxX, minY, maxY);
        }
    }

    private Vector2Int[] ChooseOppositeZoneFromPos(Vector2Int pos)
    {
        Vector2Int x = new Vector2Int(0, 0);
        Vector2Int y = new Vector2Int(0, 0);
        //choose left or right 
        if (pos.x <= ((int)(settings.mapSize/2)))
        {
            //left part 
            if (pos.y <= ((int)(settings.mapSize/2)))
            {
                //higher right  part 
                x = new Vector2Int(((int)(settings.mapSize / 2) + 1), settings.mapSize);
                y = new Vector2Int(((int)(settings.mapSize / 2) + 1), settings.mapSize);
                return new Vector2Int[] { x, y };
            }
            else
            {
                //lower right part 
                x = new Vector2Int(((int)(settings.mapSize / 2) + 1), settings.mapSize);
                y = new Vector2Int(0, ((int)(settings.mapSize / 2)));
                return new Vector2Int[] { x, y };
            }
        }
        else
        {
            //right part 
            if (pos.y <= ((int)(settings.mapSize / 2)))
            {
                //higher left  part 
                x = new Vector2Int(0, ((int)(settings.mapSize / 2)));
                y = new Vector2Int(((int)(settings.mapSize / 2) + 1), settings.mapSize);
                return new Vector2Int[] { x, y };
            }
            else
            {
                //lower left part 
                x = new Vector2Int(0, ((int)(settings.mapSize / 2)));
                y = new Vector2Int(0, ((int)(settings.mapSize / 2)));
                return new Vector2Int[] { x, y };
            }
        }

    }


    private void GenerateVillageAroundMainHouse()
    {
        Vector2Int mainHousePos = mapEventStorage.ListHousePos[0];
        Vector2Int spot = new Vector2Int();
        //first house 
        spot = new Vector2Int(mainHousePos.x, mainHousePos.y + 3);
        if (CheckIfSlotForHouse(spot))
        {
            eventTilemap.SetTile(new Vector3Int(spot.x, spot.y), blockStorage._house.tileBase);
            mapEventStorage.ListHousePos.Add(spot);
        }
        //second house 
        spot = new Vector2Int(mainHousePos.x +3, mainHousePos.y);
        if (CheckIfSlotForHouse(spot))
        {
            eventTilemap.SetTile(new Vector3Int(spot.x, spot.y), blockStorage._house.tileBase);
            mapEventStorage.ListHousePos.Add(spot);
        }
        //third house 
        spot = new Vector2Int(mainHousePos.x - 3, mainHousePos.y);
        if (CheckIfSlotForHouse(spot))
        {
            eventTilemap.SetTile(new Vector3Int(spot.x, spot.y), blockStorage._house.tileBase);
            mapEventStorage.ListHousePos.Add(spot);
        }
        //forth house 
        spot = new Vector2Int(mainHousePos.x - 3, mainHousePos.y -3);
        if (CheckIfSlotForHouse(spot))
        {
            eventTilemap.SetTile(new Vector3Int(spot.x, spot.y), blockStorage._house.tileBase);
            mapEventStorage.ListHousePos.Add(spot);
        }
        //fifth house 
        spot = new Vector2Int(mainHousePos.x + 3, mainHousePos.y - 3);
        if (CheckIfSlotForHouse(spot))
        {
            eventTilemap.SetTile(new Vector3Int(spot.x, spot.y), blockStorage._house.tileBase);
            mapEventStorage.ListHousePos.Add(spot);
        }

    }

    private bool CheckIfSlotForHouse(Vector2Int coord)
    {
        for (int i = 0; i < mapEventStorage.ListEnnemiPos.Count; i++)
        {
            if (new Vector2Int(coord.x, coord.y) == mapEventStorage.ListEnnemiPos[i])
            {
                return false;
            }
        }
        if (tilemap.GetTile(new Vector3Int(coord.x, coord.y)) == blockStorage.blocks[0].tileBase)
        {
            return false;
        }
        if (tilemap.GetTile(new Vector3Int(coord.x, coord.y)) == blockStorage.blocks[1].tileBase)
        {
            return false;
        }
        if (coord.x < 0 || coord.x >= settings.mapSize || coord.y < 0 || coord.y >= settings.mapSize)
        {
            return false;
        }
        return true;

    }

}

