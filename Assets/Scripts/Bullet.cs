using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10.0f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // If it hits an enemy...
        if (collision.tag == "Enemy")
        {

            // Destroy the laserbeam.
            Destroy(gameObject);

            //Destroy the enemy
            Destroy(collision.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
