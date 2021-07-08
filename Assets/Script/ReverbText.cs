using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverbText : MonoBehaviour
{
    public TextMesh text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = transform.localScale.x.ToString("f2");
    }
}
