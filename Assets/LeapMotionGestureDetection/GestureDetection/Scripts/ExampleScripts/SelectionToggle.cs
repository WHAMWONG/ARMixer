using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionToggle : MonoBehaviour
{

    ObjectActivation[] mActivationObjects;

    ObjectActivation m_ActivatedObject = null;

    void Start()
    {
        mActivationObjects = gameObject.GetComponentsInChildren<ObjectActivation>();

        if( m_ActivatedObject != null )
        {
            m_ActivatedObject.Activate();
        }
    }

    void Update()
    {
        foreach ( ObjectActivation obj in mActivationObjects)
        {
            if( obj.IsActivated() && ( obj != m_ActivatedObject) )
            {
                m_ActivatedObject = obj;
                DeactivateAllBut(m_ActivatedObject);
                break;
            }
        }
    }

    void DeactivateAllBut( ObjectActivation a_obj )
    {
        foreach ( ObjectActivation obj in mActivationObjects)
        {
            if ( ( obj != a_obj ) )
            {
                obj.ResetObject();
            }
        }
    }
}
