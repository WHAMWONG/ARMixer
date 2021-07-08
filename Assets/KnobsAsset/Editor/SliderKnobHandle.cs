using UnityEditor;
using UnityEngine;

namespace KnobsAsset
{
    /// <summary>
    /// Shows handles in the editor window to show a slider knob's minimum position, range of motion and current position value
    /// </summary>
    [CustomEditor(typeof(SliderKnob))]
    public class SliderKnobHandle : Editor
    {
        void OnSceneGUI()
        {
            // Get serialized private values
            float minPosition = serializedObject.FindProperty("MinPosition").floatValue;
            float movementRange = serializedObject.FindProperty("MovementRange").floatValue;
            float amountMoved = serializedObject.FindProperty("AmountMoved").floatValue;

            SliderKnob myObj = (SliderKnob)target;

            // draw range frame
            float frameWidth = myObj.transform.localScale.x * 0.3f;
            Handles.color = Color.blue;
            Vector3 minPos = myObj.transform.position + (myObj.transform.forward * minPosition * myObj.transform.localScale.z);
            Vector3 maxPos = myObj.transform.position + (myObj.transform.forward * (movementRange + minPosition) * myObj.transform.localScale.z);
            Handles.DrawLine(
                minPos + (myObj.transform.right * frameWidth),
                minPos + (myObj.transform.right * -frameWidth)
            );

            Handles.DrawLine(
                maxPos + (myObj.transform.right * frameWidth),
                maxPos + (myObj.transform.right * -frameWidth)
            );

            Handles.DrawLine(
                minPos,
                maxPos
            );

            // draw filled area to show current knob value
            float fillWidth = myObj.transform.localScale.x * 0.2f;
            Handles.color = new Color(1f, 0f, 0f, 0.5f);
            Vector3 movedToPos = myObj.transform.position + (myObj.transform.forward * (minPosition + amountMoved) * myObj.transform.localScale.z);
            Handles.DrawSolidRectangleWithOutline(
                new Vector3[] {
                minPos + (myObj.transform.right * fillWidth),
                minPos + (myObj.transform.right * -fillWidth),
                movedToPos + (myObj.transform.right * -fillWidth),
                movedToPos + (myObj.transform.right * fillWidth)
                },
                new Color(1f, 0.5f, 0.5f, 0.1f),
                Color.blue
            );
        }
    }
}
