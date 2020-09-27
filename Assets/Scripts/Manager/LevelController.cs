using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    public Text scoreText;

    private int score;

    public Animator gameOverCanvas;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void GameOver()
    {
        gameOverCanvas.SetTrigger("GameOver");
    }

    public void UpdateScore(int amount)
    {
        score += amount;
        scoreText.text = "Pontos: " + score;
    }

    public void ReloadScene()
    {
        LoadingData.sceneToLoad = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(2);
    }

    public void GoToMainMenu()
    {
        LoadingData.sceneToLoad = 0;
        SceneManager.LoadScene(2);
    }
}