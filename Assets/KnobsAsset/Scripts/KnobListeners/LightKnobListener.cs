using UnityEngine;

namespace KnobsAsset
{
    /// <summary>
    /// A knob listener for assigning a knob value to parameters of a light.
    /// </summary>
    public class LightKnobListener : KnobListener
    {
        [Tooltip("Lights that the knob will affect")]
        [SerializeField] private Light[] Lights = default;

        [Tooltip("Whether or not the knob affects the light's intensity")]
        [SerializeField] private bool AdjustIntensity = true;
        [SerializeField] private float MinimumIntensity = 0f;
        [SerializeField] private float MaximumIntensity = 1f;

        [Tooltip("Whether or not the knob affects the light's color")]
        [SerializeField] private bool AdjustColor = true;
        [SerializeField] private Color MinimumColor = Color.yellow;
        [SerializeField] private Color MaximumColor = Color.white;

        [Tooltip("Whether or not the knob affects the light's range")]
        [SerializeField] private bool AdjustRange = true;
        [SerializeField] private float MinimumRange = 10f;
        [SerializeField] private float MaximumRange = 100f;

        private void Awake()
        {
            if (Lights.Length < 1)
            {
                Debug.LogWarning("No lights assigned, this listener will have no effect", this);
            }
        }

        public override void OnKnobValueChange(float knobPercentValue)
        {
            float intensity = Mathf.Lerp(MinimumIntensity, MaximumIntensity, knobPercentValue);
            Color color = Color.Lerp(MinimumColor, MaximumColor, knobPercentValue);
            float range = Mathf.Lerp(MinimumRange, MaximumRange, knobPercentValue);

            foreach (Light light in Lights)
            {
                if (AdjustIntensity)
                {
                    light.intensity = intensity;
                }
                if (AdjustColor)
                {
                    light.color = color;
                }
                if (AdjustRange)
                {
                    light.range = range;
                }
            }
        }
    }
}
