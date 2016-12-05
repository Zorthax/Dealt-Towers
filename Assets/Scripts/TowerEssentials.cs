using UnityEngine;
using System.Collections;

public class TowerEssentials : MonoBehaviour {

    public float delayBetweenShots = 0.5f;
    public GameObject bullet;
    public float damage;
    public float bulletSpeed;
    public float radius;
    float delayTimer = 0;

    bool showRadius = true;
    GameObject radiusCircle;
    GameObject[] enemiesInSight;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckIfEnemyIsDead();
        DrawRadius();
        Shooting();
        
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

    void Shooting()
    {
        if (delayTimer > 0) delayTimer -= Time.deltaTime;

        else if (enemiesInSight != null)
        {
            GameObject bul = Instantiate(bullet, transform.position, new Quaternion(0, 0, 0, 1)) as GameObject;
            GameObject last = enemiesInSight[0];
            foreach(GameObject e in enemiesInSight)
            {
                if (e.GetComponent<EnemyBasics>().GetDistance() > last.GetComponent<EnemyBasics>().GetDistance()) //Shoot at enemy closest to end
                    last = e;
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

}
