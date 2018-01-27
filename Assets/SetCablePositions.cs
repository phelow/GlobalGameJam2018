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
		
	}
	
	// Update is called once per frame
	void Update () {
        m_cableRenderer.positionCount = 2;
        m_cableRenderer.SetPosition(0, m_cableA.transform.position);
        m_cableRenderer.SetPosition(1, m_cableB.transform.position);
    }
}
