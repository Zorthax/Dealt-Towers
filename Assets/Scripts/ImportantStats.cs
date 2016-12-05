﻿using UnityEngine;
using System.Collections;
using Text = UnityEngine.UI.Text;

public class ImportantStats : MonoBehaviour {

    public static int gold;
    public int startingGold;
    public static int enemyCount;
    static Text goldText;

	// Use this for initialization
	void Start ()
    {
        gold = startingGold;
        goldText = GameObject.FindGameObjectWithTag("Gold Text").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (goldText != null) goldText.text = gold.ToString();
	}

    void OnGui()
    {

    }

}
