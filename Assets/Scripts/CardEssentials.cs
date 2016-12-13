using UnityEngine;
using System.Collections;

public class CardEssentials : MonoBehaviour {

    public int cost;
    public GameObject tower;
    public bool goldTower;
    public static bool goldPlaced;
    public UnityEngine.UI.Text goldCost;
    public bool selected;
    static float selectionScale = 1.5f;
    Vector3 regularScale;

	// Use this for initialization
	void Start ()
    {
        regularScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (goldTower && goldPlaced)
        {
            cost = Mathf.FloorToInt(cost * 1.5f);
            goldPlaced = false;
        }

        goldCost.text = cost.ToString();

        if (selected)
            transform.localScale = regularScale * selectionScale;
        else
            transform.localScale = regularScale;
	}
}
