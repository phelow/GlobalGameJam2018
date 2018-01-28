using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCablePositions : MonoBehaviour {
    [SerializeField]
    private GameObject m_cableA;
    [SerializeField]
    private GameObject m_cableB;

    [SerializeField]
    private LineRenderer m_cableRenderer;

    // Use this for initialization
    void Start () {
        transform.parent = null;
        m_cableRenderer = this.gameObject.AddComponent<LineRenderer>();
        m_cableRenderer.widthMultiplier = .1f;
    }
	
	// Update is called once per frame
	void Update () {
        if(m_cableRenderer == null || m_cableA == null || m_cableB == null)
        {
            return;
        }

        m_cableRenderer.positionCount = 2;
        m_cableRenderer.SetPosition(0, m_cableA.transform.position);
        m_cableRenderer.SetPosition(1, m_cableB.transform.position);
    }
}
