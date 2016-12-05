using UnityEngine;
using System.Collections;

public class TowerRadius : MonoBehaviour {

    TowerEssentials parentScript;

	// Use this for initialization
	void Start ()
    {
        parentScript = GetComponentInParent<TowerEssentials>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            parentScript.AddEnemy(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            parentScript.RemoveEnemy(other.gameObject);
        }
    }
}
