using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    //How sensitive should we be to sight
    public enum SightSense { STRICT, LOOSE };

    //Sight sensitivity
    public SightSense Sense = SightSense.STRICT;

    //can we see target
    public bool CanSeeTarget = false;

    //FOV
    public float FieldOfView = 90f;

    //reference to target
    public Transform Player = null;

    //reference to eyes
    public Transform EyePoint = null;

    //reference to transform component
    public Transform ThisTransform = null;

    //reference to sphere collider
    private SphereCollider ThisCollider = null;

    //reference to last known object sighting, if any
    public Vector3 LastKnowSighting = Vector3.zero;

    //public Vector3 lastPlayerPosition;
    public Vector3 currentPlayerPosition;

    public bool playerNearby = false;

    private void Awake()
    {
        ThisTransform = GetComponent<Transform>();
        ThisCollider = GetComponent<SphereCollider>();
        LastKnowSighting = ThisTransform.position; //gets player postion once game starts
        //LastKnowSighting = Vector3.zero; //resets to zero to prevent ai thinking it already seen player
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        EyePoint = GameObject.FindGameObjectWithTag("Eyes").GetComponent<Transform>();
    }

    bool InFOV()
    {
        //get direction to target
        Vector3 DirToTarget = Player.position - EyePoint.position;

        //get angle between forward and look direction
        float Angle = Vector3.Angle(EyePoint.forward, DirToTarget);

        //are we within feild of view?
        if (Angle <= FieldOfView)
        {
            //Debug.Log("Within View");
            currentPlayerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

            return true;
        }

        //not within view
        return false;
    }

    bool ClearLineOfSight()
    {
        RaycastHit Info;
        //Current issue, raycast appears to be 
        if (Physics.Raycast(EyePoint.position, (Player.position - EyePoint.position).normalized, out Info, ThisCollider.radius)) //4 ThisCollider.radius
        {
            //if player, then can see player
            if (Info.transform.CompareTag("Player"))
            {
                //Debug.DrawRay(EyePoint.position, (Player.position - EyePoint.position).normalized, Color.yellow, 100000);
                //Debug.Log("Raycast hit player");
                LastKnowSighting = currentPlayerPosition;
                return true;
            }

            //Debug.Log("Raycast is hitting something");
            //Debug.DrawRay(EyePoint.position, (Player.position - EyePoint.position).normalized, Color.yellow, 100000);
        }
        if (currentPlayerPosition != LastKnowSighting)
        {
            //playerNearby = true;
            TrackingPlayer();
            //Debug.Log("Current position does not equal to last player position");

        }
        else
        {
            playerNearby = false;
            currentPlayerPosition = Vector3.zero;
        }
        //Debug.Log("Raycast no longer hitting player");
        return false;
    }

    void UpdateSight()
    {
        switch (Sense)
        {
            case SightSense.STRICT: CanSeeTarget = InFOV() && ClearLineOfSight();
                break;
            case SightSense.LOOSE: CanSeeTarget = InFOV() || ClearLineOfSight();
                break;
        }
    }

    void TrackingPlayer()
    {
        //Debug.Log("I'm searching for you!");
    }

    private void Update()
    {
        ClearLineOfSight();
    }

    void OnTriggerStay(Collider Other)
    {
        UpdateSight();
        //Debug.Log("Player within sphere collidor"); //Note: Triggers message despite the player not cautlly within radious collidor on enemy, needs investigating why.
        //update last known sighting
        if (CanSeeTarget)
        {
            LastKnowSighting = Player.position;
            //Debug.Log("I saw you at: "+LastKnowSighting);
        }
    }

    void OnTriggerExit(Collider Other)
    {
        if (!Other.CompareTag("Player"))
        {
            return;
        }
        CanSeeTarget = false;
    }
}
