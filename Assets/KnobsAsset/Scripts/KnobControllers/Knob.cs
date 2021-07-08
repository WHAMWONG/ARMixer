using UnityEngine;
using UnityEngine.Events;

namespace KnobsAsset
{
    /// <summary>
    /// Abstract base class that provides functionality for a knob that has a handle, listeners and taper curve
    /// </summary>
    public abstract class Knob : MonoBehaviour
    {
        [System.Serializable]
        private class FloatEvent : UnityEvent<float> { }

        [Header("Output value")]
        [Tooltip("Whether or not to use the TaperCurve")]
        [SerializeField] private bool UseCurve = true;

        [Tooltip("Curves the output value of the knob")]
        [SerializeField] private AnimationCurve TaperCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        [Header("Interaction")]
        [Tooltip("How much to multiply the size of the handle by when grabbed")]
        [SerializeField] private float HandleGrabbedScaleMultiplier = 1.1f;

        [Tooltip("Actions to call (should take a float as a parameter to get the knob value) when the knob changes value")]
        [SerializeField] private FloatEvent Action = default;

        private KnobListener[] knobListeners;

        protected bool grabbed = false;
        private Vector3 baseScale;

        protected Transform handle;

        protected virtual void Start()
        {
            // Get references to all KnobListeners that this knob will call to update
            knobListeners = GetComponents<KnobListener>();
            if (knobListeners.Length < 1 && Action.GetPersistentEventCount() < 1)
            {
                Debug.LogWarning(name + " has no attached listeners, and no assigned actions, meaning this knob will have no effect", this);
            }

            // get the handle of the knob
            handle = transform.Find("handle");
            if (handle == null)
            {
                Debug.LogException(new MissingComponentException("Knob needs to have a child called \"handle\""), this);
                return;
            }
            baseScale = handle.localScale;
        }

        /// <summary>
        /// A method for changing the knob's value from script or event. Changes the knob's position/orientation and sets the new value.
        /// </summary>
        /// <param name="percentValue">Float from [0 - 1] specifying the value to set the knob to</param>
        public void SetValue(float percentValue)
        {
            if (percentValue < 0f || percentValue > 1f)
            {
                Debug.LogException(new System.ArgumentOutOfRangeException("percentValue", percentValue, "Setting knob value requires value from [0 - 1]"), this);
                return;
            }
            SetKnobPosition(percentValue);
            OnValueChanged(percentValue);
        }

        protected abstract void SetKnobPosition(float percentValue);

        public void OnMouseDown()
        {
            OnGrabbed();
        }

        private void OnMouseUp()
        {
            OnReleased();
        }

        /// <summary>
        /// Called when the user grabs the knob, for example when clicked with the mouse.
        /// </summary>
        public virtual void OnGrabbed()
        {
            grabbed = true;
            handle.localScale = baseScale * HandleGrabbedScaleMultiplier;
        }

        /// <summary>
        /// Called when the user releases the knob, for example when the mouse is released after clicking the knob.
        /// </summary>
        public virtual void OnReleased()
        {
            handle.localScale = baseScale;
            grabbed = false;
        }

        /// <summary>
        /// A method to call when the knob has changed value, for example when concrete descendant classes are being interacted with by the user.
        /// </summary>
        /// <param name="knobPercentage"></param>
        protected void OnValueChanged(float knobPercentage)
        {
            // Check that the knob is set to a valid percentage
            if (knobPercentage < 0f || knobPercentage > 1f)
            {
                Debug.LogException(new System.ArgumentOutOfRangeException("knobPercentage", knobPercentage, "Setting knob value requires value from [0 - 1]"), this);
                return;
            }

            // Calculate the knob value on the taper curve
            float knobValue = knobPercentage;
            if (UseCurve)
            {
                knobValue = TaperCurve.Evaluate(knobValue);
            }

            // Pass the new knob value to any attached KnobListeners so they can handle the change
            foreach (KnobListener knobListener in knobListeners)
            {
                knobListener.OnKnobValueChange(knobValue);
            }

            // Pass the new knob value to any assigned action so they can handle the change
            Action.Invoke(knobValue);
        }

        protected Vector3 MousePositionOnRelativePlane()
        {
            Plane plane = new Plane(transform.up, transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out float hitDist))
            {
                Vector3 targetPoint = ray.GetPoint(hitDist);
                return targetPoint;
            }
            return Vector3.zero;
        }
    }
}
