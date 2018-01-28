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

    internal void EndCall()
    {
        this._holder.EndCall();
        this._holder = null;
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
