using UnityEngine;

public class EnnemiBehaviour : MonoBehaviour
{
    [SerializeField] private RSE_StartFight startFight;
    private void OnTriggerEnter(Collider other)
    {
        startFight.InvokeStartFight(this.gameObject);
    }
}
