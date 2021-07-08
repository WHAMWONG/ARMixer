using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeObject : MonoBehaviour
{
    public GameObject m_object;
    public float m_Scalar;

    float m_fLastDist;

    bool m_bResizing = false;

    void Start()
    {

    }

    void Update()
    {
        if (m_bResizing && DetectionManager.Get().IsBothHandsSet())
        {
            float fNewDistance = GetDistance();

            float fChange = fNewDistance - m_fLastDist;
            
            m_object.transform.localScale += new Vector3(fChange, fChange, fChange) * m_Scalar;
            
            if(m_object.transform.localScale.x < 0)
            {
                m_object.transform.localScale = new Vector3(0, 0, 0);
            }
            if (m_object.transform.localScale.x > 1.1f)
            {
                m_object.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            }

            m_fLastDist = fNewDistance;
        }
    }

    float GetDistance()
    {
        Vector3 pos1 = DetectionManager.Get().GetHand(EHand.eLeftHand).GetFinger(EFinger.eIndex).GetTipPosition();
        Vector3 pos2 = DetectionManager.Get().GetHand(EHand.eRightHand).GetFinger(EFinger.eIndex).GetTipPosition();
        return Vector3.Distance(pos1, pos2);
    }

    public void StartResize()
    {
        m_fLastDist = GetDistance();
        m_bResizing = true;
    }

    public void EndResize()
    {
        m_bResizing = false;
    }

}
