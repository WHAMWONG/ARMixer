using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPunchingBagState
{
    Stablizing,
    PunchState
}

public class PunchingBag : MonoBehaviour
{
    Rigidbody rb;

    EPunchingBagState CurrentPunchState = EPunchingBagState.Stablizing;

    Collider PunchBagCollider;

    PunchBagSpawner SpawnedBy;

    MeshRenderer Renderer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        PunchBagCollider = GetComponent<Collider>();
    }

    void Update()
    {
        
    }

    public void LeftHandPunch()
    {
        if(CurrentPunchState == EPunchingBagState.Stablizing)
        {
            HandPunch(EHand.eLeftHand);
        }
    }

    public void RightHandPunch()
    {
        if (CurrentPunchState == EPunchingBagState.Stablizing)
        {
            HandPunch(EHand.eRightHand);
        }
    }

    void HandPunch(EHand InHand)
    {
        DetectionManager.DetectionHand DetectHand = DetectionManager.Get().GetHand(InHand);
        if(DetectHand.IsSet())
        {
            if(rb)
            {
                rb.useGravity = true;
                PunchBagCollider.isTrigger = false;
                rb.AddForce(DetectHand.GetVelocity() * 200);
                CurrentPunchState = EPunchingBagState.PunchState;
                if(SpawnedBy)
                {
                    SpawnedBy.StartSpawnTimer();
                }
            }
        }
    }

    public void Setup(PunchBagSpawner BagSpawner)
    {
        SpawnedBy = BagSpawner;
    }
}
