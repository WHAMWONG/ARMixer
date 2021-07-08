using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ObjActivation
{
    public string name;
    public GameObject obj;
}


public class GameObjectActivationToggle : MonoBehaviour
{
    public ObjActivation[] m_ActivationObjects;

    [Space(10)]
    public string m_StartActivatedObject = "none";

    Dictionary<string, GameObject> m_ObjectDict = new Dictionary<string, GameObject>();

    void Start()
    {
        foreach (ObjActivation objAct in m_ActivationObjects)
        {
            m_ObjectDict.Add(objAct.name, objAct.obj);
        }

        Activate( m_StartActivatedObject );
    }

    public void Activate(string name)
    {
        foreach( string objName in m_ObjectDict.Keys )
        {
            GameObject obj = m_ObjectDict[objName];

            if(obj)
            {
                obj.SetActive( objName == name );
            }
        }
    }

    public void DeactivateAll()
    {
        foreach (string objName in m_ObjectDict.Keys)
        {
            GameObject obj = m_ObjectDict[objName];

            if (obj)
            {
                obj.SetActive(false);
            }
        }
    }

    void Update()
    {

    }
}
