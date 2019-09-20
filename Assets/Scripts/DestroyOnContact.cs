using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{
    private TowerController tc;

    // Start is called before the first frame update
    void Start()
    {
        tc = GameObject.FindObjectOfType<TowerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            tc.healthSystem.Damage(10);
            //Debug.LogWarning("damaged");
        }
    }
}
