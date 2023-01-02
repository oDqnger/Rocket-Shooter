using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnHealth : MonoBehaviour
{   
    [SerializeField] AudioSource healthPower;
    [SerializeField] TextMeshProUGUI healthText, hitPoints;
    [SerializeField] Animator FadeIn;

    float time = 20f;
    public static int count = 0;
    // Start is called before the first frame update
    void Start()
    {   
        StartCoroutine(SpawnHealthFunc(time));
    }

    // Update is called once per frame
    void Update()
    {   
        
    }

    IEnumerator SpawnHealthFunc(float timeToWait) {
        while(true) {
            yield return new WaitForSeconds(timeToWait);
            var healthPowerup = Instantiate(gameObject, new Vector3(Random.Range(-7f, 8f), Random.Range(-4f, 4f), 0f), transform.rotation);
        }
    }

    

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            PlayerControl.health++;
            healthText.text = PlayerControl.health.ToString();
            healthPower.Play();
            PlayerControl.PlayHitPoint = true;
            Destroy(gameObject);
        }
    }
}
