using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{   
    private string ROCKTAG = "Rocks";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == ROCKTAG) {
            Destroy(col.gameObject);
        } else if (col.gameObject.tag == "Bullet") {
            Destroy(col.gameObject);
        }
    }
}
