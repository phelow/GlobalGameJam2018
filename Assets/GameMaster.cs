using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {
    [SerializeField]
    private List<Monster> _monsters;


	// Use this for initialization
	void Start () {
        StartCoroutine(MatchMonsters());
	}
	
    private IEnumerator MatchMonsters()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            Monster monsterA = _monsters[Random.Range(0, _monsters.Count)];
            Monster monsterB = _monsters[Random.Range(0, _monsters.Count)];

            if(monsterA == monsterB)
            {
                continue;
            }

            if(monsterA.HasMatch() || monsterB.HasMatch())
            {
                continue;
            }

            monsterA.SetMatch(monsterB);
            monsterB.SetMatch(monsterA);
        }
    }
}
