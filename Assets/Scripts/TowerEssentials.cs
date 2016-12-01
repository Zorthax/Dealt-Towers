using UnityEngine;
using System.Collections;

public class TowerEssentials : MonoBehaviour {

    public float delayBetweenShots;
    public GameObject bullet;
    public float radius;

    bool showRadius = true;
    GameObject radiusCircle;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        DrawRadius();
	}

    void DrawRadius()
    {
        if (showRadius && radiusCircle == null)
        {
            radiusCircle = Instantiate(Resources.Load("Radius")) as GameObject;
            radiusCircle.transform.parent = transform;
            radiusCircle.transform.localPosition = Vector3.zero;
            radiusCircle.transform.localScale = new Vector3(radius, radius, radius);
        }
        else if (!showRadius && radiusCircle != null)
        {
            Destroy(radiusCircle);
        }
    }

}
