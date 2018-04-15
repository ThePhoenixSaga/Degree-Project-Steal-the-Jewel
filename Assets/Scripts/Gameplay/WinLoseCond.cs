using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseCond : MonoBehaviour {

    public bool returnedToStart; //Has player returned to exit/ starting area with main gem already collected?
    public GameObject exit; //For referencing where the exit is
    Collecting collecting; //For referencing the collecting script

    // Use this for initialization
    void Start () {
        returnedToStart = false; //Default value
        exit = GameObject.FindGameObjectWithTag("startingArea"); //Assigns gameobject with appropiate tag to exit object. In this case the gameobject will be emply but contains a large box collider covering the exit/ starting area.
        collecting = GetComponent<Collecting>(); //To enable use of variables and functions from collecting script
    }

    //When player has reached/ collided with object designated as exit/ starting area. 
    public void OnTriggerEnter(Collider other) //other referes to whatever object the player has collided with
    {
        var exit = other.gameObject.GetComponent<Collider>(); //Assigns object tagged from other variable to exit variable
        //If player has reached exit and has got the main gem
        if (exit.gameObject.tag == "startingArea" && collecting.GetComponent<Collecting>().gotObjective == true)
        {
            returnedToStart = true; //Tells GameplayManager script player has acheived the objective and is at the starting area
        }
    }
}
