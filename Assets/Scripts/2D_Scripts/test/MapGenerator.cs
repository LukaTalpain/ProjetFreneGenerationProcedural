using System;
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

    [Header("Valeur a mettre impaire ")]
    public int mapLength;
    public  List<BlockStruct[,,]> chunkList = new List<BlockStruct[,,]>();

   

    private void Start()
    {
        Generate();
    }
    private void Update()
    {
        if (Input .GetKeyDown(KeyCode.P))
        {
            Generate();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            VisualizeChunk(chunkList);
        }
    }


    private void Generate ()
    {
        float[,] map = NoiseMap.Generate(noise.width, noise.height,noise);

        DoTilemap(map);
        chunkList.Clear();
        for (int x = 0; x < mapLength; x++)
        {
            for (int y = 0; y < mapLength; y++)
            {
                BlockStruct[,,] _chunk = GenerateChunk(new Vector2Int(x,y));
                chunkList.Add(_chunk);
            }
        }
        print("done");

    }

    private BlockStruct[,,] GenerateChunk (Vector2Int mapCoord)
    {
        noise.offset.x = noise.width * mapCoord.x;
        noise.offset.y = noise.height * mapCoord.y;
        float[,] map = NoiseMap.Generate(noise.width, noise.height, noise);
        BlockStruct[,,] _chunk = new BlockStruct[noise.width, noise.width, noise.width];
        for (int x = 0; x < noise.width; x++)
        {
            for (int y = noise.width -1; y >= 0; y--)
            {
                for (int z = 0; z < noise.width; z++)
                {
                      if (map[x,z] >= (float)y /noise.width)
                    {
                        _chunk[x, y, z] = GetStruct(map[x, z]);
                    }
                    else
                    {
                        _chunk[x, y, z] = storage.blockInfos[storage.blockInfos.Length-1].blockStruct;
                    }
                }
            }
        }
        return _chunk;


    }

    private List<GameObject> debugCubes = new List<GameObject>();

    private void VisualizeChunk(List<BlockStruct[,,]> chunks)
    {
        // Nettoie l'ancienne visualisation
        foreach (var cube in debugCubes) Destroy(cube);
        debugCubes.Clear();

        for (int i  = 0; i < chunks.Count; i++)
        {
            for (int x = 0; x < noise.width; x++)
                for (int y = 0; y < noise.width; y++)
                    for (int z = 0; z < noise.width; z++)
                    {
                        BlockStruct[,,] chunk = chunks[i];
                        if (chunk[x, y, z].blockType != BlockType.air)
                        {
                            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            cube.transform.position = new Vector3(x, y, z) + new Vector3 (10*i,0,0);
                            cube.transform.parent = transform;
                            debugCubes.Add(cube);
                        }
                    }
        }

        

        
    }

    private BlockStruct GetStruct (float index)
    {
        foreach (var BlockInfo in storage.blockInfos)
            if (index <= BlockInfo.threshold)
                return BlockInfo.blockStruct;

        return storage.blockInfos[9].blockStruct;
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

        return storage.blockInfos[9].Tile;

    }

}
