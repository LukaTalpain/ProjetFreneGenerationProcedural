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
    public GameObject Prefab;
    public BlockStruct blockStruct;
}
[Serializable]
public struct BlockStruct
{
    public BlockType blockType;
}

public enum BlockType : ushort
{
    Water,
    Sand,
    Grass,
    Stone,
    Snow
}
