using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Max : MonoBehaviour
{
    public static Max Initialize;
    /// <summary>
    /// 声音播放
    /// </summary>
    public AudioSource audioSource;
    /// <summary>
    /// 混淆器
    /// </summary>
    public AudioMixer audioMixer;
    /// <summary>
    /// 左手球
    /// </summary>
    public GameObject l;
    /// <summary>
    /// 右手球
    /// </summary>
    public GameObject r;
    void Awake()
    {
        Initialize = this;
    }
    /// <summary>
    /// 设置音量
    /// </summary>
    /// <param name="slider"></param>
    public void SetVolume(InteractionSlider slider)
    {
        audioSource.volume = slider.HorizontalSliderValue;
    }
    public void SetPan(float data)
    {
        audioSource.panStereo = data;
    }

    public void Set12(InteractionSlider slider)
    {
        audioMixer.SetFloat("12", slider.HorizontalSliderValue);
    }
    public void Set25(InteractionSlider slider)
    {
        audioMixer.SetFloat("25", slider.HorizontalSliderValue);
    }
    public void Set80(InteractionSlider slider)
    {
        audioMixer.SetFloat("80", slider.HorizontalSliderValue);
        print("80");
    }
    public GameObject square;
    public void Reverb()
    {
        audioSource.reverbZoneMix = square.transform.localScale.x;
    }

    public bool isReverb=false;
    public void SetReverb(bool data)
    {
        isReverb = data;
    }
    void Update()
    {
        //if (isReverb)
        //{
        //    Reverb();
        //}
        //else
        //{
        //    l.SetActive(false);
        //    r.SetActive(false);
        //}
        
       // audioMixer.SetFloat("3", 2);
        //audioMixer.SetFloat("80", 2);
        //audioMixer.SetFloat("25", 2);
    }
}
