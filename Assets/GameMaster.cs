using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    private List<Monster> _monsters;
    internal static GameMaster s_instance;
    private int _boundX;
    private int _boundY;

    [SerializeField]
    private GameObject _monster;
    [SerializeField]
    private GameObject _phone;
    private int _score = 0;
    [SerializeField]
    private Text _scoreText;


    private void Awake()
    {
        s_instance = this;
    }

    // Use this for initialization
    void Start()
    {
        _boundX = 5;
        _boundY = 5;

        StartCoroutine(MatchMonsters());
        StartCoroutine(SpawnMonsters());
    }

    internal List<Monster> GetAllMonsters()
    {
        return _monsters;
    }

    internal void EndGame()
    {
        PlayerPrefs.SetInt("HighScore", _score);
    }

    public void AddPoint()
    {
        _score++;
        _scoreText.text = "" + _score;
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
        _boundX++;
        _boundY++;
        int x;
        if (UnityEngine.Random.Range(0, 100) < 50)
        {
            int startingPoint = (int)Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
            x = UnityEngine.Random.Range(startingPoint - _boundX, startingPoint) - _boundX;
        }
        else
        {
            int startingPoint = (int)Camera.main.ScreenToWorldPoint(new Vector3(1, 1, 1)).x;
            x = UnityEngine.Random.Range(startingPoint, startingPoint + _boundX) + _boundX;
        }

        int y;
        if (UnityEngine.Random.Range(0, 100) < 50)
        {
            int startingPoint = (int)Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
            y = UnityEngine.Random.Range(startingPoint - _boundY, startingPoint) - _boundY;
        }
        else
        {
            int startingPoint = (int)Camera.main.ScreenToWorldPoint(new Vector3(1, 1, 1)).y;
            y = UnityEngine.Random.Range(startingPoint, startingPoint + _boundY) + _boundY;
        }

        Vector3 point = new Vector3(x, y, 1);

        Vector2 screenPos = Camera.main.WorldToScreenPoint(point);

        if (point.x > -.5f && point.x < 1.5 && point.y > -.5f && point.y < 1.5f)
        {
            return;
        }
        Phone phone = GameObject.Instantiate(_phone, point, new Quaternion(0, 0, 0, 0), null).GetComponent<Phone>();

        PlayerPhoneManager.s_instance.AddPhone(phone);
    }

    private void SpawnMonster()
    {
        _boundX++;
        _boundY++;
        int x;
        if (UnityEngine.Random.Range(0, 100) < 50)
        {
            int startingPoint = (int)Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
            x = UnityEngine.Random.Range(startingPoint - _boundX, startingPoint) - _boundX;
        }
        else
        {
            int startingPoint = (int)Camera.main.ScreenToWorldPoint(new Vector3(1, 1, 1)).x;
            x = UnityEngine.Random.Range(startingPoint, startingPoint + _boundX) + _boundX;
        }

        int y;
        if (UnityEngine.Random.Range(0, 100) < 50)
        {
            int startingPoint = (int)Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
            y = UnityEngine.Random.Range(startingPoint - _boundY, startingPoint) - _boundY;
        }
        else
        {
            int startingPoint = (int)Camera.main.ScreenToWorldPoint(new Vector3(1, 1, 1)).y;
            y = UnityEngine.Random.Range(startingPoint, startingPoint + _boundY) + _boundY;
        }

        Vector3 point = new Vector3(x, y, 1);
        Vector2 screenPos = Camera.main.WorldToScreenPoint(point);

        Monster monster = GameObject.Instantiate(_monster, point, new Quaternion(0, 0, 0, 0), null).GetComponent<Monster>();


        _monsters.Add(monster);
    }

    private IEnumerator MatchMonsters()
    {
        while (true)
        {
            Monster monsterA = _monsters[UnityEngine.Random.Range(0, _monsters.Count)];
            Monster monsterB = _monsters[UnityEngine.Random.Range(0, _monsters.Count)];

            if (monsterA == monsterB)
            {
                yield return new WaitForSeconds(10.0f);
                continue;
            }

            if (monsterA.HasMatch() || monsterB.HasMatch())
            {
                yield return new WaitForSeconds(10.0f);
                continue;
            }

            monsterA.AddMatch(monsterB);
            monsterB.AddMatch(monsterA);
            yield return new WaitForSeconds(10.0f);
        }
    }

    internal IEnumerable<Monster> GetAllMonstersWithMatchesWhoNeedPhones()
    {
        return _monsters.Where(monster => monster.HasMatch() && !monster.HasPhone());
    }
}
