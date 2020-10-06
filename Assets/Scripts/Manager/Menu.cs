using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void IniciarJogo()
    {
        LoadingData.sceneToLoad = 1;
        SceneManager.LoadScene(2);
    }

    public void Online()
    {
        LoadingData.sceneToLoad = 3;
        SceneManager.LoadScene(2);
    }
}