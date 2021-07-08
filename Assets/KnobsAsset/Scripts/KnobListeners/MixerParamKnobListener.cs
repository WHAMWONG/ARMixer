using UnityEngine;
using UnityEngine.Audio;

namespace KnobsAsset
{
    /// <summary>
    /// Knob listener for assigning a knob value to an exposed mixer parameter.
    /// </summary>
    public class MixerParamKnobListener : KnobListener
    {
        [Tooltip("The Audio Mixer to affect a parameter on")]
        [SerializeField] private AudioMixer Mixer = default;

        [Tooltip("The mixer parameter to affect. Make sure to expose it in the mixer.")]
        [SerializeField] private string ExposedParamName = "Put Your Exposed Parameter Name Here";

        [Tooltip("Minimum value to set the parameter to")]
        [SerializeField] private float MinValue = -80f;

        [Tooltip("Maximum value to set the parameter to")]
        [SerializeField] private float MaxValue = 0f;

        private void Awake()
        {
            if (Mixer == null)
            {
                Debug.LogException(new MissingReferenceException("Reference to an Audio Mixer is required"), this);
            }
            if (ExposedParamName == null)
            {
                Debug.LogException(new System.ArgumentNullException("ExposedParamName", "Exposed param name should not be null"), this);
            }
        }

        public override void OnKnobValueChange(float knobPercentValue)
        {
            float newValue = Mathf.Lerp(MinValue, MaxValue, knobPercentValue);
            Mixer.SetFloat(ExposedParamName, newValue);
        }
    }
}
