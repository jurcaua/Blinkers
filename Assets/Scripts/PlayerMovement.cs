using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 10f;

    private Rigidbody m_Rigidbody;

    private Vector3 m_Movement;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * movementSpeed * Time.deltaTime);
    }
}
