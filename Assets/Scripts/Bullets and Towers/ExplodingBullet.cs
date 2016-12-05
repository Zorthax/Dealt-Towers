using UnityEngine;
using System.Collections;

public class ExplodingBullet : BulletEssentials {

    public float explosionRadius;
    float timer = 0.2f;
    int counter = 5;
	
    protected override void OnHit()
    {
        if (target != null)target.GetComponent<EnemyBasics>().DealDamage(damage);
        transform.localScale = new Vector3(explosionRadius, explosionRadius, explosionRadius);
    }

    protected override void ExtraTrigger(Collider2D other)
    {
        if (other.tag == "Enemy" && transform.localScale.x == explosionRadius && counter > 0)
        {
            //OnHit();
            other.GetComponent<EnemyBasics>().DealDamage(damage);
            counter--;
        }
    }

    void LateUpdate()
    {
        if (transform.localScale.x == explosionRadius && timer > 0) timer -= Time.deltaTime;
        else if (transform.localScale.x == explosionRadius) Destroy(gameObject);
    }
}
