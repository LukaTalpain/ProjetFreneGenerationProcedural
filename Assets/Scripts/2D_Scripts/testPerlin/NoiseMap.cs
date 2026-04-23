using UnityEngine;

public static class NoiseMap
{
    public static float[,] Generate(int width, int height, NoiseSettings settings)
    {
        float[,] map = new float[width, height];

        System.Random rng = new System.Random(settings.seed);
        Vector2[] octaveOffsets = new Vector2[settings.octaves];

        for (int i = 0; i < settings.octaves; i++)
        {
            float offsetX = rng.Next(-100000, 100000) + settings.offset.x;
            float offsetY = rng.Next(-100000, 100000) + settings.offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = width / 2f;
        float halfHeight = height / 2f;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float amplitude = 1f;
                float frequency = 1f;
                float noiseHeight = 0f;

                for (int o = 0; o < settings.octaves; o++)
                {
                    float sampleX = (x - halfWidth + octaveOffsets[o].x) / settings.scale * frequency;
                    float sampleY = (y - halfHeight + octaveOffsets[o].y) / settings.scale * frequency;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2f - 1f;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= settings.persistence;
                    frequency *= settings.lacunarity;
                }

                maxNoiseHeight = Mathf.Max(maxNoiseHeight, noiseHeight);
                minNoiseHeight = Mathf.Min(minNoiseHeight, noiseHeight);
                map[x, y] = noiseHeight;
            }
        }

        // Normalisation dans [0, 1]
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                map[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, map[x, y]);

        return map;
    }
}
