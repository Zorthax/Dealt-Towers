using UnityEngine;
using System.Collections;

public class PausePlay : MonoBehaviour {

    UnityEngine.UI.Image img;
    public Sprite play;
    public Sprite pause;

	// Use this for initialization
	void Start ()
    {
        img = GetComponent<UnityEngine.UI.Image>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (ImportantStats.speed == 0) img.sprite = play;
        else img.sprite = pause;
	}
}
