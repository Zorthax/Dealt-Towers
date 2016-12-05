using UnityEngine;
using System.Collections;

public class CardEssentials : MonoBehaviour {

    public int cost;
    public GameObject tower;
    public bool goldTower;
    public static bool goldPlaced;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (goldTower && goldPlaced)
        {
            cost = Mathf.FloorToInt(cost * 1.5f);
            goldPlaced = false;
        }
	}
}
