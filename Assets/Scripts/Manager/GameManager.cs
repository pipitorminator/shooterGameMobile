using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int totalPoints;
    public int scoreRecord;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        totalPoints = PlayerPrefs.GetInt(PlayerPrefsConstants.TotalPoints);
        scoreRecord = PlayerPrefs.GetInt(PlayerPrefsConstants.ScoreRecord);
    }
}