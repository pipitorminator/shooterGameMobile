using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;

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
}