﻿using UnityEngine;

public class Tutorializeable : MonoBehaviour
{
    [SerializeField]
    private LineRenderer _tutorialLineRenderer;

    internal void SetTutorialTarget(Vector3 position)
    {
        _tutorialLineRenderer.positionCount = 2;
        _tutorialLineRenderer.SetPosition(0, Vector3.Lerp(this.transform.position, position, .2f));
        _tutorialLineRenderer.SetPosition(1, position);
        _tutorialLineRenderer.startWidth = 0f;
        _tutorialLineRenderer.endWidth = Mathf.Lerp(1, .2f, Vector3.Distance(this.transform.position, position) / 100.0f);
    }

    internal void SetUrgency(float urgency)
    {
        Color color = Color.Lerp(Color.Lerp(Color.red, Color.green, urgency), Color.clear, 0.5f);
        _tutorialLineRenderer.material.color = color;
    }

    internal void ClearTutorialTarget()
    {
        _tutorialLineRenderer.positionCount = 0;
    }
}