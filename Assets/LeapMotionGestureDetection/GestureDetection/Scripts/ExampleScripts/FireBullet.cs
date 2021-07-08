using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public EHand m_Hand;

    public GameObject m_Bullet;

    public float m_Speed;

    bool bSpawnBullet = false;

    bool bGunActive = false;

    void Start()
    {

    }

    public void Shoot()
    {
        bSpawnBullet = true;
    }

    public void StartGun()
    {
        bGunActive = true;
    }

    public void EndGun()
    {
        bGunActive = false;
    }

    void Update()
    {
        DetectionManager.DetectionHand detectHand = DetectionManager.Get().GetHand(m_Hand);

        if ( detectHand.IsSet() )
        {
            if( bSpawnBullet && bGunActive )
            {
                bSpawnBullet = false;

                Vector3 dir = detectHand.GetDirectionToFingers();
                dir.Normalize();
                Vector3 startPos = detectHand.GetFinger(EFinger.eIndex).GetTipPosition();

                GameObject obj = Instantiate(m_Bullet, startPos, m_Bullet.transform.rotation);
                obj.GetComponent<Rigidbody>().velocity = dir * m_Speed;
            }
        }
    }
}
