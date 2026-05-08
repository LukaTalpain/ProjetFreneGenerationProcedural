using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightManager : MonoBehaviour
{
    [SerializeField] private RSE_StartFight startFightEvent;
    [SerializeField] private GameObject fightUI;
    [SerializeField] private TextMeshProUGUI PVennemi;
    [SerializeField] private TextMeshProUGUI PVplayer;
    [SerializeField] private GameObject LostMessage; 
    [SerializeField] private int ennemiMaxPV;
    [SerializeField] private int ennemiCurrentPV;
    [SerializeField] private int playerMaxPV;
    [SerializeField] private int playerCurrentPV;

    [SerializeField] private int bossMaxPV;
    [SerializeField] private int bossCurrentPV;
    [SerializeField] private RSO_Stats stats;

    private GameObject currentEnnemi;

    private bool isBossFight = false;   
    private bool fightStarted = false;  
    private void OnEnable()
    {
        startFightEvent.StartFightEvent += StartFight;
        startFightEvent.StartBossFightEvent += StartBossFight;
    }
    private void OnDisable()
    {
        startFightEvent.StartFightEvent -= StartFight;
        startFightEvent.StartBossFightEvent -= StartBossFight;
    }
    private void Start()
    {
        fightUI.SetActive(false);
        LostMessage.SetActive(false);
    }
    private void StartFight (GameObject go)
    {
        fightStarted = true;
        currentEnnemi = go; 
        isBossFight = false;
        Debug.Log("Fight Started");
        bossMaxPV = stats.bossHealth;
        ennemiMaxPV = stats.ennemiHealth;
        playerMaxPV = stats.playerHealth;
        ennemiCurrentPV = ennemiMaxPV;  
        playerCurrentPV = playerMaxPV;
        PVennemi.text = $"Ennemi PV : {ennemiCurrentPV}/{ennemiMaxPV}";
        PVplayer.text = $"Player PV : {playerCurrentPV}/{playerMaxPV}";
        fightUI.SetActive(true);
        StartCoroutine(ennemiAtkCooldwon());
    }
    private void StartBossFight ()
    {
        fightStarted = true;
        isBossFight = true;
        Debug.Log("Boss Fight Started");
        bossMaxPV = stats.bossHealth;
        playerMaxPV = stats.playerHealth;
        bossCurrentPV = bossMaxPV;
        playerCurrentPV = playerMaxPV;
        PVennemi.text = $"Boss PV : {bossCurrentPV}/{bossMaxPV}";
        PVplayer.text = $"Player PV : {playerCurrentPV}/{playerMaxPV}";
        fightUI.SetActive(true);
        StartCoroutine(BossFightAtckCooldown());
    }
    private void Update()
    {
        if (fightStarted)
        {
            if (isBossFight)
            {
                UpdateBossFight();
            }
            else
            {
                UpdateNormalFight();
            }
        }
    }
    private void UpdateNormalFight ()
    {

        PVennemi.text = $"Ennemi PV : {ennemiCurrentPV}/{ennemiMaxPV}";
        PVplayer.text = $"Player PV : {playerCurrentPV}/{playerMaxPV}";

        if (playerCurrentPV <= 0)
        {
            Debug.Log("Player Defeated");
            PlayerLost();
        }
        if (ennemiCurrentPV <= 0)
        {
            Debug.Log("Ennemi Defeated");
            EnnemiDefeated();
        }
    }

    private void UpdateBossFight ()
    {
        PVennemi.text = $"Boss PV : {bossCurrentPV}/{bossMaxPV}";
        PVplayer.text = $"Player PV : {playerCurrentPV}/{playerMaxPV}";
        if (playerCurrentPV <= 0)
        {
            Debug.Log("Player Defeated");
            PlayerLost();
        }
        if (bossCurrentPV <= 0)
        {
            Debug.Log("Boss Defeated");
            BossDefeated();
        }
    }
    IEnumerator BossFightAtckCooldown ()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Boss Attacked");
        playerCurrentPV -= 2;
        if (playerCurrentPV > 0 && bossCurrentPV > 0)
        {
            StartCoroutine(BossFightAtckCooldown());
        }

    }
    IEnumerator ennemiAtkCooldwon ()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Ennemi Attacked");
        playerCurrentPV -= 1;
        if (playerCurrentPV > 0 && ennemiCurrentPV > 0)
        {
            StartCoroutine(ennemiAtkCooldwon());
        }
            
    }

    public void PlayerAtk ()
    {
        if (isBossFight)
        {
            bossCurrentPV -= 1;
        }
        else
        {
            ennemiCurrentPV -= 1;
        }
    }


    private void PlayerLost ()
    {
        Debug.Log("Player Defeated");
        fightStarted = false;
        LostMessage.SetActive(true);
    }

    private void EnnemiDefeated ()
    {
        currentEnnemi.SetActive(false);
        fightStarted = false;
        fightUI.SetActive(false);
    }

    private void BossDefeated ()
    {
        Debug.Log("Boss Defeated");
        fightStarted = false;
        fightUI.SetActive(false);
        startFightEvent.InvokeEndBossFight();
    }

    public void BackToMenu ()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
