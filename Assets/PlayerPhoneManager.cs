using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhoneManager : MonoBehaviour {

    public Receiver _heldPhone;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
