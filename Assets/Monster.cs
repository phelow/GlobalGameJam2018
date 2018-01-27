using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Tutorializeable {

    private Receiver _phone;
    private List<Monster> _matches;
    
	// Use this for initialization
	void Start () {
        _matches = new List<Monster>();

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

    internal bool HasMatch()
    {
        return _matches.Count > 0;
    }

    internal void AddMatch(Monster monsterB)
    {
        _matches.Add(monsterB);
    }

    internal IEnumerable<Monster> GetMatches()
    {
        return _matches;
    }
}
