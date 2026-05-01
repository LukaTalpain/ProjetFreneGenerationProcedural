using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private RSE_GenerateWorld generateWorldEvent;
    [SerializeField] private RSO_Settings settings;
    [SerializeField] private Tilemap tilemap;


    private void OnEnable()
    {
        generateWorldEvent.GenerateWorldEvent += GenerateWorld;
    }
    private void OnDisable()
    {
        generateWorldEvent.GenerateWorldEvent -= GenerateWorld;
    }




    private void GenerateWorld ()
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
        float zoneSize = (float)mapHeight / settings.tileBases.Count;
        int index = Mathf.Clamp((int)(y / zoneSize), 0, settings.tileBases.Count - 1);
        return settings.tileBases[index];
    }



}

