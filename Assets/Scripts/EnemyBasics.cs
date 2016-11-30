using UnityEngine;
using System.Collections;

public class EnemyBasics : MonoBehaviour {

    public float speed;
    int index = 0;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (index < LevelPath.path.Length) FollowPath();
	}

    void FollowPath()
    {
        transform.position = Vector3.MoveTowards(transform.position, LevelPath.path[index], speed);
        if (transform.position == LevelPath.path[index]) index++;
    }
}
