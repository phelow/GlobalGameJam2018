﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhoneManager : MonoBehaviour
{
    public static PlayerPhoneManager s_instance;

    private Receiver _heldPhone;

    [SerializeField]
    private List<Receiver> _recievers;
    internal bool HasUnusedPhones { get; private set; }


    // Use this for initialization
    void Start()
    {
        s_instance = this;
    }

    internal void AddPhone(Phone phone)
    {
        _recievers.Add(phone.recieverA);
        _recievers.Add(phone.recieverB);
    }

    // Update is called once per frame
    void Update()
    {
        this.HasUnusedPhones = false;
        foreach (Receiver reciever in _recievers)
        {
            if (!reciever.HasMonsterOnOtherEnd())
            {
                this.HasUnusedPhones = true;
            }
            reciever.ClearTutorialTarget();
        }

        foreach (Monster monster in GameMaster.s_instance.GetAllMonsters())
        {
            monster.ClearTutorialTarget();
        }

        if (_heldPhone == null)
        {

            float heldUrgency = 1.0f;
            foreach (Monster monster in GameMaster.s_instance.GetAllMonsters())
            {
                if (monster.CanTakeAction())
                {
                    heldUrgency = Mathf.Min(heldUrgency, monster.GetHealth());
                }
            }

            foreach (Receiver reciever in _recievers)
            {

                if (reciever.Isheld()|| _heldPhone == reciever.PairedReciever() || !HasPotentialMatch(reciever))
                {
                    continue;
                }

                reciever.SetTutorialTarget(this.transform.position);

                if (reciever.HasMonsterOnOtherEnd())
                {
                    reciever.SetUrgency(reciever.GetMonsterOnOtherEnd().GetHealth());
                }
                else
                {
                    reciever.SetUrgency(heldUrgency);
                }
            }
        }
        else
        {
            foreach (Receiver reciever in _recievers)
            {
                reciever.ClearTutorialTarget();
            }

            if (_heldPhone.HasMonsterOnOtherEnd())
            {
                foreach (Monster monster in _heldPhone.GetMonsterOnOtherEnd().GetMatches())
                {
                    if (monster.HasPhone())
                    {
                        continue;
                    }

                    monster.SetTutorialTarget(this.transform.position);
                }
            }
            else
            {
                foreach (Monster monster in GameMaster.s_instance.GetAllMonstersWithMatchesWhoNeedPhones())
                    monster.SetTutorialTarget(this.transform.position);
            }
        }
    }

    internal bool HasUnusedPhonePairs()
    {
        foreach (Receiver r in _recievers)
        {
            if (!r.Isheld() && r.GetMonsterOnOtherEnd() == null)
            {
                return true;
            }
        }
        return false;
    }

    private void CollideWithGameObject(GameObject go)
    {
        Monster monster = go.GetComponent<Monster>();
        Receiver handheld = go.GetComponent<Receiver>();
        if (_heldPhone == null)
        {

            if (handheld != null && handheld.Isheld() == false)
            {
                //If all of the matches of the monster on the other line are holding phones do not let the player pick up the phone o


                if (!HasPotentialMatch(handheld))
                {
                    return;
                }


                go.gameObject.transform.SetParent(this.transform);
                _heldPhone = handheld;
            }
        }
        else if (monster != null && _heldPhone != null && !monster.HasPhone())
        {
            if (monster.GivePhone(_heldPhone))
            {
                _heldPhone = null;
            }
        }
    }

    private bool HasPotentialMatch(Receiver handheld)
    {
        bool hasPotentialMatch = false;

        if (handheld.HasMonsterOnOtherEnd())
        {
            foreach (Monster match in handheld.GetMonsterOnOtherEnd().GetMatches())
            {
                if (!match.HasPhone())
                {
                    hasPotentialMatch = true;
                }
            }
        }
        else
        {
            hasPotentialMatch = true;
        }

        return hasPotentialMatch;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.CollideWithGameObject(collision.gameObject);
    }

    public bool HasPhone()
    {
        return _heldPhone != null && _heldPhone.HasMonsterOnOtherEnd() == false ;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        this.CollideWithGameObject(other.gameObject);
    }
}
