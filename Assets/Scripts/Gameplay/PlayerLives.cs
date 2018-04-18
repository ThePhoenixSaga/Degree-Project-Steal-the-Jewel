using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerLives : MonoBehaviour {

    //For getting original transform values for level reset
    public GameObject enemy; //For referencing enmey object
    public GameObject player; //For referencing player object

    public Vector3 orgEnemyTrans; //inital starting position
    public Vector3 orgPlayerTrans; //Intial starting position

    public int playerLives = 3; //Pulls playerLives from static class
    public bool outOfLives; //Has the player ran out of lives?

    public GameObject playerLivesUI; //Player live's UI object
    public Text playerLivesText; //Output text for player live's UI object

    // Use this for initialization
    void Start () {

        enemy = GameObject.FindGameObjectWithTag("Enemy"); //Get enemy object
        player = GameObject.FindGameObjectWithTag("Player"); //Get player object

        orgEnemyTrans = enemy.GetComponent<Transform>().position; //Get starting position
        orgPlayerTrans = player.GetComponent<Transform>().position; //Get starting position

        outOfLives = false; //Default value

        playerLivesUI = GameObject.Find("LivesText"); //Gets player lives UI object
        playerLivesText = playerLivesUI.GetComponent<Text>(); //Gets Text property from player's lives UI object
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
            playerLives = playerLives - 1; //Deducts player lives by 1

            //Reset player's and AI's position
            enemy.transform.position = orgEnemyTrans; //Set current position to original position
            player.transform.position = orgPlayerTrans; //Set current position to original position
        }
    }
}
