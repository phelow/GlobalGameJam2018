﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    [SerializeField]
    private Text m_text;
	// Use this for initialization
	void Start () {
        m_text.text = "Your high score is: " + PlayerPrefs.GetInt("HighScore", 0);

    }
	
	// Update is called once per frame
	void Update () {
	}
}
