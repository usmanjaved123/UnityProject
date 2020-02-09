using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScene : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name.Equals("Character"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
    }
}
