using System;
using UnityEngine;

[CreateAssetMenu(fileName = "StartFight", menuName = "RSE/MapEvent/StartFight", order = 0)]
public class RSE_StartFight : ScriptableObject
{
    public event Action<GameObject> StartFightEvent;
    public void InvokeStartFight (GameObject go)
    {
        StartFightEvent?.Invoke(go);
    }


    public event Action StartBossFightEvent;
    public void InvokeStartBossFight ()
    {
        StartBossFightEvent?.Invoke();
    }

    public event Action EndBossFightEvent;
    public void InvokeEndBossFight ()
    {
        EndBossFightEvent?.Invoke();
    }
}
