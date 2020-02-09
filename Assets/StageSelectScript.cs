using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelectScript : MonoBehaviour
{
    // Use this for initialization
    public GameObject LoadingScreen;
    public Slider slider;
    public void SelectStage1()
    {
        StartCoroutine("LoadStage1");
    }
    IEnumerator LoadStage1()
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
    public void GoBack()
    {
        transform.gameObject.SetActive(false);
    }
    public void SelectStage2()
    {
        //do nothing now

    }
}
