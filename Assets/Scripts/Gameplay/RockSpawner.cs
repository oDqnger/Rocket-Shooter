using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RockSpawner : MonoBehaviour
{      
    public GameObject[] rocks;

    int randomNumber;

    private string BULLETTAG = "Bullet";

    public static int count = 0;

    public static int wave = 1;
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] ParticleSystem[] winEffect;
    [SerializeField] AudioSource winAudio; 

    public static bool hasGameEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRocks(5f));
    }

    // Update is called once per frame
    void Update()
    {   

    }

    IEnumerator SpawnRocks(float timeToWait) { 
        while(!hasGameEnd) {
            yield return new WaitForSeconds(Random.Range(1f, timeToWait - count));
            randomNumber = Random.Range(0, 3);

            if (wave == 1) {
                Instantiate(rocks[randomNumber], new Vector3(Random.Range(-5f, 5f),5f, 0f), transform.rotation);
            } else if (wave == 2) {
                for (int x = 0; x<3 * 2; x++) {
                    yield return new WaitForSeconds(0.8f);
                    Instantiate(rocks[randomNumber], new Vector3(Random.Range(-7f, 8f), 5f, 0f), transform.rotation);
                }
            } else {
                for (int x = 0; x<5 * 3; x++) {
                    yield return new WaitForSeconds(0.2f);
                    var rock = Instantiate(rocks[randomNumber], new Vector3(Random.Range(-7f, 8f), 5f, 0f), transform.rotation);
                    rock.GetComponent<Rigidbody2D>().mass = 3.5f;
                    rock.GetComponent<Rigidbody2D>().gravityScale = 2f;
                }
            }
                    
            
            if (PlayerControl.isNextWave) {
                yield return new WaitForSeconds(6f - count);
                PlayerControl.isNextWave = false;
                wave++;
                switch(wave) {
                    case 2:
                        waveText.text = "Wave Two";
                        break;
                    case 3:
                        waveText.text = "Wave FINAL";
                        break;
                    default:
                        waveText.text = "YOU HAVE WON!!!";
                        winAudio.Play();
                        yield return new WaitForSeconds(1.5f);
                        winEffect[0].transform.position = new Vector3(0f, 4.49f, 0f);
                        winEffect[0].Play();
                        winEffect[1].transform.position = new Vector3(0f, -4.75f, 0f);
                        winEffect[1].Play();
                        break;

                }
                count++;
            }

            if (wave == 4) {
                Debug.Log("You won!");
                break;
            }
        }  
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == BULLETTAG) {
            Destroy(col.gameObject); 
        } 
    }
}
