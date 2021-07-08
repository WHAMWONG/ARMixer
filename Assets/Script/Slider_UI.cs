using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider_UI : MonoBehaviour
{
    public TextMesh text;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetVolume(InteractionSlider slider)
    {
        text.text = slider.HorizontalSliderValue.ToString("f2");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
