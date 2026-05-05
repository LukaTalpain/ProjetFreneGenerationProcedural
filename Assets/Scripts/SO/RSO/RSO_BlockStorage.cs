using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[CreateAssetMenu(fileName = "BlockStorage", menuName = "RSO/BlockStorage", order = 0)]
public class RSO_BlockStorage : ScriptableObject
{
    public List<Block> blocks = new List<Block>();

    public Block ennemi;
}

[Serializable]
public class Block
{
    public GameObject gameobject;
    public TileBase tileBase ;
}

