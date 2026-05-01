using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName ="settings", menuName = "RSO/settings",order = 0)]
public class RSO_Settings : ScriptableObject
{
    public int Seed = 0;


    [Header("Map generation Settings ")]

    public int mapSize;
    public float mapScale;
    public List<TileBase> tileBases = new List<TileBase>();
}
