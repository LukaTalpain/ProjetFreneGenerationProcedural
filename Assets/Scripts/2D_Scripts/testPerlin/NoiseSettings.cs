using UnityEngine;

[System.Serializable]
public class NoiseSettings
{
    public int width = 256;
    public int height = 256;
    [Range(10f, 500f)]
    public float scale = 100f;
    [Range(1, 8)]
    public int octaves = 4;
    [Range(0f, 1f)]
    public float persistence = 0.5f;   // amplitude diminue à chaque octave
    [Range(1f, 4f)]
    public float lacunarity = 2f;     // fréquence augmente à chaque octave
    public int seed = 42;
    public Vector2 offset = Vector2.zero;
}