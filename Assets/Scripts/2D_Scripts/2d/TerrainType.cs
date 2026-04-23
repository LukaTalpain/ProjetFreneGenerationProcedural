using UnityEngine;

[System.Serializable]
public struct TerrainType
{
    public string name;
    public Color color;
    [Range(0f, 1f)]
    public float threshold; // hauteur max pour ce biome
}