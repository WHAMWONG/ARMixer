using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SelectableObject : GestureBase
{
    bool m_bDetected = false;

    public void StartActivation()
    {
        m_bDetected = true;
    }

    public void EndActivation()
    {
        m_bDetected = false;
    }

    public override bool Detected()
    {
        return m_bDetected;
    }
}

