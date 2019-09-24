using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;
    [SerializeField]
    public List<ScriptableWeapon> availableWeapons;
    public GameObject mainMenu;
    public GameObject gameOverMenu;
    public GameObject startButton;
    public GameObject playAgainButton;
    public GameObject restartButton;
    public TextMeshProUGUI scoreTMP;
    public GameObject UIplayerInfo;
    private float maxWidth;
    private float screenHeight = 10f;

    public static float score = 0;

    public TowerController tc;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        Vector3 targetWidth = Camera.main.ScreenToWorldPoint(upperCorner);
        maxWidth = targetWidth.x;
        UpdateText();

        mainMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        UIplayerInfo.SetActive(false);
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        UIplayerInfo.SetActive(true);
        StartCoroutine(Enemy());
    }

    public void PlayAgain()
    {
        tc.RestartPlayer();
        StartGame();
    }

    private void StopGame()
    {
        StopCoroutine(Enemy());
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        UIplayerInfo.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        UpdateText();
    }

    IEnumerator Enemy()
    {
        yield return new WaitForSeconds(0.5f);

        Debug.Log(tc.healthSystem.GetHealthPercent());

        while (tc.healthSystem.GetHealthPercent() > 0)
        {
            GameObject enemy = enemies[0];

            Vector3 spawnPosition = new Vector3(Random.Range(-maxWidth * 0.95f, maxWidth * 0.95f), screenHeight);
            Instantiate(enemy, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(0.25f, 1.25f));
        }

        StopGame();
    }

    void UpdateText()
    {
        scoreTMP.text = "Score: " + score;
    }

}
