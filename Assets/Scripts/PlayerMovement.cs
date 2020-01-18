using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 10f;
    public float jumpForce = 5f;
    public float reorientSpeed = 20;

    private Rigidbody m_Rigidbody;

    private Vector3 m_Movement;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        HandleJump();
    }

    // Here we will always be Slerping towards being in a straight orientation, so rotation components (in euler) x and z moving towards 0
    void Reorient()
    {
        Debug.Log("Reorienting!");
        m_Rigidbody.MoveRotation(
            Quaternion.Slerp(
                m_Rigidbody.rotation, 
                Quaternion.Euler(0f, m_Rigidbody.rotation.eulerAngles.y, 0f),
                reorientSpeed * Time.fixedDeltaTime));
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_Rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }
    Vector3 CleanForwardVector(Vector3 forwardVector)
    {
        forwardVector.y = 0;
        return forwardVector.normalized;
    }

    Vector3 CalculateRightVector(Vector3 forwardVector)
    {
        return -Vector3.Cross(forwardVector, transform.up).normalized;
    }

    public void MoveRelativeToCamera(Transform cameraTranform)
    {
        Vector3 relativeForward = CleanForwardVector(cameraTranform.forward);
        Vector3 relativeRight = CalculateRightVector(relativeForward);

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool isMoving = !Mathf.Approximately(Mathf.Abs(horizontal), 0f) || !Mathf.Approximately(Mathf.Abs(vertical), 0f);

        m_Movement = relativeForward * vertical + relativeRight * horizontal;
        m_Movement.Normalize();

        if (isMoving)
        {
            m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * movementSpeed * Time.fixedDeltaTime);
        }
    }

    public void LookAtTransform(Transform toLookAt)
    {
        m_Rigidbody.MoveRotation(Quaternion.LookRotation(toLookAt.position - transform.position, transform.up));
        Reorient();
    }
}
