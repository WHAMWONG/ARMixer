using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// EQ选择
/// </summary>
public class EQControl : MonoBehaviour
{
    public TextMesh text;
    public int id = 0;
    public TextMesh[] texts_value;//值文本
    public TextMesh[] texts_describe;//描述
    public Color color_start;
    public Color color_ent;
    public InteractionSlider[] sliders;//滑动条
    /// <summary>
    /// 记录值
    /// </summary>
    public float value;

    // Start is called before the first frame update
    void Start()
    {
        text = texts_value[id];
        text.text = sliders[id].HorizontalSliderValue.ToString("f2");
        value = sliders[id].HorizontalSliderValue;
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

        //初始化数据
        text = texts_value[id];
        text.text = sliders[id].HorizontalSliderValue.ToString("f2");
        value = sliders[id].HorizontalSliderValue;
    }

    public void QieHuan()
    {
        id++;
        if (id>2)
        {
            id = 0;
        }
        EntRecord();
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
        if (recordHand != null)
        {
            //计算移动比例
            float f = recordHand.transform.position.y - y;
            value += f * scale;
            y = recordHand.transform.position.y;
            //设置值
            sliders[id].HorizontalSliderValue = value;
            if (id == 0)
            {
                Max.Initialize.audioMixer.SetFloat("80", value);
            }
            if (id == 1)
            {
                Max.Initialize.audioMixer.SetFloat("25", value);
            }
            if (id == 2)
            {
                Max.Initialize.audioMixer.SetFloat("12", value);
            }
            print(sliders[id].HorizontalSliderValue);
        }
        text.text = value.ToString("f2");
        //值的取值范围检测
        value = Mathf.Clamp(value, 0.05f, 3f);
        foreach (var item in texts_describe)
        {
            item.color = color_start;
        }
        texts_describe[id].color = color_ent;
    }

}
