using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    //General variables
    int buildIndex;
    public GameObject pauseMenuScreen;
    //Player variables
    public GameObject player;
    //Game over variables
    public static bool isGameOver;
    public GameObject gameOverScreen;
    public GameObject controlsPanel;
    public GameObject healthBar;
    public GameObject coinsCounter;
    public GameObject pause;
    //Coin variables
    public static int howManyCoins;
    public TextMeshProUGUI coinsText;
    //Boss variables
    public static bool metBoss;
    public GameObject bossLife;
    //Game won variables
    public static bool gameWon;
    public GameObject winning;

    void Awake()
    {
        isGameOver = false;
        metBoss = false;
        gameWon = false;
        buildIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Start()
    {
        howManyCoins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        coinsText.text = ""+howManyCoins;
        if (isGameOver||gameWon)
        {
            PopulateLeaderboard.SendLeaderBoard(howManyCoins);
            controlsPanel.SetActive(false);
            healthBar.SetActive(false);
            coinsCounter.SetActive(false);
            player.SetActive(false);
            pauseMenuScreen.SetActive(false);
            pause.SetActive(false);
            if (metBoss)
                bossLife.SetActive(false);
            if (isGameOver)
                gameOverScreen.SetActive(true);
            if (gameWon)
                winning.SetActive(true);
            gameObject.SetActive(false);
        }
        if (metBoss&&!gameWon&&!isGameOver)
            bossLife.SetActive(true);
    }

    public void ReplayLevel()
    {
        ResumeGame();
        howManyCoins = 0;
        SceneManager.LoadScene(buildIndex);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        controlsPanel.SetActive(false);
        pauseMenuScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenuScreen.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(2);
    }
}
