using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageSpawner : MonoBehaviour {
    [SerializeField]
    GameObject _message;

    Queue<string> _queuedMessages = new Queue<string>();

    public static MessageSpawner s_instance;
	// Use this for initialization
	void Awake () {
        s_instance = this;
        _queuedMessages.Enqueue("You have started a new dating site for monsters.");
    }

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        while (true)
        {
            while(_queuedMessages.Count == 0)
            {
                yield return new WaitForEndOfFrame();
            }

            Text text = GameObject.Instantiate(_message, this.transform.position, this.transform.rotation, this.transform).GetComponentInChildren<Text>();
                text.text = _queuedMessages.Dequeue();
            text.transform.parent.localPosition = new Vector3(0, -200, 0);
            yield return new WaitForSeconds(4.1f / MessageSpawner.s_instance.GetSpeed());
        }
    }

    public float GetSpeed()
    {
        return _queuedMessages.Count + 1;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnMessage(string message)
    {
        _queuedMessages.Enqueue(message);
    }
}
