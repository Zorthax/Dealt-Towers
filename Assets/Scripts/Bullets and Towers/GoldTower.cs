using UnityEngine;
using System.Collections;

public class GoldTower : MonoBehaviour {

    public int goldProduced;
    public float goldDelay;
    float delay;

	// Use this for initialization
	void Start ()
    {
        CardEssentials.goldPlaced = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (delay < goldDelay) delay += Time.deltaTime;
        else
        {
            delay = 0;
            ImportantStats.gold += goldProduced;
        }
	}
}
