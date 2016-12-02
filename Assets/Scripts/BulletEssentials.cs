using UnityEngine;
using System.Collections;

public class BulletEssentials : MonoBehaviour {

    float damage;
    float speed;
    GameObject target;
    Vector3 newPosition;


	// Use this for initialization
	void Start ()
    {
        newPosition = target.transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        MoveTowardsTarget();

	}

    public void SetVariables(float _damage, float _speed, GameObject _target)
    {
        damage = _damage;
        speed = _speed;
        target = _target;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            OnHit();
            
        }
    }

    void MoveTowardsTarget()
    {
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed);
            newPosition = target.transform.position;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, newPosition, speed);
            if (transform.position == newPosition)
                Destroy(gameObject);
        }
    }

    protected virtual void OnHit()
    {
        if (target != null)
            target.GetComponent<EnemyBasics>().DealDamage(damage);
        Destroy(gameObject);
    }
}
