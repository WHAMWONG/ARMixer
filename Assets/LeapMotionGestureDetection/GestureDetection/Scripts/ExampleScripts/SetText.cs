using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowData
{
    public ShowData(string text, float time)
    {
        this.text = text;
        this.time = time;
    }

    public string text;
    public float time;
}

public class SetText : MonoBehaviour
{
    Text m_Text;

    float m_ShownTime = 5.0f;
    List<ShowData> DataList = new List<ShowData>();

    void Start()
    {
        m_Text = GetComponent<Text>();
    }

    private void Update()
    {
        string message = "Detected: ";

        List<ShowData> removeList = new List<ShowData>();

        for (int i = 0; i < DataList.Count; i++)
        {
            ShowData data = DataList[i];
            message += data.text + ", ";
            float time = data.time;
            time -= Time.deltaTime;
            if (time <= 0.0f)
            {
                removeList.Add(data);
            }
            data.time = time;
            DataList[i] = data;
        }

        m_Text.text = message;

        foreach (ShowData data in removeList)
        {
            DataList.Remove(data);
        }
    }

    public void Set(string text)
    {
        ShowData data = new ShowData(text, m_ShownTime);

        foreach (ShowData d in DataList)
        {
            if(d.text.Equals(text))
            {
                return;
            }
        }

        DataList.Add(data);
    }
}
