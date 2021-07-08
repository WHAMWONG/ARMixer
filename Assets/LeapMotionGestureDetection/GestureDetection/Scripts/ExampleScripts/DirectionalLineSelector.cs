using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DirectionalLineSelector : MonoBehaviour
{
    LineRenderer m_Renderer;

    public Material m_Material;
    public float m_LineWidth;

    public float m_Distance;

    public bool m_bOnlyOn;

    bool m_bActive = false;

    void Start()
    {
        m_Renderer = GetComponent<LineRenderer>();

        m_Renderer.material = m_Material;
        m_Renderer.startWidth = m_LineWidth;
        m_Renderer.endWidth = m_LineWidth;
    }

    public void StartGesture()
    {
        m_bActive = true;
    }

    public void EndGesture()
    {
        m_bActive = false;
    }

    void Update()
    {
        if (!DetectionManager.Get().IsHandSet(EHand.eRightHand))
        {
            m_Renderer.SetPosition(0, new Vector3(0, 0, 0));
            m_Renderer.SetPosition(1, new Vector3(0, 0, 0));

            return;
        }

        if (m_bActive || m_bOnlyOn)
        {
            DetectionManager.DetectionFinger finger = DetectionManager.Get().GetHand(EHand.eRightHand).GetFinger(EFinger.eIndex);
            Vector3 startPos = finger.GetTipPosition();
            Vector3 direction = finger.GetFingerDirection();
            Vector3 endPos = startPos + direction * m_Distance;

            m_Renderer.SetPosition(0, startPos);
            m_Renderer.SetPosition(1, endPos);
        }
        else
        {
            m_Renderer.SetPosition(0, new Vector3(0, 0, 0));
            m_Renderer.SetPosition(1, new Vector3(0, 0, 0));
        }
    }
}
