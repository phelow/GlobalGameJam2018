using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D m_rigidbody;

    [SerializeField]
    private bool m_isSelected;

    public static LineRenderer globalLineRenderer;

    // Use this for initialization
    void Start () {
		if(globalLineRenderer == null)
        {
            globalLineRenderer = GameObject.Find("TargetingLine").GetComponent<LineRenderer>();
        }
	}

    // Update is called once per frame
    void Update()
    {
        if (m_isSelected)
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            globalLineRenderer.positionCount = 2;
            globalLineRenderer.SetPosition(0, this.transform.position);
            globalLineRenderer.SetPosition(1, new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, this.transform.position.z));
        }
	}


}
