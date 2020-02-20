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
    public void Selectlevel(int id)
    {
        StartCoroutine("LoadLevel",id);
    }
    IEnumerator LoadLevel(int id)
    {
        AsyncOperation operation= SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + id);
        LoadingScreen.SetActive(true);
        while(!operation.isDone)
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
