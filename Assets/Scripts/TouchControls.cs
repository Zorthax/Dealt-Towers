using UnityEngine;
using System.Collections;

public class TouchControls : MonoBehaviour {

    GameObject tower;
    int cost;
    bool overrideMap;
    bool map;
    GameObject selectedTower;

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
            CheckAllCollisions();
        }
        

    }

    void LateUpdate()
    {
        
        overrideMap = false;
        map = false;

        //if (Input.GetMouseButtonUp(0))
        //{
        //    transform.position = new Vector3(-10, -10, 0);
        //}
    }

    void Deselection()
    {
        if (selectedTower != null)
        {
            TowerEssentials t = selectedTower.GetComponent<TowerEssentials>();
            if (t != null) t.selected = false;
            selectedTower = null;
        }
    }

    void MoveSelector()
    {
        transform.position = new Vector3(-10, -10, 0);
    }
    
    void CreateTower()
    {
        if (tower != null)
        {
            selectedTower = Instantiate(tower, transform.position, new Quaternion(0, 0, 0, 1)) as GameObject;
            ImportantStats.gold -= cost;
            MoveSelector();
            tower = null;
            cost = 0;
        }
    }

    void CheckAllCollisions()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, 0.01f);
        bool solved = false;

        if (!solved) foreach (Collider2D col in collisions)
            {
                if (col.tag == "Tower")
                {
                    Deselection();
                    col.GetComponent<TowerEssentials>().selected = true;
                    selectedTower = col.gameObject;
                    MoveSelector();
                    solved = true;
                }
            }

        if (!solved) foreach (Collider2D col in collisions)
            {
                if (col.tag == "Pause")
                {
                    if (ImportantStats.speed == 0) ImportantStats.speed = 1;
                    else ImportantStats.speed = 0;
                    overrideMap = true;
                    MoveSelector();
                    solved = true;
                }
            }
        if (!solved) foreach (Collider2D col in collisions)
            {
                if (col.tag == "FastForward")
                {
                    if (ImportantStats.speed != FastForward.fastSpeed) ImportantStats.speed = FastForward.fastSpeed;
                    else ImportantStats.speed = 1;
                    overrideMap = true;
                    MoveSelector();
                    solved = true;
                }
            }
        if (!solved) foreach (Collider2D col in collisions)
            {
                if (col.tag == "Card")
                {
                    Deselection();
                    CardEssentials script = col.GetComponent<CardEssentials>();
                    tower = script.tower;
                    cost = script.cost;
                    overrideMap = true;
                    MoveSelector();
                    solved = true;
                }
            }
        if (!solved) foreach (Collider2D col in collisions)
            {
                if (col.tag == "Map" && ImportantStats.gold >= cost)
                {
                    Deselection();
                    CreateTower();
                    MoveSelector();
                    map = true;
                    solved = true;
                }
            }
        if (!solved) foreach (Collider2D col in collisions)
            {
                if (col.tag == "Map" && ImportantStats.gold < cost)
                {
                    Deselection();
                    MoveSelector();
                    solved = true;
                }
            }
    }
}
