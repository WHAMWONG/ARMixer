using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnobsAsset
{
    /// <summary>
    /// Class with public methods for changing gravity. For the physics demo scene
    /// </summary>
    public class PhysicsExampleController : MonoBehaviour
    {
        public void SetGravityDown()
        {
            Physics.gravity = Vector3.down * 9.81f;
        }

        public void SetGravityUp()
        {
            Physics.gravity = Vector3.up * 9.81f;
        }

        public void SetGravityLeft()
        {
            Physics.gravity = Vector3.left * 9.81f;
        }

        public void SetGravityRight()
        {
            Physics.gravity = Vector3.right * 9.81f;
        }
    }
}
