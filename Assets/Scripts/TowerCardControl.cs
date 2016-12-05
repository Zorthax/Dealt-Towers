using UnityEngine;
using System.Collections;

public class TowerCardControl : MonoBehaviour {

    GameObject tower;
    int cost;
    bool overrideMap;
    bool map;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {

        //transform.position = new Vector3(-10, -10, 0);
        if (Input.GetMouseButtonDown(0))
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 1);
        }

    }

    void LateUpdate()
    {
        if (!overrideMap && map && tower != null)
        {
            Instantiate(tower, transform.position, new Quaternion(0, 0, 0, 1));
            ImportantStats.gold -= cost;
            transform.position = new Vector3(-10, -10, 0);
            tower = null;
            cost = 0;
        }
        overrideMap = false;
        map = false;

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Card")
        {
            CardEssentials script = other.GetComponent<CardEssentials>();
            tower = script.tower;
            cost = script.cost;
            overrideMap = true;
        }
        else if (other.tag == "Map" && ImportantStats.gold >= cost)
        {
            map = true;
        }
        else if (other.tag == "Map" && ImportantStats.gold < cost)
        {
            transform.position = new Vector3(-10, -10, 0);
        }
    }
}
