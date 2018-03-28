using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WaypointSystem : MonoBehaviour
{

    //Change color of waypoints
    public Color PathObjectColor = Color.green;
    public Color PathLinesColor = Color.red;

    //creates list of path objects
    public List<Transform> waypoints = new List<Transform>();

    //holds number of path objects
    int PathObjectIndex = 0;

    //allows path following to be disabled during play mode, useful for telling AI to stop following
    public bool disableInGame;

    // Update is called once per frame
    void Update()
    {

        if (!disableInGame)
        {
            //auto adds path objects as they are made int he editor
            Transform[] PathObjects = GetComponentsInChildren<Transform>();

            if (waypoints.Count > 0)
            {
                waypoints.Clear();

                PathObjectIndex = 0; //defaults to 0 while no path objects exist

                //Renames objects automatically.
                foreach (Transform t in PathObjects)
                {
                    if (t != transform)
                    {
                        t.name = "PathObj_0" + PathObjectIndex.ToString();

                        if (waypoints.Count > 9) //rename after 9th waypoint
                        {
                            t.name = "PathObj_" + PathObjectIndex.ToString();
                        }
                        
                        waypoints.Add(t);
                        PathObjectIndex++;
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (waypoints.Count > 0)
        {
            //Draws the path objects
            Gizmos.color = PathObjectColor;

            foreach (Transform t in waypoints)
            {
                Gizmos.DrawSphere(t.position, 1f);
            }

            //Draws the connected lines between the path objects
            Gizmos.color = PathLinesColor;

            for (int a = 0; a < waypoints.Count - 1; a++)
            {
                Gizmos.DrawLine(waypoints[a].position, waypoints[a + 1].position);
            }
        }
    }


    public void TestMessage()
    {
        Debug.Log("Testing message");
    }
}
