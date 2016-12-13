using UnityEngine;
using System.Collections;

public class TowerEssentials : MonoBehaviour {

    public float delayBetweenShots = 0.5f;
    public GameObject bullet;
    public float damage;
    public float bulletSpeed;
    public float radius;
    float delayTimer = 0;
    Color naturalColor;
    public bool onPath = false;
    public bool pickedUp;
    static float placeDelay = 0.2f;
    float placeTimer = 0;
    Vector3 lastPosition;
    public int cost;
    bool costApplied = false;

    public bool selected = false;
    GameObject radiusCircle;
    GameObject[] enemiesInSight;

    public bool randomShooting;

    public bool goldTower;
    public int goldProduced;
    public float goldDelay;
    float goldTimer;

    // Use this for initialization
    void Start ()
    {
        delayTimer = delayBetweenShots;
        selected = true;
        naturalColor = GetComponent<SpriteRenderer>().color;
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckCollision();
        CheckIfEnemyIsDead();
        DrawRadius();
        if (!pickedUp && !onPath) Shooting();
        if (goldTower) GoldProduction();

    }

    void LateUpdate()
    {
        Placeable();
    }

    void GoldProduction()
    {
        if (goldTimer < goldDelay) goldTimer += ImportantStats.deltaTime;
        else
        {
            goldTimer = 0;
            ImportantStats.gold += goldProduced;
        }
    }

    void DrawRadius()
    {
        if (selected && radiusCircle == null)
        {
            radiusCircle = Instantiate(Resources.Load("Radius")) as GameObject;
            radiusCircle.transform.parent = transform;
            radiusCircle.transform.localPosition = Vector3.zero;
            radiusCircle.transform.localScale = new Vector3(radius, radius, radius);
        }
        else if (!pickedUp && !selected && radiusCircle != null)
        {
            radiusCircle.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        }
        else if (!pickedUp && selected && radiusCircle != null)
        {
            radiusCircle.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        }
    }

    void Shooting()
    {
        if (delayTimer > 0) delayTimer -= ImportantStats.deltaTime;

        else if (enemiesInSight != null)
        {
            GameObject bul = Instantiate(bullet, transform.position, new Quaternion(0, 0, 0, 1)) as GameObject;
            GameObject last = enemiesInSight[0];
            if (randomShooting)
            {
                last = enemiesInSight[Random.Range(0, enemiesInSight.Length - 1)];
            }
            else 
            {
                foreach (GameObject e in enemiesInSight)
                {
                    if (e.GetComponent<EnemyBasics>().GetDistance() > last.GetComponent<EnemyBasics>().GetDistance()) //Shoot at enemy closest to end
                        last = e;
                }
            }
            bul.GetComponent<BulletEssentials>().SetVariables(damage, bulletSpeed, last);
            delayTimer = delayBetweenShots;
        }
    }

    void CheckIfEnemyIsDead()
    {
        if (enemiesInSight != null)
        for (int i = 0; i < enemiesInSight.Length; i++)
        {
            if (enemiesInSight[i] == null)
            {
                RemoveEnemy(enemiesInSight[i]);
                if (enemiesInSight == null)
                    break;
                else
                {
                    i = enemiesInSight.Length;
                    CheckIfEnemyIsDead();
                }
            }
        }
    }

    public void AddEnemy(GameObject enemy)
    {
        //Create new array if doesn't yet contain enemies
        if (enemiesInSight == null)
        {
            enemiesInSight = new GameObject[1];
            enemiesInSight[0] = enemy;
        }
        //Add new enemy to array
        else
        {
            GameObject[] temp = new GameObject[enemiesInSight.Length + 1]; //temp array to hold enemies
            for (int i = 0; i < enemiesInSight.Length; i++)
            {
                temp[i] = enemiesInSight[i];
            }
            temp[enemiesInSight.Length] = enemy;
            enemiesInSight = temp;
        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        if (enemiesInSight != null)
        {
            if (enemiesInSight.Length == 1)
                enemiesInSight = null;

            else
            {
                //Add everything but enemy to temp array
                GameObject[] temp = new GameObject[enemiesInSight.Length - 1];
                for (int i = 0, j = 0; i < enemiesInSight.Length; i++)
                {
                    if (enemiesInSight[i] != enemy)
                    {
                        temp[j] = enemiesInSight[i];
                        j++;
                    }
                }
                enemiesInSight = temp;
            }
        }
    }

    void CheckCollision()
    {
        onPath = false;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, transform.localScale.x / 5, new Vector2(0, 0));
        foreach(RaycastHit2D h in hits)
        {
            if (h.transform.tag == "Path" || h.transform.tag == "Pause" ||
            h.transform.tag == "FastForward" || h.transform.tag == "Card")
            {
                onPath = true;
                break;
            }

            if (h.transform.tag == "Tower" && h.transform != transform)
            {
                onPath = true;
                break;
            }

        }
    }

    void Placeable()
    {
        if (selected && onPath) //Go red while being held above path
        {
            Color c = new Color(1, 0, 0, 1);
            GetComponent<SpriteRenderer>().color = c;
            radiusCircle.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);
            placeTimer = placeDelay;
        }
        if (!selected && onPath) //Either reset position or destroy self when placed on path
        {
            if (placeTimer <= 0)
            {
                if (lastPosition != new Vector3(0, 0, 0))
                {
                    transform.position = lastPosition;
                    GetComponent<SpriteRenderer>().color = naturalColor;
                    pickedUp = false;
                    onPath = false;
                }
                else
                    Destroy(gameObject);
            }
            else
            {
                placeTimer -= Time.deltaTime;
            }
        }
        if (!pickedUp && !onPath) //Set last position
        {
            if (!costApplied)
            {
                if (goldTower) CardEssentials.goldPlaced = true;
                ImportantStats.gold -= cost;
                costApplied = true;
            }
            
            lastPosition = transform.position;
            placeTimer = 0;
        }

        if (!onPath)
        {
            GetComponent<SpriteRenderer>().color = naturalColor;
            lastPosition = transform.position;
        }

        if (pickedUp && !onPath)
        {
            GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.6f, 1, 1);
            radiusCircle.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0.5f);
        }
    }

}
