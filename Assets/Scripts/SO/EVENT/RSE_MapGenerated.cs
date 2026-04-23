using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class RSE_MapGenerated : ScriptableObject
{
    public event Action TilemapGenerated;

    public void InvokeTilemapGenerated()
    {
        TilemapGenerated?.Invoke();
    }
}
