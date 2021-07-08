using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightColorSetter : MonoBehaviour
{

    public Light m_Light;
    public Color m_Color;
    
    public void SetLightToColor()
    {
        if( m_Light )
        {
            m_Light.color = m_Color;
        }
    }

}
