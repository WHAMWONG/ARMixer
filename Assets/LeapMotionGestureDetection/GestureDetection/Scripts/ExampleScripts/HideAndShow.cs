using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAndShow : MonoBehaviour
{
    public bool originalState;
    public GameObject[] objects;

    private void Start()
    {
        SetVisibleState(originalState);
    }

    public void SetVisibleState(bool visible)
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(visible);
        }
    }
}
