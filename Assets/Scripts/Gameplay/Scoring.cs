using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Scoring : MonoBehaviour {

    public int score; //Player's score count
    public GameObject scoreUI; //UI object for displaying score
    public Text scoreText; //Output Text property from score UI object

    Collecting collecting; //Declare collecting as Collecting type script

    // Use this for initialization
    void Start()
    {
        score = 0; ///Defualt value starting at 0
        scoreUI = GameObject.Find("Score"); //Gets score UI object
        scoreText = scoreUI.GetComponent<Text>(); //Gets Text proerpty from score UI object

        collecting = GetComponent<Collecting>(); //Gets Collecting script for referencing its variables and functions
    }

    // Update is called once per frame
    void Update()
    {
        //collectedSmallGem equals true if player has collected a small gem
        if(collecting.GetComponent<Collecting>().CollectedSmallGem == true) //        
        {
            score = score + 100; //Add to score
            scoreText.text = score.ToString(); //Writes to score UI text 
            collecting.GetComponent<Collecting>().CollectedSmallGem = false; //Resets to false to prevent looping from adding score
        }

        //collectedMainGem equals true if player has collected the main gem
        if (collecting.GetComponent<Collecting>().CollectedMainGem == true)
        {
            score = score + 1000; //Add to score
            scoreText.text = score.ToString(); //Writes to score UI text 
            collecting.GetComponent<Collecting>().CollectedMainGem = false;//Resets to false to prevent looping from adding score 
        }
    }
}
