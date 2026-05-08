using UnityEngine;

public class bossBehaviour : MonoBehaviour
{
    [SerializeField] private RSE_StartFight startFight;
    private void OnTriggerEnter(Collider other)
    {
        startFight.InvokeStartBossFight();
    }
}
