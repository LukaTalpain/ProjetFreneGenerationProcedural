using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private RSO_Settings settings;
    [SerializeField] private RSE_GenerateWorld generateWorld;
    [SerializeField] private RSE_StartFight fightEvent;

    private void OnEnable()
    {
        fightEvent.EndBossFightEvent += EndBossFight;
    }
    private void OnDisable()
    {
        fightEvent.EndBossFightEvent -= EndBossFight;
    }
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
    private void EndBossFight()
    {
        Debug.Log("Boss Fight Ended");
        generateWorld.InvokeGenerateWorld();
    }   

}
