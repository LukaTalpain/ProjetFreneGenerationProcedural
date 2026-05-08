using UnityEngine;
using UnityEngine.Tilemaps;

public class World3DGenerator : MonoBehaviour
{
    [SerializeField] private RSE_EndWorldGeneration2D generation2D;
    [SerializeField] private RSO_Settings settings;
    [SerializeField] private RSO_BlockStorage blockStorage;
    [SerializeField] private RSO_MapEventStorage mapEventStorage;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private RSE_MapEventGenerated mapEventGenerated;
    [SerializeField] private RSE_PlayerGoSpawned playerGoSpawnedEvent;


    private void OnEnable()
    {
        generation2D.BaseWorldGeneration2DEnded += Generate3DWorld;
        mapEventGenerated.MapEventGeneratedEvent += Generate3DMapEvent;
    }
    private void OnDisable()
    {
        generation2D.BaseWorldGeneration2DEnded -= Generate3DWorld;
        mapEventGenerated.MapEventGeneratedEvent -= Generate3DMapEvent;
    }





    private void Generate3DWorld ()
    {
        if (transform.childCount > 0)
        {
            ClearAllChild();
        }
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
    private void ClearAllChild()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }


    private void Generate3DMapEvent()
    {
        GenerateEnnemi();
        GenerateMainHouse();
        GenerateHouse();
        GenerateTower();

        GeneratePlayer();
    }

    private void GenerateEnnemi()
    {
        for (int i = 0; i < mapEventStorage.ListEnnemiPos.Count; i++)
        {
            TileBase tile = tilemap.GetTile(new Vector3Int(mapEventStorage.ListEnnemiPos[i].x, mapEventStorage.ListEnnemiPos[i].y));
            int height = GetHeightFromTile(tile);
            GameObject go = Instantiate(blockStorage.ennemi.gameobject, new Vector3(mapEventStorage.ListEnnemiPos[i].x, height, -mapEventStorage.ListEnnemiPos[i].y), Quaternion.Euler(new Vector3(0, 0, 0)), transform);
        }
    }

    private void GenerateMainHouse()
    {
        TileBase tile = tilemap.GetTile(new Vector3Int(mapEventStorage.ListHousePos[0].x, mapEventStorage.ListHousePos[0].y));
        int height = GetHeightFromTile(tile);  
        GameObject go = Instantiate(blockStorage.mainHouse.gameobject, new Vector3(mapEventStorage.ListHousePos[0].x, height, -mapEventStorage.ListHousePos[0].y), Quaternion.Euler(new Vector3(0, 0, 0)), transform);

    }
    private void GenerateHouse()
    {
        for (int i = 1; i < mapEventStorage.ListHousePos.Count; i++)
        {
            TileBase tile = tilemap.GetTile(new Vector3Int(mapEventStorage.ListHousePos[i].x, mapEventStorage.ListHousePos[i].y));
            int height = GetHeightFromTile(tile);
            GameObject go = Instantiate(blockStorage._house.gameobject, new Vector3(mapEventStorage.ListHousePos[i].x, height, -mapEventStorage.ListHousePos[i].y), Quaternion.Euler(new Vector3(0, 0, 0)), transform);
        }
    }
     private void GenerateTower()
    {
        TileBase tile = tilemap.GetTile(new Vector3Int(mapEventStorage.TowerPos.x, mapEventStorage.TowerPos.y));
        int height = GetHeightFromTile(tile);
        GameObject go = Instantiate(blockStorage.tower.gameobject, new Vector3(mapEventStorage.TowerPos.x, height, -mapEventStorage.TowerPos.y), Quaternion.Euler(new Vector3(0, 0, 0)), transform);
    }

    private int GetHeightFromTile (TileBase tile)
    {
        for (int i = 0; i < blockStorage.blocks.Count; i++)
        {
            if (blockStorage.blocks[i].tileBase == tile)
            {
                return i+1;
            }
        }
        Debug.LogError("Tile not found in block storage");
        return -1;
    }


    private void GeneratePlayer()
    {
        TileBase tile = tilemap.GetTile(new Vector3Int(mapEventStorage.ListHousePos[0].x-1, mapEventStorage.ListHousePos[0].y));
        int height = GetHeightFromTile(tile);
        GameObject go = Instantiate(blockStorage.player.gameobject, new Vector3(mapEventStorage.ListHousePos[0].x-1, height, -mapEventStorage.ListHousePos[0].y), Quaternion.Euler(new Vector3(0, 0, 0)), transform);
        playerGoSpawnedEvent.InvokePlayerGoSpawnedEvent(go);
    }
}
