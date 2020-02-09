using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {


    public GameObject StageSelectmenu;
    public GameObject PlayerUpgrademenu;
    // Use this for initialization
    public void PlayGame () {
        StageSelectmenu.SetActive(true);
    }
    public void UpgradePlayer()
    {
        PlayerUpgrademenu.SetActive(true);
    }
    public void QuitGame()
    {
        //Debug.Log("QUIT!");
        Application.Quit();
    }
}
