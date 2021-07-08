using UnityEngine;

namespace KnobsAsset
{
    /// <summary>
    /// Abstract base class for defining behavior that reacts to a knob changing value.
    /// </summary>
    [RequireComponent(typeof(Knob))]
    public abstract class KnobListener : MonoBehaviour
    {
        /// <summary>
        /// Performs an action based on the new value of the knob.
        /// </summary>
        /// <param name="knobPercentValue">Float from [0 - 1] corresponding to what position the knob is set to.</param>
        public abstract void OnKnobValueChange(float knobPercentValue);
    }
}
