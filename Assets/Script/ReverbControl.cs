using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 混响的值
/// </summary>
public class ReverbControl : MonoBehaviour
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
        value = Max.Initialize.audioSource.reverbZoneMix;
    }
    //public float y = 0;
    /// <summary>
    /// 控制移动比例
    /// </summary>
   // public float scale;
    public GameObject recordHand;
    /// <summary>
    /// 开始记录
    /// </summary>
    public void StartRecord(GameObject data)
    {
        recordHand = data;
      
    }
    /// <summary>
    /// 结束
    /// </summary>
    public void EntRecord()
    {
        recordHand = null;
    }
    // Update is called once per frame
    void Update()
    {
        text.text = value.ToString("f2");
       
        if (recordHand != null)
        {
            //计算移动比例
            value = recordHand.transform.localScale.y;
            Max.Initialize.audioSource.reverbZoneMix = value;
            if (recordHand.transform.localScale.x <= 0.1f)
            {
                recordHand.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            }
            else if (recordHand.transform.localScale.y >= 1.2f)
            {
                recordHand.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            }
        }


        value = Mathf.Clamp(value, 0f, 1.1f);


    }
}
