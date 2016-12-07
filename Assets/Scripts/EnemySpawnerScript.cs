using UnityEngine;
using System.Collections;

public class EnemySpawnerScript : MonoBehaviour {

    public Wave[] waves;
    public int index = 0;

	// Use this for initialization
	void Start ()
    {
	    foreach (Wave w in waves)
        {
            w.SetPosition(transform.position);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (index < waves.Length)
        {
            waves[index].Update(); //Update wave
            if (waves[index].waveLength <= 0) index++; //Move onto next wave at end
        }
	}

    [System.Serializable]
    public class Wave
    {
        public float waveLength;
        public Burst[] enemyGroups;
        Vector3 position;
        

        [System.Serializable]
        public class Burst
        {
            public GameObject enemyType;
            public float timeUntilStart;
            public int spawnCount;
            public float timeBetweenEnemies;
            float timer = 0;

            public float getTimer() { return timer; }
            public void setTimer(float t) { timer = t; }

        }

        public void Update()
        {
            waveLength -= ImportantStats.deltaTime;
            foreach(Burst e in enemyGroups) //All groups at the same time
            if (e.spawnCount > 0) //If no enemies are left, skip all code
            {
                if (e.timeUntilStart > 0) e.timeUntilStart -= ImportantStats.deltaTime; //Delay before starting time (once per burst)
                else if (e.getTimer() < e.timeBetweenEnemies) e.setTimer(e.getTimer() + ImportantStats.deltaTime); //Delay between enemies
                else                                                            //Spawn enemy
                {
                    e.setTimer(0);
                    Instantiate(e.enemyType, position, new Quaternion(0, 0, 0, 1));
                    e.spawnCount--;
                }
            }
        }

        public void SetPosition(Vector3 pos)
        {
            position = pos;
        }
    }
}
