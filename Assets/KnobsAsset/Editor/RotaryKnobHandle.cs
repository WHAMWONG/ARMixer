using UnityEngine;
using UnityEditor;

namespace KnobsAsset
{
    /// <summary>
    /// Shows handles in the editor window to show a rotary knob's minimum angle, range of motion and current rotation value
    /// </summary>
    [CustomEditor(typeof(RotaryKnob))]
    public class RotaryKnobHandle : Editor
    {
        void OnSceneGUI()
        {
            // Get serialized private values
            float minAngle = serializedObject.FindProperty("MinAngle").floatValue;
            float angleRange = serializedObject.FindProperty("AngleRange").floatValue;
            float amountRotated = serializedObject.FindProperty("AmountRotated").floatValue;

            RotaryKnob myObj = (RotaryKnob)target;

            Vector3 startingAngle = Quaternion.AngleAxis(minAngle, myObj.transform.up) * myObj.transform.forward;

            float size = myObj.transform.localScale.magnitude;
            float radiusIncrement = size / 10f;

            // draw filled angle to show current rotation value
            Handles.color = new Color(1f, 0f, 0f, 0.5f);
            float radius = size;
            float filledAngleToDraw = amountRotated - minAngle;
            while (filledAngleToDraw > 360f)
            {
                Handles.DrawSolidArc(myObj.transform.position, myObj.transform.up, startingAngle, 360f, radius);
                filledAngleToDraw -= 360f;
                radius += radiusIncrement;
            }
            Handles.DrawSolidArc(myObj.transform.position, myObj.transform.up, startingAngle, filledAngleToDraw, radius);

            // draw unfilled angle to show entire range of motion
            Handles.color = Color.black;
            radius = size;
            float anglesToDraw = angleRange;
            while (anglesToDraw > 360f)
            {
                Handles.DrawWireArc(myObj.transform.position, myObj.transform.up, startingAngle, 360f, radius);
                anglesToDraw -= 360f;
                radius += radiusIncrement;
            }
            Handles.DrawWireArc(myObj.transform.position, myObj.transform.up, startingAngle, anglesToDraw, radius);

            // draw min angle as a line
            Handles.color = Color.yellow;
            Handles.DrawLine(myObj.transform.position, myObj.transform.position + (startingAngle * radius));
        }
    }
}
