using System.Collections;
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
    private const float c_movementConstant = 1000.0f;

    void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {
        this.StartCoroutine(this.MovePlayer());
    }
    
    private IEnumerator MovePlayer()
    {
        bool waving = false;
        while (true)
        {
            yield return new WaitForEndOfFrame();
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 movement = new Vector2(0, 0);

            if (Input.GetMouseButton(0))
            {
                movement = (new Vector2(worldPos.x, worldPos.y) - new Vector2(transform.position.x, transform.position.y));
            }

            if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.01f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.01f)
            {
                m_lastScreenPosition = Input.mousePosition;
                worldPos = new Vector3(Input.GetAxis("Horizontal") * 100.0f, Input.GetAxis("Vertical") * 100.0f, 0.0f);
                movement = (new Vector2(worldPos.x, worldPos.y) - new Vector2(transform.position.x, transform.position.y));
            }

            if (movement.magnitude > .03f)
            {
                m_rigidbody.AddForce(movement.normalized * c_movementConstant * Time.deltaTime);
            }

            //if (transform.position.x > worldPos.x + .1f)
            //{

            //    m_animator.SetBool("Left", true);
            //    m_animator.SetBool("Right", false);
            //    m_animator.SetBool("Wave", false);
            //    m_animator.SetBool("Down", false);

            //}
            //else if (transform.position.x < worldPos.x - .1f)
            //{

            //    m_animator.SetBool("Left", false);
            //    m_animator.SetBool("Right", true);
            //    m_animator.SetBool("Wave", false);
            //    m_animator.SetBool("Down", false);

            //}
            //else
            //{

            //    m_animator.SetBool("Left", false);
            //    m_animator.SetBool("Right", false);
            //    m_animator.SetBool("Wave", false);
            //    m_animator.SetBool("Down", true);
            //}

        }
    }


}