using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Tutorializeable {

    private Receiver _phone;
    private List<Monster> _matches = new List<Monster>();
    private float _health = 1.0f;
    private const float c_timeModifier = 0.01f;

    // Update is called once per frame
    void Update () {
		if(_matches.Count >0 &&( _phone == null || _phone.GetMonsterOnOtherEnd() == null || !_matches.Contains(_phone.GetMonsterOnOtherEnd()) && PlayerPhoneManager.s_instance.HasUnusedPhones))
        {
            SubtractHealth(Time.deltaTime * c_timeModifier);
        }
	}

    private void SubtractHealth(float deltaTime)
    {
        _health -= deltaTime;
        if(_health < 0)
        {
            GameMaster.s_instance.EndGame();
        }

        SetUrgency(_health);
    }

    internal void ResetHealth()
    {
        _health = 1.0f;
        SetUrgency(_health);
    }

    internal bool GivePhone(Receiver phone)
    {
        if(_matches.Count == 0)
        {
            return false;
        }

        if (_phone == null && (phone.GetMonsterOnOtherEnd() == null|| (this._matches.Contains(phone.GetMonsterOnOtherEnd()))))
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

    internal void EndCall()
    {
        _phone.transform.SetParent(null);
        _phone = null;
    }

    internal IEnumerable<Monster> GetMatches()
    {
        return _matches;
    }

    internal bool HasPhone()
    {
        return _phone != null;
    }
}
