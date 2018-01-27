using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameMaster : MonoBehaviour {
    [SerializeField]
    private List<Monster> _monsters;
    internal static GameMaster s_instance;
    private int _boundX;
    private int _boundY;

    [SerializeField]
    private GameObject _monster;
    [SerializeField]
    private GameObject _phone;

    private void Awake()
    {
        s_instance = this;
    }

    // Use this for initialization
    void Start () {
        _boundX = 10;
        _boundY = 10;

        StartCoroutine(MatchMonsters());
        StartCoroutine(SpawnMonsters());
	}
	
    internal List<Monster> GetAllMonsters()
    {
        return _monsters;
    }

    private IEnumerator SpawnMonsters()
    {
        while (true)
        {
            yield return new WaitForSeconds(10.0f);
            SpawnMonster();
            yield return new WaitForSeconds(10.0f);
            SpawnMonster();
            yield return new WaitForSeconds(10.0f);
            SpawnMonster();
            yield return new WaitForSeconds(10.0f);
            SpawnPhone();
        }
    }

    private void SpawnPhone()
    {
        Phone phone = GameObject.Instantiate(_phone, new Vector3(UnityEngine.Random.Range(-_boundX, _boundX), UnityEngine.Random.Range(-_boundY, _boundY), 1), new Quaternion(0,0,0,0), null).GetComponent<Phone>();
        _boundX++;
        _boundY++;

        PlayerPhoneManager.s_instance.AddPhone(phone);
    }

    private void SpawnMonster()
    {
        Monster monster = GameObject.Instantiate(_monster, new Vector3(UnityEngine.Random.Range(-_boundX, _boundX), UnityEngine.Random.Range(-_boundY, _boundY), 1), new Quaternion(0, 0, 0, 0), null).GetComponent<Monster>();
        _boundX++;
        _boundY++;


        _monsters.Add(monster);
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
