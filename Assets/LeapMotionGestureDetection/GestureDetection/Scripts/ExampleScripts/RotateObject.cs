using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public GameObject m_Object;

    public float m_RotationSpeed;

    Vector3 m_CurrentTorque;

    public float m_SlowDownSpeed = 1f;


    void Start()
    {

    }

    void Update()
    {
        Vector3 rot = m_CurrentTorque * m_RotationSpeed;

        m_Object.transform.Rotate(m_Object.transform.worldToLocalMatrix * rot);

        m_CurrentTorque = Vector3.Lerp(m_CurrentTorque, Vector3.zero, m_SlowDownSpeed * Time.deltaTime);
    }

    public void AddXTorque(float a_torque)
    {
        m_CurrentTorque.x += a_torque;
    }

    public void AddYTorque(float a_torque)
    {
        m_CurrentTorque.y += a_torque;
    }

    public void AddZTorque(float a_torque)
    {
        m_CurrentTorque.z += a_torque;
    }

    public void StopRotation()
    {
        m_CurrentTorque = Vector3.zero;
    }
}
