
using System;
using UnityEngine;

public static class NoiseGeneration 
{

    public static float GetPerlinHeight(float sampleX, float sampleY , float scale) //, MyRandomState myState
    {
        int seed = SeedManager.seed;

        return Mathf.PerlinNoise(sampleX* scale + seed,sampleY * scale + seed);


    }
}
