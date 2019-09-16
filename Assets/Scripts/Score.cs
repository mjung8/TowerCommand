using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int scoreValue = 1;

    private TowerController tc;
    private GameController gc;

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindObjectOfType<GameController>();
        tc = GameObject.FindObjectOfType<TowerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            gc.score += 100 * scoreValue;
            //gc.damageTaken -= 0.5f * scoreValue;
            tc.healthSystem.Heal(5);
            Debug.LogWarning("enemy killed");
        }
    }
}
