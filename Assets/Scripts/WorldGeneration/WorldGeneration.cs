using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGeneration : MonoBehaviour
{
    [Header ("Event :")]
    public RSE_MapGenerated tilemapGenerated;

    [Header("RSO :")]
    public RSO_TilemapInfo tilemapInfo;


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
        tilemapInfo.tilemap = tilemap;
        tilemapInfo.Width = width;
        tilemapInfo.Height = height;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                //regarder la tile et faire en fonction 
            }
        }
    }
}
