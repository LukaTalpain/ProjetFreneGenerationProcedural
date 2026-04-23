using System;
using UnityEngine;
using UnityEngine.Tilemaps;
[CreateAssetMenu]
[Serializable]
public class RSO_TilemapInfo : ScriptableObject
{
    public Tilemap tilemap = null;
    public int Width = 0;
    public int Height = 0;


}
