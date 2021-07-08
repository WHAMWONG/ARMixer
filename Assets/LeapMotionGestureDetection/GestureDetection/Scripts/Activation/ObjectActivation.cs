using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ObjectActivation : MonoBehaviour
{
    public bool m_UseObjectsColorAsStartColor;
    public Color m_StartColor;
    public Color m_EndColor;
    public Color m_ActivationColor;

    MeshRenderer m_Renderer;

    float m_CurrentActivationTime = 0.0f;

    public float m_ActivationTime;

    bool m_Activated = false;

    public bool m_ActivatedObject = false;

    public UnityEvent m_ObjectActivated;
    public UnityEvent m_ObjectDeactivated;

    void Start()
    {
        m_Renderer = GetComponent<MeshRenderer>();

        if ( m_UseObjectsColorAsStartColor )
        {
            m_StartColor = m_Renderer.material.color;
        }
    }

    void Update()
    {
        if (m_Activated)
        {
            m_CurrentActivationTime += Time.deltaTime;

            if( m_CurrentActivationTime > m_ActivationTime )
            {
                m_CurrentActivationTime = m_ActivationTime;
                if(!m_ActivatedObject)
                {
                    m_ObjectActivated.Invoke();
                    m_ActivatedObject = true;
                }
            }
        }
        else
        {
            m_CurrentActivationTime -= Time.deltaTime;

            if (m_CurrentActivationTime < 0.0f)
            {
                m_CurrentActivationTime = 0.0f;
            }
        }

        m_Renderer.material.color = GetCurrentColor();
    }

    Color GetCurrentColor()
    {
        float interp = m_CurrentActivationTime / m_ActivationTime;

        Color interpColor = Color.Lerp(m_StartColor, m_EndColor, interp);

        return m_CurrentActivationTime == m_ActivationTime || m_ActivatedObject ? m_ActivationColor : interpColor;
    }

    public bool IsActivated()
    {
        return m_ActivatedObject;
    }

    public void Activate()
    {
        m_ActivatedObject = true;
        m_ObjectDeactivated.Invoke();
    }

    public void StopActivating()
    {
        m_Activated = false;
    }

    public void StartActivating()
    {
        m_Activated = true;
    }

    public void ResetObject()
    {
        m_ActivatedObject = false;
        m_ObjectDeactivated.Invoke();
    }
}
