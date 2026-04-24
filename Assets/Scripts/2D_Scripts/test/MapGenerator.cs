using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    [Header("Bruit")]
    public NoiseSettings noise;

    [Header("Tilemap cible")]
    public Tilemap tilemap;



    private void Start()
    {
        Generate();
    }


    private void Generate ()
    {
        float[,] map = NoiseMap.Generate(noise.width, noise.height,noise);

        for (int y = 0; y < noise.height; y++)
        {
            for (int x = 0; x < noise.width; x++)
            {
                TileBase tileName = GetTile(map[x, y]);
                
            }
        }
    }


    private TileBase GetTile(float index)
    {

        //if (height <= terrain.threshold)
                //return terrain.name;

        //return terrains[terrains.Length - 1].name;

        return null;
    }
}
