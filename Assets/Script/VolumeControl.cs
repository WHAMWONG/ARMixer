using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 声音大小控制
/// </summary>
public class VolumeControl : MonoBehaviour
{
    public TextMesh text;
    /// <summary>
    /// 记录值
    /// </summary>
    public float value;

    // Start is called before the first frame update
    void Start()
    {
        text.text = Max.Initialize.audioSource.volume.ToString("f2");
        value = Max.Initialize.audioSource.volume;
    }
    public float y = 0;
    /// <summary>
    /// 控制移动比例
    /// </summary>
    public float scale;
    public GameObject recordHand;
    /// <summary>
    /// 开始记录
    /// </summary>
    public void StartRecord(GameObject data)
    {
        recordHand = data;
        y = data.transform.position.y;
    }
    /// <summary>
    /// 结束
    /// </summary>
    public void EntRecord()
    {
        recordHand = null;
    }
    // Update is called once per frame
    public GameObject qiu;
    void Update()
    {
        
       
        if (recordHand!=null)
        {
            Max.Initialize.audioSource.volume = value;
            //计算移动比例
            float f = recordHand.transform.position.y - y;
            value += f * scale;
            y = recordHand.transform.position.y;
            value = Mathf.Clamp(value, 0, 1);
         
        }

        Color color = qiu.GetComponent<MeshRenderer>().material.color;
        color.a = Max.Initialize.audioSource.volume;
        qiu.GetComponent<MeshRenderer>().material.color = color;
        text.text = Max.Initialize.audioSource.volume.ToString("f2");

    }
}
