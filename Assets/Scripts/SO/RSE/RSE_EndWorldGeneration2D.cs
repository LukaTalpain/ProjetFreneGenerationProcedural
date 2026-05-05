using System;
using UnityEngine;
[CreateAssetMenu(fileName = "EndGenerateWolrd", menuName = "RSE/EndGenerateWolrd", order = 0)]
public class RSE_EndWorldGeneration2D : ScriptableObject
{
    public event Action BaseWorldGeneration2DEnded;

    public void InvokeBaseWorldGeneration2DEnded ()
    {
        BaseWorldGeneration2DEnded?.Invoke ();
    }
}
