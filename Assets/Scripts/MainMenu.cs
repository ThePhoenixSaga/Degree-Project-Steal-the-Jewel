using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void StartGame() //Load first level
    {
        SceneManager.LoadScene("Scenes/level_1", LoadSceneMode.Single);
    }

    public void QuitGame() //Close down application
    {
        Application.Quit();
    }
}
