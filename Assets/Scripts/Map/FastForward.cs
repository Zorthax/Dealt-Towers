using UnityEngine;
using System.Collections;

public class FastForward : MonoBehaviour {

    public static float fastSpeed = 4.0f;
    UnityEngine.UI.Image img;
    public Sprite active;
    public Sprite inactive;

    // Use this for initialization
    void Start ()
    {
        img = GetComponent<UnityEngine.UI.Image>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (ImportantStats.speed == fastSpeed) img.sprite = active;
        else img.sprite = inactive;
	}
}
