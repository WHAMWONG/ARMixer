using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchBagSpawner : MonoBehaviour
{
    public GameObject PunchBag;

    PunchingBag CurrentPunchBag;

    float Timer;

    void Start()
    {
        SpawnNewPunchBag();
    }

    private void Update()
    {
        if(Timer > 0.0f)
        {
            Timer -= Time.deltaTime;
            if(Timer <= 0.0f)
            {
                Timer = 0.0f;
                SpawnNewPunchBag();
            }
        }
    }

    public void LeftHandPunch()
    {
        CurrentPunchBag.LeftHandPunch();
    }

    public void RightHandPunch()
    {
        CurrentPunchBag.RightHandPunch();
    }

    public void StartSpawnTimer()
    {
        Timer = 1.0f;
    }

    public void SpawnNewPunchBag()
    {
        GameObject obj = Instantiate(PunchBag, transform.position, transform.rotation);
        CurrentPunchBag = obj.GetComponent<PunchingBag>();
        CurrentPunchBag.Setup(this);
    }
}
