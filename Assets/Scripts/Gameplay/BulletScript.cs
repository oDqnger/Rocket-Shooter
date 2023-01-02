using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BulletScript : MonoBehaviour
{
    [SerializeField] ParticleSystem RocksExplode;

    public static bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        RocksExplode = GameObject.Find("RockExplode").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Rocks") {
            isPlaying = true;
            PlayerControl.points++;
            RocksExplode.transform.position = transform.position;
            RocksExplode.Play();
            Destroy(gameObject);
            Destroy(col.gameObject);
        }
    }
}
