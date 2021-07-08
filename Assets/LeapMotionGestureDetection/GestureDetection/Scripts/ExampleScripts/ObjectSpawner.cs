using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

    public GameObject m_Object;
    public Transform m_SpawnLocation;

    void Start()
    {

    }

    void Update()
    {

    }

    public void Spawn()
    {
        Instantiate(m_Object, m_SpawnLocation.position, m_SpawnLocation.rotation);
    }
}
