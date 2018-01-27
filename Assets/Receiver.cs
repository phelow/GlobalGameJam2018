using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receiver : Tutorializeable {
    private Monster _holder;
    [SerializeField]
    private Receiver _pairedReceiver;

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

    internal bool HasMonsterOnOtherEnd()
    {
        return _pairedReceiver.Isheld();
    }

    internal Monster GetMonsterOnOtherEnd()
    {
        return _pairedReceiver.GetMonster();
    }

    internal Monster GetMonster()
    {
        return _holder;
    }
}
