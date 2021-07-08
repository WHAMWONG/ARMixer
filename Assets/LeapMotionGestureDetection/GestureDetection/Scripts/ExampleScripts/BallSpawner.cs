using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject m_Object;

    public void SpawnAtLeftHand()
    {
        Vector3 pos = DetectionManager.Get().GetHand(EHand.eLeftHand).GetFinger(EFinger.eIndex).GetTipPosition();
        Instantiate(m_Object, pos, m_Object.transform.rotation);
    }

    public void SpawnAtRightHand()
    {
        Vector3 pos = DetectionManager.Get().GetHand(EHand.eRightHand).GetFinger(EFinger.eIndex).GetTipPosition();
        Instantiate(m_Object, pos, m_Object.transform.rotation);
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
