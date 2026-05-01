using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private RSO_Settings settings;
    [SerializeField] private RSE_GenerateWorld generateWorld;
    private void Awake()
    {
        SeedManager.InitializeSeed(settings.Seed);
        settings.Seed = SeedManager.seed;
    }

    private void Start()
    {
        generateWorld.InvokeGenerateWorld();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            generateWorld.InvokeGenerateWorld();
        }
    }


}
