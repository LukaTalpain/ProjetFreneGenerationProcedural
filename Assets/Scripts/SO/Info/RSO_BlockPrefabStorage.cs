using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
[Serializable]
public class RSO_BlockPrefabStorage : ScriptableObject
{
    public List<GameObject> m_BlockPrefabs = new List<GameObject>();
    public List<Tile> tiles = new List<Tile>();
}
