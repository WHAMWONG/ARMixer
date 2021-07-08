using UnityEngine;

namespace KnobsAsset
{
    /// <summary>
    /// Provides behavior of a knob that slides over a fixed range of motion.
    /// </summary>
    public class SliderKnob : Knob
    {
        [Header("Slider configuration")]
        [Tooltip("How far back the slider's minimum position is")]
        [SerializeField] private float MinPosition = -1f;

        [Tooltip("How far past the minimum position the slider can move")]
        [SerializeField] private float MovementRange = 2f;

        [Tooltip("How far along the range of motion the slider is. In the inspector this acts as the initial value")]
        [SerializeField] private float AmountMoved = 0f;

        private Vector3 handleInitialPosition;

        protected override void Start()
        {
            base.Start();
            handleInitialPosition = handle.localPosition;

            if (MovementRange < 0f)
            {
                Debug.LogWarning("Movement range should be positive", this);
            }
            if (AmountMoved < 0f || AmountMoved > MovementRange)
            {
                Debug.LogWarning("Amount moved should be within the movement range", this);
            }

            // set the position of the transform based on position
            SetPositionBasedOnAmountMoved();

            // propagate the initial knob value to listeners
            float positionPercentage = AmountMoved / MovementRange;
            OnValueChanged(positionPercentage);
        }

        private void Update()
        {
            if (grabbed)
            {
                // check how much to move based on mouse position
                Vector3 mousePos = MousePositionOnRelativePlane();// - grabbedMouseOffset;
                Vector3 mousePosOnAxis = PositionOnXAxisClosestToPoint(mousePos);
                float distance = Vector3.Distance(mousePosOnAxis, handle.position);
                float dot = Vector3.Dot(transform.forward, mousePosOnAxis - handle.position); // dot product is -1 when vectors point in opposite directions
                AmountMoved += (distance * (dot < 0f ? -1f : 1f));

                // clamp position to position range
                AmountMoved = Mathf.Clamp(AmountMoved, 0f, MovementRange);

                // set the position of the transform based on position
                SetPositionBasedOnAmountMoved();

                // propagate the changed knob value to listeners
                float positionPercentage = AmountMoved / MovementRange;
                OnValueChanged(positionPercentage);
            }
        }

        protected override void SetKnobPosition(float percentValue)
        {
            AmountMoved = Mathf.Lerp(0f, MovementRange, percentValue);
            SetPositionBasedOnAmountMoved();
        }

        private void SetPositionBasedOnAmountMoved()
        {
            Vector3 minPosition = (Vector3.forward * MinPosition);
            handle.localPosition = minPosition + (Vector3.forward * AmountMoved);
        }

        private Vector3 PositionOnXAxisClosestToPoint(Vector3 point)
        {
            Ray xAxis = new Ray(transform.position, transform.forward);
            return xAxis.origin + xAxis.direction * Vector3.Dot(xAxis.direction, point - xAxis.origin);
        }
    }
}
