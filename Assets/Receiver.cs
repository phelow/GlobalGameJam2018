using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receiver : Tutorializeable {
    private Monster _holder;
    [SerializeField]
    private Receiver _pairedReceiver;
    private const float c_callTime = 30.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    internal void SetHolder(Monster monster)
    {
        _holder = monster;

        if (_pairedReceiver.GetHolder() != null)
        {
            StartCoroutine(PlaceCall());
        }
    }

    private IEnumerator PlaceCall()
    {
        MessageSpawner.s_instance.SpawnMessage(this.GetMonsterOnOtherEnd().name + " is dating " + this._holder.name);
        GameMaster.s_instance.AddPoint();
        _pairedReceiver.GetMonster().ResetHealth();
        this.GetMonster().ResetHealth();
        yield return new WaitForSeconds(1.0f);

        float tPassed = 0.0f;
        while(tPassed <= 20.0f && GameMaster.s_instance.PlayerHasMoves())
        {
            tPassed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        _pairedReceiver.EndCall();
        this.EndCall();
    }

    private IEnumerator ThrowPhone()
    {
        Rigidbody2D rigidbody = this.gameObject.AddComponent<Rigidbody2D>();
        rigidbody.AddForce(10.0f * (this.transform.position - this._holder.transform.position));
        rigidbody.gravityScale = 0.0f;
        yield return new WaitForSeconds(4.0f);
        Destroy(rigidbody);
        try
        {
            MessageSpawner.s_instance.SpawnMessage(this.GetMonsterOnOtherEnd().name + " has broken up with " + this._holder.name);
        }
        catch
        {

        }
        if (this._holder != null)
        {
            this._holder.RemoveMatch(this.GetMonsterOnOtherEnd());
        }

        Monster temp = this.GetMonsterOnOtherEnd();
        if (temp != null)
        {
            temp.RemoveMatch(this._holder);
        }

        if (this._holder != null)
        {
            this._holder.EndCall();
        }
        this._holder = null;
    }

    internal void EndCall()
    {
        StartCoroutine(ThrowPhone());
        
    }

    internal Monster GetHolder()
    {
        return _holder;
    }

    internal bool Isheld()
    {
        return _holder != null;
    }

    internal bool HasMonsterOnOtherEnd()
    {
        return _pairedReceiver.Isheld();
    }

    internal Monster GetMonsterOnOtherEnd()
    {
        return _pairedReceiver.GetMonster();
    }

    internal Monster GetMonster()
    {
        return _holder;
    }
}
