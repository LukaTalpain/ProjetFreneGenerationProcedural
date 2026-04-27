

using System;
using System.Collections.Generic;

public static class SeedManager 
{
    private static Dictionary<MyRandomState, UnityEngine.Random.State> states;
    private static MyRandomState ActualState;
    public static int seed;


    public static void InitializeSeed (int AskingSeed)
    {
        if (AskingSeed == 0)
        {
            seed = UnityEngine.Random.Range(1,1000);
            UnityEngine.Random.InitState(seed);

        }
        else
        {
            seed = AskingSeed;
            UnityEngine.Random.InitState (AskingSeed);
        }

        foreach (MyRandomState value in Enum.GetValues(typeof(MyRandomState))) 
        {
            states.Add(value, UnityEngine.Random.state);
        }
        ActualState = MyRandomState.WorldGeneration;
    }

    public static int MyRandom(int min, int max, MyRandomState state)
    {
        InitializeState(state);
        return UnityEngine.Random.Range (min, max);
    }

    public static float MyRandom(float min, float max, MyRandomState state)
    {
        InitializeState(state);

        return UnityEngine.Random.Range(min, max);
    }

    private static void InitializeState(MyRandomState state)
    {
        states[ActualState] = UnityEngine.Random.state;
        UnityEngine.Random.state = states[state];
    }


}

public enum MyRandomState
{
    WorldGeneration,
    fight
}
