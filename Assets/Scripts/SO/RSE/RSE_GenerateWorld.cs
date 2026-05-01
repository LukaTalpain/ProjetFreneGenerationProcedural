using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GenerateWolrd", menuName = "RSE/GenerateWolrd", order = 0)]
public class RSE_GenerateWorld : ScriptableObject
{
    public event Action GenerateWorldEvent;

    public void InvokeGenerateWorld ()
    {
        GenerateWorldEvent?.Invoke();
    }
}
