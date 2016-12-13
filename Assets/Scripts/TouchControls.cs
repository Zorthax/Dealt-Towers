using UnityEngine;
using System.Collections;

public class TouchControls : MonoBehaviour {

    public float dragDelay = 0.75f;
    GameObject tower;
    int cost;
    bool overrideMap;
    bool map;
    GameObject selectedTower;
    CardEssentials selectedCard;
    float dragTimer;

	// Use this for initialization
	void Start ()
    {
        dragTimer = dragDelay;
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
        
        if (Input.GetMouseButton(0))
        {
            DragTower();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (selectedTower != null && selectedTower.GetComponent<TowerEssentials>().onPath)
                Deselection();
            else if (selectedTower != null)
                selectedTower.GetComponent<TowerEssentials>().pickedUp = false;
            dragTimer = dragDelay;
        }

    }

    void DragTower()
    {
        if (selectedTower != null)
        {
            if (dragTimer <= 0)
            {
                selectedTower.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 1);
                selectedTower.GetComponent<TowerEssentials>().pickedUp = true;
            }
            else
                dragTimer -= Time.deltaTime;
        }
    }

    void Deselection()
    {
        if (selectedTower != null)
        {
            TowerEssentials t = selectedTower.GetComponent<TowerEssentials>();
            selectedTower.GetComponent<TowerEssentials>().pickedUp = false;
            if (t != null) t.selected = false;
            selectedTower = null;
        }
        if (selectedCard != null)
        {
            CardEssentials c = selectedCard.GetComponent<CardEssentials>();
            if (c != null) c.selected = false;
            selectedCard = null;
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
            selectedTower.GetComponent<TowerEssentials>().cost = cost;
            MoveSelector();
            tower = null;
            cost = 0;
            dragTimer = 0;
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
                    TowerEssentials t = col.GetComponent<TowerEssentials>();
                    if (t != null) t.selected = true;
                    selectedTower = col.gameObject;
                    MoveSelector();
                    solved = true;
                    dragTimer = dragDelay;
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
                    selectedCard = col.GetComponent<CardEssentials>();
                    tower = selectedCard.tower;
                    cost = selectedCard.cost;
                    selectedCard.selected = true;
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
