
using System;
using UnityEngine;

public static class NoiseGeneration 
{

    public static float GetPerlinHeight(float sampleX, float sampleY )
    {
        int seed = SeedManager.seed;

        return Mathf.PerlinNoise(sampleX*seed,sampleY*seed);


    }
}
