using UnityEngine;
using System.Collections;

public class EnemyBasics : MonoBehaviour {

    public float speed;
    public float totalHealth;
    float health;
    float distance;
    int index = 0;

	// Use this for initialization
	void Start ()
    {
        health = totalHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (index < LevelPath.path.Length) FollowPath();
        if (health <= 0) Destroy(gameObject);
	}

    void FollowPath()
    {
        distance += Time.deltaTime * speed;
        transform.position = Vector3.MoveTowards(transform.position, LevelPath.path[index], speed);
        if (transform.position == LevelPath.path[index]) index++;
    }

    public float GetDistance()
    {
        return distance;
    }

    public void DealDamage(float damage)
    {
        health -= damage;
    }

}
