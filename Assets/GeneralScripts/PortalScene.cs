using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PortalScene : MonoBehaviour {

    // Use this for initialization
    // Use this for initialization
    public GameObject LoadingScreen;
    public Slider slider;
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name.Equals("Character"))
        {
            PlayerPrefs.SetInt("LevelPassed", SceneManager.GetActiveScene().buildIndex);
            StartCoroutine("LoadLevel");
        }
        
    }
    private IEnumerator LoadLevel()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        LoadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progress;

            yield return null;
        }


    }
}
