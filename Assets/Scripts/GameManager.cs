using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private RSO_Settings settings;
    private void Awake()
    {
        SeedManager.InitializeSeed(settings.Seed);
        settings.Seed = SeedManager.seed;
    }
}
