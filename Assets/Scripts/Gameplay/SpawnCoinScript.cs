using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoinScript : MonoBehaviour
{
    public static bool isCollected = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCoin());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnCoin() {
        while (!RockSpawner.hasGameEnd) {
            yield return new WaitForSeconds(Random.Range(7.5f, 15f));
            var dupedCoin = Instantiate(gameObject, new Vector3(Random.Range(-7f, 8f), Random.Range(-4f, 4f), 0f), transform.rotation);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            isCollected = true;
            Destroy(gameObject);
        }
    }
}
