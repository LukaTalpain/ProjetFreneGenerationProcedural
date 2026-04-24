using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    [Header("Bruit")]
    public NoiseSettings noise;

    [Header("Tilemap cible")]
    public Tilemap tilemap;
    [Header("RSO")]
    public RSO_BlockStorage storage;

    [Header("Tableau de valeur ")]

    private BlockStruct[,,] chunk;
    private List<BlockStruct[,,]> chunkList;   

    private void Start()
    {
        chunk = new BlockStruct[storage.blockInfos.Length, storage.blockInfos.Length, storage.blockInfos.Length];
        Generate();
    }
    private void Update()
    {
        if (Input .GetKeyDown(KeyCode.P))
        {
            Generate();
        }
    }


    private void Generate ()
    {
        float[,] map = NoiseMap.Generate(noise.width, noise.height,noise);

        DoTilemap(map);

        for (int y = 0; y < noise.height; y++)
        {
            for (int x = 0; x < noise.width; x++)
            {

            }
        }

    }

    private void DoTilemap(float[,] value)
    {
        for (int y = 0; y < noise.height; y++)
        {
            for (int x = 0; x < noise.width; x++)
            {
                TileBase tile = GetTile(value[x, y]);
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
    }
    private TileBase GetTile(float index) // a therme il faudrat enlever la partie tilemap et directement le traduire en blockStruct
    {
        foreach (var BlockInfo in storage.blockInfos)
            if (index <= BlockInfo.threshold)
                return BlockInfo.Tile;

        return storage.blockInfos[storage.blockInfos.Length - 1].Tile;

    }

}
