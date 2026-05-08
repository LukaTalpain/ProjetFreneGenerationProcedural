using System;
using System.Collections.Generic;

public static class SeedManager 
{
    private static Dictionary<MyRandomState, UnityEngine.Random.State> states = new();
    private static Dictionary<MyRandomState, UnityEngine.Random.State> initialStates = new();
    private static MyRandomState ActualState;
    public static int seed;


    public static void InitializeSeed (int AskingSeed)
    {
        states = new Dictionary<MyRandomState, UnityEngine.Random.State>();
        initialStates = new Dictionary<MyRandomState, UnityEngine.Random.State>();
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
            UnityEngine.Random.Range(0, 1000);
            states.Add(value, UnityEngine.Random.state);
            initialStates.Add(value, UnityEngine.Random.state);
        }
        ActualState = MyRandomState.WorldGeneration1;
    }
    public static void ResetStateToInitial(MyRandomState state)
    {
        states[state] = initialStates[state];
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
        ActualState = state;
    }


}

[Serializable]
public enum MyRandomState
{
    WorldGeneration1,
    WorldGeneration2,
    WorldGeneration3,
    WorldGeneration4,
    WorldGeneration5,
    WorldGeneration6,
    WorldGeneration7,
    WorldGeneration8,
    WorldGeneration9,
    WorldGeneration10,
    WorldGeneration11,
    WorldGeneration12,
    WorldGeneration13,
    WorldGeneration14,
    WorldGeneration15,
    fight
}
