using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;
    public HealthBar healthBar;
    private float healthBarSize;
    private float initialDuration = 30f;
    private float maxWidth;
    private float screenHeight = 10f;
    public float damageTaken;
    public float score;

    // Start is called before the first frame update
    void Start()
    {
        healthBarSize = 1f;
        healthBar.SetSize(healthBarSize);

        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        Vector3 targetWidth = Camera.main.ScreenToWorldPoint(upperCorner);
        maxWidth = targetWidth.x;
        StartCoroutine(Enemy());

    }

    private void Update()
    {
        Debug.Log("damage taken: " + damageTaken);
        if (damageTaken != 0 && healthBarSize > 0)
        {
            healthBarSize -= damageTaken / 7.5f;
            if (healthBarSize < 0)
                healthBarSize = 0;

            if (healthBarSize > 1f)
                healthBarSize = 1f;

            Debug.LogWarning("health: " + healthBarSize);

            healthBar.SetSize(healthBarSize);
            damageTaken = 0;
        }

        if (healthBarSize <= 0)
        {
            StopCoroutine(Enemy());
        }

    }

    IEnumerator Enemy()
    {
        while (healthBarSize > 0)
        {
            GameObject enemy = enemies[0];

            Vector3 spawnPosition = new Vector3(Random.Range(-maxWidth * 0.95f, maxWidth * 0.95f), screenHeight);

            Instantiate(enemy, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(0.25f, 1.25f));
        }
    }

}
