using UnityEngine;
using System;

[CreateAssetMenu(fileName = "MapEventGenerated", menuName = "RSE/MapEventGenerated", order = 0)]
public class RSE_MapEventGenerated : ScriptableObject
{
    public event Action MapEventGeneratedEvent;


    public void InvokeMapEventGenerated ()
    {
        MapEventGeneratedEvent?.Invoke();
    }   
}
