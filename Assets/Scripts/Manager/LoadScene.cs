using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    private AsyncOperation sceneLoad;
    public Slider loadingSlider;

    void Start()
    {
        sceneLoad = SceneManager.LoadSceneAsync(LoadingData.sceneToLoad);
    }

    private void Update()
    {
        loadingSlider.value = Mathf.Clamp01(sceneLoad.progress / 0.9f);
    }
}