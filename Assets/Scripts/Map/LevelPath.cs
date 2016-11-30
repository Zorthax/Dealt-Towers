using UnityEngine;
using System.Collections;

public class LevelPath : MonoBehaviour {

    public static Vector3[] path;

	// Use this for initialization
	void Start ()
    {
        Waypoint[] waypoints = Object.FindObjectsOfType<Waypoint>();
        path = new Vector3[waypoints.Length];
        foreach (Waypoint w in waypoints)
        {
            path[w.orderNumber] = w.transform.position;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
