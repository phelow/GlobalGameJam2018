using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(MoveMessage());
	}

    private IEnumerator MoveMessage()
    {
        for(int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(2.0f / MessageSpawner.s_instance.GetSpeed());
            transform.position = new Vector3(transform.position.x, transform.position.y - 100, transform.position.z);
        }

        Destroy(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
