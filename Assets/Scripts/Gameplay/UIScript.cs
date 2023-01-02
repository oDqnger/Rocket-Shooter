using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{   
    [SerializeField] GameObject DeathScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoHome() {
        ResetVariables();
        if (DeathScreen.active) {
            DeathScreen.SetActive(false);
        }
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ResetVariables();
        if (DeathScreen.active) {
            DeathScreen.SetActive(false);
        }
    }

    void ResetVariables() {
        BulletScript.isPlaying = false;
        PlayerControl.health = 3;
        PlayerControl.points = 0;
        PlayerControl.isNextWave = false;
        PlayerControl.target = 20;
        PlayerControl.coins = 0;
        PlayerControl.shouldExplode = false;
        PlayerControl.PlayHitPoint = false;
        RockSpawner.count = 0;
        RockSpawner.wave = 1;
        RockSpawner.hasGameEnd = false;
        BulletScript.isPlaying = false;
        SpawnCoinScript.isCollected = false;
        SpawnHealth.count = 0;
    }
}
