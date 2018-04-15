using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerLives : MonoBehaviour {

    public int playerLives; //How many lives the player will have
    public bool outOfLives; //Has the player ran out of lives?

    public GameObject playerLivesUI; //Player live's UI object
    public Text playerLivesText; //Output text for player live's UI object

    // Use this for initialization
    void Start () {
        outOfLives = false; //Default value

        playerLivesUI = GameObject.Find("LivesText"); //Gets player lives UI object
        playerLivesText = playerLivesUI.GetComponent<Text>(); //Gets Text property from player's lives UI object
        playerLives = 3; //Defualt number of player lives to start on each scene load
    }
	
	// Update is called once per frame
	void Update () {
        //If player has ran out of lives
        if (playerLives <= 0)
        {
            outOfLives = true; //Tells GameplayManager player has lost all lives
        }
        playerLivesText.text = playerLives.ToString(); //Writes the current number of player lives.
    }

    //When player is hit by bullet
    private void OnTriggerEnter(Collider other) //other refers to what ever object has hit player
    {
        var bullet = other.gameObject.GetComponent<Collider>(); //Gets collider component from what ever object hit the player

        //If bullet hits player, reset level and deduct 1 from player live's pool
        if (bullet.gameObject.tag == "Bullet")
        {
            playerLives = playerLives--; //Deducts player lives by 1
            //Reloads level 1 scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //SceneManager.LoadScene("Scenes/level_1", LoadSceneMode.Single);

        }
    }
}
