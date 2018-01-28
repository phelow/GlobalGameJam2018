using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Monster : Tutorializeable {
    public string name;
    private Receiver _phone;
    private List<Monster> _matches = new List<Monster>();
    private float _health = 1.0f;
    private const float c_timeModifier = 0.01f;
    [SerializeField]
    private Rigidbody2D _rigidbody;

    [SerializeField]
    private List<Sprite> _images;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Text _monsterText;

    public bool HasAvailableMatches { get; set; }
    private static string RandomString(int length)
    {
        const string pool = "abcdefghijklmnopqrstuvwxyz";
        var builder = new StringBuilder();

        for (var i = 0; i < length; i++)
        {
            var c = pool[UnityEngine.Random.Range(0, pool.Length)];
            builder.Append(c);
        }

        return builder.ToString();
    }

    private void Start()
    {
        name = RandomString(UnityEngine.Random.Range(2, 10));
        _monsterText.text = name;
        MessageSpawner.s_instance.SpawnMessage(name + " has joined your dating site.");
        _spriteRenderer.sprite = _images[UnityEngine.Random.RandomRange(0,_images.Count)];
        StartCoroutine(DelayedCouroutineFreezing());
    }

    private IEnumerator DelayedCouroutineFreezing()
    {
        yield return new WaitForSeconds(1.0f);

        _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    // Update is called once per frame
    void Update () {
		if(CanTakeAction())
        {
            SubtractHealth(Time.deltaTime * c_timeModifier);
        }
	}

    internal void RemoveMatch(Monster monster)
    {
        this._matches.Remove(monster);
    } 

    internal bool CanTakeAction()
    {
        foreach(Monster match in _matches)
        {
            if (!match.HasAvailableMatches)
            {
                continue;
            }
            if(!match.HasPhone() && PlayerPhoneManager.s_instance.HasUnusedPhones)
            {
                return true;
            }
            if (match.HasPhone() && (!match.GetPhone().HasMonsterOnOtherEnd()))
            {
                return true;
            }
        }

        return false;
    }

    private Receiver GetPhone()
    {
        return this._phone;
    }
    bool patient = true;

    private void SubtractHealth(float deltaTime)
    {
        _health -= deltaTime;
        if(_health < .5f && patient)
        {
            patient = false;
            MessageSpawner.s_instance.SpawnMessage(this.name + " is getting impatient.");
        }

        if(_health < 0)
        {
            GameMaster.s_instance.EndGame();
        }

        SetUrgency(_health);
    }

    internal float GetHealth()
    {
        return _health;
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
