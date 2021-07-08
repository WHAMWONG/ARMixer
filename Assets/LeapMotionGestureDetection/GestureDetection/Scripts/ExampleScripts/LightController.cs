using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightController : MonoBehaviour
{
    Light m_light;

    void Start()
    {
        m_light = GetComponent<Light>();
        m_light.enabled = false;
    }

    void Update()
    {

    }

    public void SetLightState(bool a_bOn)
    {
        m_light.enabled = a_bOn;
    }

    public void ToggleLightState()
    {
        m_light.enabled = !m_light.enabled;
    }

    public void SetColor(Color a_Color)
    {
        m_light.color = a_Color;
    }
}
