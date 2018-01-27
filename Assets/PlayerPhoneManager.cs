using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhoneManager : MonoBehaviour {

    public Receiver heldPhone;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Receiver handheld = other.gameObject.GetComponent<Receiver>();
        if (heldPhone == null)
        {
            if (handheld != null)
            {
                other.gameObject.transform.SetParent(this.transform);
                heldPhone = handheld;
            }
        }
    }
}
