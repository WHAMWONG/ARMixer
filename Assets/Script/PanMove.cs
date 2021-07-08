using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanMove : MonoBehaviour
{
    public float gap = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(gap*Max.Initialize.audioSource.panStereo, transform.localPosition.y, transform.localPosition.z);
    }
}
