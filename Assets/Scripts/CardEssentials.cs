using UnityEngine;
using System.Collections;

public class CardEssentials : MonoBehaviour {

    public int cost;
    public GameObject tower;
    public bool goldTower;
    public static bool goldPlaced;
    public UnityEngine.UI.Text goldCost;
    public bool selected;
    static float selectionScale = 1.3f;
    Vector3 regularScale;
    SpriteRenderer rend;

	// Use this for initialization
	void Start ()
    {
        regularScale = transform.localScale;
        rend = GetComponent<SpriteRenderer>();
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
        {
            transform.localScale = regularScale * selectionScale;
            rend.sortingOrder = 10;
            goldCost.canvas.sortingOrder = 10;
        }
        else
        {
            transform.localScale = regularScale;
            rend.sortingOrder = 1;
            goldCost.canvas.sortingOrder = 1;
        }
	}
}
