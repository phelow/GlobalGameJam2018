using UnityEngine;

public class Tutorializeable : MonoBehaviour
{
    [SerializeField]
    private LineRenderer _tutorialLineRenderer;

    internal void SetTutorialTarget(Vector3 position)
    {
        _tutorialLineRenderer.positionCount = 2;
        _tutorialLineRenderer.SetPosition(0, this.transform.position);
        _tutorialLineRenderer.SetPosition(1, position);
    }

    protected void SetUrgency(float urgency)
    {
        Color color = Color.Lerp(Color.red, Color.green, urgency);
        _tutorialLineRenderer.material.color = color;
    }

    internal void ClearTutorialTarget()
    {
        _tutorialLineRenderer.positionCount = 0;
    }
}