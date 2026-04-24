using System;
using UnityEngine;
using UnityEngine.Tilemaps;
[CreateAssetMenu]
public class RSO_BlockStorage : ScriptableObject
{
    public BlockInfo[] blockInfos = new BlockInfo[0];
}

[Serializable]
public class BlockInfo
{
    public float threshold;
    public TileBase Tile;
    public BlockStruct blockStruct;
}
[Serializable]
public struct BlockStruct
{
    public BlockType blockType;
}

public enum BlockType : ushort
{
    DeepWater,
    Water,
    Sand,
    Grass,
    MidGrass,
    HighGrass,
    Stone,
    MidStone,
    HighStone,
    Snow,
    air
}
