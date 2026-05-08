using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerGoSpawned", menuName = "RSE/PlayerGoSpawned", order = 0)]
public class RSE_PlayerGoSpawned : ScriptableObject
{
    public event Action<GameObject> PlayerGoSpawnedEvent;

    public void InvokePlayerGoSpawnedEvent(GameObject playerGo)
    {
        PlayerGoSpawnedEvent?.Invoke(playerGo);
    }

}
