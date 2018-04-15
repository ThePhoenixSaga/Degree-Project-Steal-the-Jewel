using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameplayManager : MonoBehaviour {

    //Set up script types
    WinLoseCond winLoseCond;
    PlayerLives playerLives;

    // Use this for initialization
    void Start()
    {
        //Gets scripts for enabling use of there functions and variables
        winLoseCond = GetComponent<WinLoseCond>();
        playerLives = GetComponent<PlayerLives>();
    }

    // Update is called once per frame
    void Update()
    {
        //If player has retreived the main gem and is at the starting location
        if (winLoseCond.returnedToStart == true)
        {
            //Load win screen scene
            SceneManager.LoadScene("Scenes/WinScreen", LoadSceneMode.Single);
        }

        //If player has lost all lives
        if (playerLives.outOfLives == true)
        {
            //Load game over screen
            SceneManager.LoadScene("Scenes/LoseScreen", LoadSceneMode.Single);
        }
    }


}
