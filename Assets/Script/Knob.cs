using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knob : MonoBehaviour
{
    /// <summary>
    /// 最大值
    /// </summary>
    public float maxValue;
    /// <summary>
    /// 最小值
    /// </summary>
    public float minValue;
    /// <summary>
    /// 当前值
    /// </summary>
    public float value;
    public TextMesh text;
    void Start()
    {
        fromVector = transform.up;
    }

    Vector3 fromVector;
    void Update()
    {
        //value = Mathf.Clamp(transform.localEulerAngles.z / 90, -1, 1) ;
        Vector3 toVector = transform.up;
        float angle = Vector3.Angle(fromVector, toVector); //求出两向量之间的夹角 
        Vector3 normal = Vector3.Cross(fromVector, toVector);//叉乘求出法线向量 
        angle *= Mathf.Sign(Vector3.Dot(normal, transform.forward))*-1;  //求法线向量与物体上方向向量点乘，结果为1或-1，修正旋转方向 
        
        value = Mathf.Clamp(angle / 90, -1, 1);
        text.text = value.ToString("f2");
        Max.Initialize.SetPan(value);
    }
}
