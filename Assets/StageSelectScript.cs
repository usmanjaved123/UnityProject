using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelectScript : MonoBehaviour
{
    public Button level2button, level3button;
    int levelPassed;
    // Use this for initialization
    public GameObject LoadingScreen;
    public Slider slider;

    void Start()
    {
        levelPassed = PlayerPrefs.GetInt("LevelPassed");
        level2button.interactable = false;
        level3button.interactable = false;
        if(levelPassed==1)
        {
            level2button.interactable = true;
        }
        if (levelPassed == 2)
        {
            level2button.interactable = true;
            level3button.interactable = true;
        }
    }
    public void Selectlevel1()
    {
        StartCoroutine("LoadLevel1");
    }
    IEnumerator LoadLevel1()
    {
        AsyncOperation operation= SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        LoadingScreen.SetActive(true);
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progress;

            yield return null;
        }
        

    }

    public void Selectlevel2()
    {
        StartCoroutine("LoadLevel2");

    }
    IEnumerator LoadLevel2()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 2);
        LoadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progress;

            yield return null;
        }


    }

    public void Selectlevel3()
    {
        StartCoroutine("LoadLevel3");

    }
    IEnumerator LoadLevel3()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 3);
        LoadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progress;

            yield return null;
        }


    }
    public void Selectlevel4()
    {
        StartCoroutine("LoadLevel4");

    }
    IEnumerator LoadLevel4()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 4);
        LoadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progress;

            yield return null;
        }


    }
    public void GoBack()
    {
        transform.gameObject.SetActive(false);
    }
    public void Reset()
    {
        level2button.interactable = false;
        level3button.interactable = false;
        PlayerPrefs.DeleteAll();
    }
}
