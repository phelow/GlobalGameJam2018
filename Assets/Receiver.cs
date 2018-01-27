using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receiver : MonoBehaviour {
    private Monster _holder;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    internal void SetHolder(Monster monster)
    {
        _holder = monster;
    }

    internal bool Isheld()
    {
        return _holder != null;
    }
}
