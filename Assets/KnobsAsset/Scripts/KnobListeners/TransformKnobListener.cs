using UnityEngine;

namespace KnobsAsset
{
    /// <summary>
    /// Knob listener for assigning a knob value to change the transform values of an object.
    /// </summary>
    public class TransformKnobListener : KnobListener
    {
        [System.Serializable]
        private enum TransformTypes
        {
            POSITION, ROTATION, SCALE, LOCAL_POSITION, LOCAL_ROTATION
        }

        [Tooltip("The transform of the object that the knob will affect")]
        [SerializeField] private Transform TransformToManipulate = default;

        [Tooltip("The part of the transform to affect")]
        [SerializeField] private TransformTypes TransformType = TransformTypes.POSITION;

        [Tooltip("Minimum value to set the transform field to")]
        [SerializeField] private Vector3 MinValue = Vector3.zero;

        [Tooltip("Maximum value to set the transform field to")]
        [SerializeField] private Vector3 MaxValue = Vector3.one;

        [Tooltip("Whether or not the min and max values are adding to the initial values of the transform")]
        [SerializeField] private bool RelativeToInitialValue = true;

        private Vector3 initialPosition;
        private Vector3 initialRotation;
        private Vector3 initialScale;
        private Vector3 initialLocalPosition;
        private Vector3 initialLocalRotation;

        void Awake()
        {
            if (TransformToManipulate == null)
            {
                Debug.LogException(new MissingReferenceException("A Transform to manipulate is required"), this);
                return;
            }

            initialPosition = TransformToManipulate.position;
            initialRotation = TransformToManipulate.eulerAngles;
            initialScale = TransformToManipulate.localScale;
            initialLocalPosition = TransformToManipulate.localPosition;
            initialLocalRotation = TransformToManipulate.localEulerAngles;
        }

        public override void OnKnobValueChange(float knobPercentValue)
        {
            Vector3 transformValue = Vector3.Lerp(MinValue, MaxValue, knobPercentValue);
            switch (TransformType)
            {
                case TransformTypes.POSITION:
                    TransformToManipulate.position = transformValue + (RelativeToInitialValue ? initialPosition : Vector3.zero);
                    break;
                case TransformTypes.ROTATION:
                    TransformToManipulate.eulerAngles = transformValue + (RelativeToInitialValue ? initialRotation : Vector3.zero);
                    break;
                case TransformTypes.SCALE:
                    TransformToManipulate.localScale = Vector3.Scale((RelativeToInitialValue ? initialScale : Vector3.one), transformValue);
                    break;
                case TransformTypes.LOCAL_POSITION:
                    TransformToManipulate.localPosition = transformValue + (RelativeToInitialValue ? initialLocalPosition : Vector3.zero);
                    break;
                case TransformTypes.LOCAL_ROTATION:
                    TransformToManipulate.localEulerAngles = transformValue + (RelativeToInitialValue ? initialLocalRotation : Vector3.zero);
                    break;
                default:
                    Debug.LogException(new System.InvalidOperationException("Invalid TransformTypes value " + TransformType), this);
                    return;
            }
        }
    }
}
