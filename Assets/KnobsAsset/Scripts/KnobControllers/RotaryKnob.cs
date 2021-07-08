using UnityEngine;

namespace KnobsAsset
{
    /// <summary>
    /// Provides behavior of a knob that rotates on a fixed range of motion.
    /// </summary>
    public class RotaryKnob : Knob
    {
        [System.Serializable]
        private enum InteractionTypes
        {
            VERTICAL_DRAG, ROTATION_DRAG
        }

        [Header("Rotation configuration")]
        [Tooltip("The angle the knob points when at it's minimum value")]
        [SerializeField] private float MinAngle = -135f;

        [Tooltip("The total rotational range of motion the knob can turn in. Can be >360 for a multi-rotate knob")]
        [SerializeField] private float AngleRange = 270f;

        [Tooltip("Angle past the min angle that the knob is currently turned. Acts as the knob's initial value when editing it from the inspector")]
        [SerializeField] private float AmountRotated = 0f;

        [Header("Rotation interaction")]
        [Tooltip("Only used in VERTICAL_DRAG mode. How much the mouse movement affects the knob rotation")]
        [SerializeField] private float MouseDragSensitivity = 50f;

        [Tooltip("How the mouse affects the knob's rotation")]
        [SerializeField] private InteractionTypes InteractionType = InteractionTypes.VERTICAL_DRAG;

        private Vector3 handleInitialRotation;

        private Vector3 prevMouseDirection;
        private Vector3 grabbedMouseOffset;

        protected override void Start()
        {
            base.Start();

            if (AngleRange < 0f)
            {
                Debug.LogWarning("AngleRange should be positive", this);
            }

            if (AmountRotated < MinAngle || AmountRotated > MinAngle + AngleRange)
            {
                Debug.LogWarning("Initial AmountRotated should be within angle range", this);
            }

            handleInitialRotation = handle.localEulerAngles;
            if (handle.localPosition != Vector3.zero)
            {
                Debug.LogException(new System.InvalidOperationException("Knob handle position needs to be (0, 0, 0)"), this);
                return;
            }

            // Set the rotation of the transform to the rotation variable
            SetRotationBasedOnAmountRotated();

            // propagate the initial knob value to listeners
            float rotationPercentage = (AmountRotated - MinAngle) / AngleRange;
            OnValueChanged(rotationPercentage);
        }

        void Update()
        {
            if (grabbed)
            {
                // Change the amountRotated based on mouse movement
                float angleToRotate;
                switch (InteractionType)
                {
                    case InteractionTypes.VERTICAL_DRAG:
                        angleToRotate = (Input.GetAxis("Mouse Y") / Screen.height) * 100f * MouseDragSensitivity;
                        break;
                    case InteractionTypes.ROTATION_DRAG:
                        Vector3 mousePosOnPlane = MousePositionOnRelativePlane() - grabbedMouseOffset;
                        angleToRotate = Vector3.SignedAngle(prevMouseDirection - transform.position, mousePosOnPlane - transform.position, transform.up);
                        prevMouseDirection = mousePosOnPlane;
                        break;
                    default:
                        Debug.LogException(new System.InvalidOperationException("Invalid InteractionTypes value " + InteractionType), this);
                        return;
                }
                AmountRotated += angleToRotate;

                // Clamp knob rotation to the range parameters
                AmountRotated = Mathf.Clamp(AmountRotated, MinAngle, MinAngle + AngleRange);

                // Set the rotation of the transform to the rotation variable
                SetRotationBasedOnAmountRotated();

                // propagate the changed knob value to listeners
                float rotationPercentage = (AmountRotated - MinAngle) / AngleRange;
                OnValueChanged(rotationPercentage);
            }
        }

        public override void OnGrabbed()
        {
            base.OnGrabbed();
            grabbedMouseOffset = MousePositionOnRelativePlane() - transform.position;
        }

        protected override void SetKnobPosition(float percentValue)
        {
            AmountRotated = Mathf.Lerp(MinAngle, MinAngle + AngleRange, percentValue);
            SetRotationBasedOnAmountRotated();
        }

        private void SetRotationBasedOnAmountRotated()
        {
            handle.localEulerAngles = handleInitialRotation + (Vector3.up * AmountRotated);
        }
    }
}
