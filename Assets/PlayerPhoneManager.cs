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
            foreach (Receiver reciever in _recievers)
            {
                if (reciever.Isheld())
                {
                    continue;
                }

                reciever.SetTutorialTarget(this.transform.position);
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

    private void CollideWithGameObject(GameObject go)
    {
        Monster monster = go.GetComponent<Monster>();
        Receiver handheld = go.GetComponent<Receiver>();
        if (_heldPhone == null)
        {
            if (handheld != null && handheld.Isheld() == false)
            {
                go.gameObject.transform.SetParent(this.transform);
                _heldPhone = handheld;
            }
        }
        else if (monster != null && _heldPhone != null)
        {
            if (monster.GivePhone(_heldPhone))
            {
                _heldPhone = null;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.CollideWithGameObject(collision.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        this.CollideWithGameObject(other.gameObject);
    }
}
