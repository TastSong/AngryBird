using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelAsync : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Screen.SetResolution(1600, 1000, false);
        Invoke("Loading", 2);
	}
	void Loading()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
