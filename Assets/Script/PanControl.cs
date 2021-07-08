using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// pan控制器
/// </summary>
public class PanControl : MonoBehaviour
{
    public static PanControl initialize;
    private void Awake()
    {
        initialize = this;
    }
    public TextMesh text;
    /// <summary>
    /// 记录值
    /// </summary>
    public float value;

    // Start is called before the first frame update
    void Start()
    {
        text.text = Max.Initialize.audioSource.panStereo.ToString("f2");
        value = Max.Initialize.audioSource.panStereo;
    }
    public Vector2  v = Vector2.zero;
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
        v = new Vector2(data.transform.position.x, data.transform.position.z);
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

        if (recordHand != null)
        {
            //计算移动比例
            float f = (new Vector2(recordHand.transform.position.x, recordHand.transform.position.z) - v).magnitude;

            DetectionManager.DetectionHand detectHand = DetectionManager.Get().GetHand(EHand.eLeftHand);

            Vector3 velocity = detectHand.GetVelocity();

            velocity = Camera.main.transform.InverseTransformDirection(velocity);
            int fangXiang = 0;
            if (velocity.x >= 0) //right
            {
                fangXiang = 1;
            }
            else if (velocity.x <= 0)//left
            {
                fangXiang = -1;
            }

            value += f * scale* fangXiang;
            v = new Vector2(recordHand.transform.position.x, recordHand.transform.position.z);

            text.text = Max.Initialize.audioSource.panStereo.ToString("f2");
            Max.Initialize.audioSource.panStereo = value;
        }
        value = Mathf.Clamp(value, -1, 1);
    }
}
