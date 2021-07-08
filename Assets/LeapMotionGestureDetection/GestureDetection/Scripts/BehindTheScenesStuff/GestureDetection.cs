using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GestureDetection : MonoBehaviour
{
    [Space(5)]
    GestureBase[] m_Gestures;

    [Space(5)]
    public UnityEvent OnGestureStart;
    public UnityEvent OnGestureEnd;
    public UnityEvent OnGestureStay;

    bool m_bDetected = false;

    void Awake()
    {
        m_Gestures = gameObject.GetComponents<GestureBase>();
    }

    void Start()
    {

    }

    void Update()
    {
        int NumTrue = 0;

        foreach (GestureBase gesture in m_Gestures)
        {
            if (gesture.IsDetected())
            {
                NumTrue++;
            }
        }

        if (NumTrue >= m_Gestures.Length)
        {
            if (!m_bDetected)
            {
                OnGestureStart.Invoke();
                m_bDetected = true;
            }

            OnGestureStay.Invoke();
        }
        else
        {
            if (m_bDetected)
            {
                OnGestureEnd.Invoke();
                m_bDetected = false;
            }
        }
    }
}
