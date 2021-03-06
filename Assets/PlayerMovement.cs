﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Animator m_animator;
    [SerializeField]
    private Rigidbody2D m_rigidbody;
    private Vector2 m_lastScreenPosition;
    private const float c_movementConstant = 500.0f;


    [SerializeField]
    private List<AudioClip> _audioclips;

    [SerializeField]
    private AudioSource _audioclip;

    [SerializeField]
    private ParticleSystem _emitter;

    void Awake()
    {
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            _emitter.Emit(100);
            _audioclip.PlayOneShot(_audioclips[Random.Range(0, _audioclips.Count)]);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_rigidbody.AddForce((new Vector2(worldPos.x, worldPos.y) - new Vector2(transform.position.x, transform.position.y)).normalized * c_movementConstant);
        }
    }


}