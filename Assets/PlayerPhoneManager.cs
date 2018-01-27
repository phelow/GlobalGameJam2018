using System.Collections.Generic;
using UnityEngine;

public class PlayerPhoneManager : MonoBehaviour
{

    private Receiver _heldPhone;

    [SerializeField]
    private List<Receiver> _recievers;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (Receiver reciever in _recievers)
        {
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
                foreach (Monster monster in GameMaster.s_instance.GetAllMonstersWithMatches())
                    monster.SetTutorialTarget(this.transform.position);
            }
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        Monster monster = other.gameObject.GetComponent<Monster>();
        Receiver handheld = other.gameObject.GetComponent<Receiver>();
        if (_heldPhone == null)
        {
            if (handheld != null && handheld.Isheld() == false)
            {
                other.gameObject.transform.SetParent(this.transform);
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
}
