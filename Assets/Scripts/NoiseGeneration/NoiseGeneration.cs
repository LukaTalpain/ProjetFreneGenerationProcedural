
using System;
using UnityEngine;

public static class NoiseGeneration 
{

    public static float GetPerlinHeight(float sampleX, float sampleY , float scale , int seed) //, MyRandomState myState
    {

        return Mathf.PerlinNoise(sampleX* scale + seed,sampleY * scale + seed);


    }
}
