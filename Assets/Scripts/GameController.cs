using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;
    public GameObject canvasBackground;
    public GameObject startButton;
    public GameObject restartButton;
    public Text scoreText;
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
        score = 0;
        healthBarSize = 1f;
        healthBar.SetSize(healthBarSize);

        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        Vector3 targetWidth = Camera.main.ScreenToWorldPoint(upperCorner);
        maxWidth = targetWidth.x;
        UpdateText();
    }

    public void StartGame()
    {
        canvasBackground.SetActive(false);
        startButton.SetActive(false);
        StartCoroutine(Enemy());
    }

    private void StopGame()
    {
        StopCoroutine(Enemy());
        canvasBackground.SetActive(true);
        restartButton.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

            UpdateText();
        }

        if (healthBarSize <= 0)
        {
            StopGame();
        }

    }

    IEnumerator Enemy()
    {
        yield return new WaitForSeconds(0.5f);

        while (healthBarSize > 0)
        {
            GameObject enemy = enemies[0];

            Vector3 spawnPosition = new Vector3(Random.Range(-maxWidth * 0.95f, maxWidth * 0.95f), screenHeight);
            Instantiate(enemy, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(0.25f, 1.25f));
        }
    }

    void UpdateText()
    {
        scoreText.text = "Score: " + score;
    }

}
