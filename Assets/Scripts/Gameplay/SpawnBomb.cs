using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnBomb : MonoBehaviour
{   

    [SerializeField] GameObject player;

    GameObject dupedBomb;

    [SerializeField] ParticleSystem explodeParticles;

    Animator shakeAnimation;

    [SerializeField] AudioSource explodeAudio;
    
    [SerializeField] TextMeshProUGUI Score, Coins, Bombs, _rockCollected;
    [SerializeField] GameObject DeathScreen;
    [SerializeField] Animator ScreenSlide;

    // Start is called before the first frame update
    void Start()
    {
        shakeAnimation = GameObject.Find("Main Camera").GetComponent<Animator>();
        StartCoroutine(SpawnBombFunc());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnBombFunc() {
        while(true) {
            yield return new WaitForSeconds(Random.Range(7f, 15f)); 
            PlayerControl.shouldExplode = true;
            dupedBomb = Instantiate(gameObject, new Vector3(Random.Range(-7f, 8f), Random.Range(-4f, 4f), 0f), transform.rotation);
            yield return new WaitForSeconds(7f);
            Debug.Log("waited");
            if (PlayerControl.shouldExplode) {
                Destroy(gameObject);
                explodeAudio.Play();
                explodeParticles.transform.position = player.transform.position;
                shakeAnimation.SetBool("isUltraShake", true);
                Destroy(player);
                explodeParticles.Play();
                shakeAnimation.SetBool("isUltraShake", false);
                RockSpawner.hasGameEnd = false;


                // death screen
                DeathScreen.SetActive(true);
                ScreenSlide.SetBool("IsSlide", true);
                Coins.text = PlayerControl.coins.ToString();
                Bombs.text = PlayerControl.BombsCollected.ToString();
                _rockCollected.text = PlayerControl.points.ToString();
                Score.text = $"Total Score: {((PlayerControl.coins + PlayerControl.BombsCollected + PlayerControl.points) / 2)}";
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            PlayerControl.BombsCollected++;
            PlayerControl.shouldExplode = false;
            Destroy(gameObject);
        }
    }
}
