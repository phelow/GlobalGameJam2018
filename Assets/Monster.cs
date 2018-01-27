using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

    private Receiver _phone;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    internal bool GivePhone(Receiver phone)
    {
        if (_phone == null)
        {
            _phone = phone;
            _phone.transform.SetParent(this.transform);
            _phone.SetHolder(this);
            return true;
        }

        return false;
    }
}
