using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

partial class DetectionManager
{
    public class DetectionFinger
    {
        Finger m_Finger;
        Finger.FingerType m_Type;

        public void SetFinger(Finger a_Finger)
        {
            m_Type = a_Finger.Type;
            m_Finger = a_Finger;
        }

        public EFinger GetFingerType()
        {
            return (EFinger)((int)m_Type);
        }

        public Quaternion GetFingerRotation()
        {
            return m_Finger.bones[0].Rotation.ToQuaternion();
        }

        public Vector3 GetFingerDirection()
        {
            return m_Finger.Direction.ToVector3();
        }

        public Vector3 GetTipPosition()
        {
            Vector pos = m_Finger.TipPosition;
            return pos.ToVector3();
        }

        //Deprecated
        public Vector3 GetTipVelocity()
        {
            return new Vector3(0,0,0);
        }

        public Quaternion GetBoneRotation(ESpecificBone a_Bone)
        {
            return GetBone(a_Bone).Rotation.ToQuaternion();
        }

        public Vector3 GetBoneDirection(ESpecificBone a_Bone)
        {
            return GetBone(a_Bone).Direction.ToVector3();
        }

        public Vector3 GetBonePosition(ESpecificBone a_Bone)
        {
            Vector pos = GetBone(a_Bone).Center;
            return pos.ToVector3();
        }

        Bone GetBone(ESpecificBone a_Bone)
        {
            return m_Finger.bones[(int)a_Bone];
        }

        public bool IsExtended()
        {
            return m_Finger.IsExtended;
        }

        void Start()
        {

        }

        void Update()
        {

        }
    }
}
