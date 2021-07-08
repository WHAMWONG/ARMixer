using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace KnobsAsset
{
    /// <summary>
    /// Provides behavior of a momentary button that can be pushed with mouse click or function calls.
    /// </summary>
    public class PushButton : MonoBehaviour
    {
        [System.Serializable]
        private enum PerformActionOnModes
        {
            ON_MOUSE_DOWN, ON_MOUSE_UP
        }

        [Header("Animation")]
        [Tooltip("Seconds it takes for the button to animate pushing in")]
        [SerializeField] private float PushInAnimationLength = 0.01f;

        [Tooltip("Seconds it takes for the button to animate pushing out")]
        [SerializeField] private float ReleaseAnimationLength = 0.01f;

        [Tooltip("How far the handle part of the button should move when pushed in")]
        [SerializeField] private float HandlePushInDistance = 0.1f;

        [Header("Sounds")]
        [Tooltip("Sound to play when the button is pressed")]
        [SerializeField] private AudioClip OnPressSound = default;

        [Tooltip("Sound to play when the button is released")]
        [SerializeField] private AudioClip OnReleaseSound = default;

        [Header("Action")]
        [Tooltip("At which point in a button press to perform the action. ON_MOUSE_DOWN performs the action when the button is pressed down, ON_MOUSE_UP performs the action when the button is released")]
        [SerializeField] private PerformActionOnModes PerformActionOn = PerformActionOnModes.ON_MOUSE_UP;

        [Tooltip("Action the button performs")]
        [SerializeField] private UnityEvent Action = default;

        private Transform handle;
        private Vector3 handleInitialPosition;

        private AudioSource audioSource;

        private Coroutine animationCoroutine;

        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();

            foreach (Transform child in GetComponentsInChildren<Transform>())
            {
                if (child.name.Equals("handle"))
                {
                    handle = child;
                    break;
                }
            }
            if (handle == null)
            {
                Debug.LogException(new MissingComponentException("Push Button needs a child called \"handle\""), this);
                return;
            }
            handleInitialPosition = handle.localPosition;
        }

        private void OnMouseDown()
        {
            PressButton();
        }

        private void OnMouseUp()
        {
            ReleaseButton();
        }

        private IEnumerator AnimateMove(Transform subject, Vector3 fromPosition, Vector3 toPosition, float duration)
        {
            if (duration > 0f)
            {
                float elapsedTime = 0f;
                while (elapsedTime < duration)
                {
                    subject.localPosition = Vector3.Lerp(fromPosition, toPosition, elapsedTime / duration);
                    yield return null;
                    elapsedTime += Time.deltaTime;
                }
            }
            subject.localPosition = toPosition;
        }

        /// <summary>
        /// Presses down the button
        /// </summary>
        public void PressButton()
        {
            // perform action is settings specify to do so
            if (PerformActionOn == PerformActionOnModes.ON_MOUSE_DOWN)
            {
                Action.Invoke();
            }
            if (animationCoroutine != null)
            {
                StopCoroutine(animationCoroutine);
            }

            // animate the button moving
            animationCoroutine = StartCoroutine(AnimateMove(
                handle,
                handle.localPosition,
                handleInitialPosition + (HandlePushInDistance * Vector3.down),
                PushInAnimationLength
            ));

            // play sound
            if (OnPressSound != null)
            {
                if (audioSource != null)
                {
                    audioSource.PlayOneShot(OnPressSound);
                }
                else
                {
                    Debug.LogWarning("Could not play sound effect, please attach an AudioSource", this);
                }
            }
        }

        /// <summary>
        /// Unpresses the button
        /// </summary>
        public void ReleaseButton()
        {
            // perform action is settings specify to do so
            if (PerformActionOn == PerformActionOnModes.ON_MOUSE_UP)
            {
                Action.Invoke();
            }

            // animate the button moving
            if (animationCoroutine != null)
            {
                StopCoroutine(animationCoroutine);
            }
            animationCoroutine = StartCoroutine(AnimateMove(
                handle,
                handle.localPosition,
                handleInitialPosition,
                ReleaseAnimationLength
            ));

            // play sound
            if (OnReleaseSound != null)
            {
                if (audioSource != null)
                {
                    audioSource.PlayOneShot(OnReleaseSound);
                }
                else
                {
                    Debug.LogWarning("Could not play sound effect, please attach an AudioSource", this);
                }
            }
        }

        /// <summary>
        /// Presses and then releases the button with a short time between pressing and releasing
        /// </summary>
        public void PressAndReleaseButton()
        {
            PressAndReleaseButton(0.1f);
        }

        /// <summary>
        /// Presses and releases the button with a specified amount of time between pressing and releasing
        /// </summary>
        /// <param name="timeBetweenPressAndRelease">Seconds between pressing and releasing the button</param>
        public void PressAndReleaseButton(float timeBetweenPressAndRelease)
        {
            StartCoroutine(PressAndReleaseButtonCoroutine(timeBetweenPressAndRelease));
        }

        private IEnumerator PressAndReleaseButtonCoroutine(float timeBetweenPressAndRelease)
        {
            PressButton();
            yield return new WaitForSeconds(timeBetweenPressAndRelease);
            ReleaseButton();
        }
    }
}
