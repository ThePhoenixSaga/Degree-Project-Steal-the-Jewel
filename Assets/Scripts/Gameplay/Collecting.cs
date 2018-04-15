using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Collecting : MonoBehaviour {

    //Booleans to determine if the player has collected a gem, used in other scripts to exeucte other commands.
    public bool CollectedSmallGem = false; //Has player just collected a small gem?
    public bool CollectedMainGem = false; //Has player just collected the main gem?
    public bool gotObjective = false; //Has player got main gem when reaching back to the exit/ starting area?

    //Sets up audio object variables for sound effects playbacks.
    AudioSource audioSource;
    public AudioClip soundCollectSmallGem;
    public AudioClip soundCollectMainGem;

    public int numberOfSmallGemsCol; //Number of small gems collected.
    public GameObject numberOfSmallGemsColUI; //UI object for small gems counter.
    public Text numberOfSmallGemsText; //Text output for small gems counter value.

    public int numberOfMainGemsCol; //Number of main gems collected
    public GameObject numberOfMainGemsColUI; //UI object for main gems counter
    public Text numberOfMainGemsText; //Text output for main gems counter

    // Use this for initialization
    void Start () {

        //Loads sound effects from Resource folder, so these are ready to play upon request
        audioSource = GetComponent<AudioSource>();
        soundCollectSmallGem = (AudioClip)Resources.Load("Sounds/player_collectsmallgem"); //For collecting a small gem
        soundCollectMainGem = (AudioClip)Resources.Load("Sounds/player_collectmaingem"); //For collecting the main gem

        numberOfSmallGemsCol = 0; //How many small gems have been collected
        numberOfSmallGemsColUI = GameObject.Find("SmallGemText"); //Gets small gems UI object
        numberOfSmallGemsText = numberOfSmallGemsColUI.GetComponent<Text>(); //Gets Text property from small gems UI to write to

        numberOfMainGemsCol = 0; //How many main gems have been collected
        numberOfMainGemsColUI = GameObject.Find("MainGemText"); //Gets main gems UI object
        numberOfMainGemsText = numberOfMainGemsColUI.GetComponent<Text>(); //Gets Text proerty from main gems UI to write to
    }

    //Triggers when player has entered collsion box for any gem objects in level
    private void OnTriggerEnter(Collider other) //other referes to whatever collision object the player object (or whatever object this script is attached to) has collided with
    {
        var gemCollider = other.gameObject.GetComponent<Collider>(); //Assigns variable with collider componenet from other object

        //If collided with small gem
        if (gemCollider.gameObject.tag == "SmallGem")
        {
            audioSource.PlayOneShot(soundCollectSmallGem); //Play sound effect
            CollectedSmallGem = true; //Tells score script to add points
            numberOfSmallGemsCol++; //Collected a small gem
            numberOfSmallGemsText.text = numberOfSmallGemsCol.ToString(); //Writes to small gem UI to show how mnay gems the player has collected.
            Destroy(other.gameObject); //Removes collided object
        }

        //If collided with main gem
        if (gemCollider.gameObject.tag == "MainGem")
        {
            audioSource.PlayOneShot(soundCollectMainGem); //Plaer sound effect
            CollectedMainGem = true; //Tells score script to add points
            gotObjective = true; //Tells WinLoseCond script player has main gem
            numberOfMainGemsText.text = "1/1"; //Writes to main gem UI to show main gem has been collected
            Destroy(other.gameObject); //Removes collided object
        }
    }
}
