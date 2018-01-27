using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameMaster : MonoBehaviour {
    [SerializeField]
    private List<Monster> _monsters;
    internal static GameMaster s_instance;

    private void Awake()
    {
        s_instance = this;
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(MatchMonsters());
	}
	
    internal List<Monster> GetAllMonsters()
    {
        return _monsters;
    }

    private IEnumerator MatchMonsters()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            Monster monsterA = _monsters[UnityEngine.Random.Range(0, _monsters.Count)];
            Monster monsterB = _monsters[UnityEngine.Random.Range(0, _monsters.Count)];

            if(monsterA == monsterB)
            {
                continue;
            }

            if(monsterA.HasMatch() || monsterB.HasMatch())
            {
                continue;
            }

            monsterA.AddMatch(monsterB);
            monsterB.AddMatch(monsterA);
        }
    }

    internal IEnumerable<Monster> GetAllMonstersWithMatches()
    {
        return _monsters.Where(monster => monster.HasMatch());
    }
}
