using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicInput : MonoBehaviour
{
    AudioSource audioSource;
    public string selectedDevice;
    
    // Start is called before the first frame update
    void Start()
    {
    selectedDevice = Microphone.devices[0].ToString();
    Debug.Log (selectedDevice);
    AudioSource audioSource = GetComponent<AudioSource>();
    audioSource.clip = Microphone.Start(selectedDevice, true, 1, AudioSettings.outputSampleRate);
    audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}