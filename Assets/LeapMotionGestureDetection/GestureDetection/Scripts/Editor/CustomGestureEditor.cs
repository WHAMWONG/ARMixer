using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public struct CustomGestureIcons
{
    public static Texture LoadDownArrow() { return Resources.Load<Texture>("Icons/DownArrow"); }
    public static Texture LoadUpArrow() { return Resources.Load<Texture>("Icons/UpArrow"); }
    public static Texture LoadRightArrow() { return Resources.Load<Texture>("Icons/RightArrow"); }
    public static Texture LoadLeftArrow() { return Resources.Load<Texture>("Icons/LeftArrow"); }
    public static Texture LoadPlusIcon() { return Resources.Load<Texture>("Icons/BasicGreenPlus"); }
    public static Texture LoadRemoveIcon() { return Resources.Load<Texture>("Icons/BasicBlackX"); }
}

[CustomEditor(typeof(CustomGesture))]
[CanEditMultipleObjects]
public class CustomGestureEditor : Editor
{
    CustomGesture TargetGesture;

    private void OnEnable()
    {
        TargetGesture = (CustomGesture)target;
    }


    bool DropDownOpen1 = false;
    bool DropDownOpen2 = false;

    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        DrawRequirementDropdown(EHand.eLeftHand, ref DropDownOpen1, AddItemLeftHand);
        DrawRequirementDropdown(EHand.eRightHand, ref DropDownOpen2, AddItemRightHand);
    }

    void DrawRequirementDropdown(EHand Hand, ref bool OutDropDownOpen, GenericMenu.MenuFunction2 AddFunc)
    {
        GUIStyle RemoveButton = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleLeft,
            stretchWidth = false
        };

        GUIStyle ExtrasButton = new GUIStyle(GUI.skin.button)
        {
            alignment = TextAnchor.MiddleLeft,
            stretchWidth = false
        };

        GUIStyle LabelText = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter,
            fontSize = 20,
            fontStyle = FontStyle.Bold
        };

        GUIStyle BorderStyle = new GUIStyle(EditorStyles.helpBox)
        {
            margin = new RectOffset(1, 1, 1, 1)
        };

        GUI.backgroundColor = Color.black;
        EditorGUILayout.BeginHorizontal(BorderStyle);
        GUI.backgroundColor = Color.white;
        EditorGUILayout.BeginHorizontal(BorderStyle);
        if (GUILayout.Button(OutDropDownOpen ? CustomGestureIcons.LoadDownArrow() : CustomGestureIcons.LoadRightArrow(), RemoveButton, GUILayout.Width(25), GUILayout.Height(25)))
        {
            OutDropDownOpen = !OutDropDownOpen;
        }

        GUILayout.Label((Hand == EHand.eLeftHand ? "Left" : "Right") + " Hand Requirements", LabelText);

        GUI.backgroundColor = Color.green;
        if (GUILayout.Button(CustomGestureIcons.LoadPlusIcon(), ExtrasButton, GUILayout.Width(25), GUILayout.Height(25)))
        {
            GenericMenu AddMenu = new GenericMenu();

            GestureRequirement[] AllRequirements = TargetGesture.GetListOfAllRequirements();
            foreach (GestureRequirement require in AllRequirements)
            {
                AddMenu.AddItem(new GUIContent(require.GetName()), false, AddFunc, require);
                AddMenu.AddSeparator("");

                AddMenu.ShowAsContext();
            }

        }
        GUI.backgroundColor = Color.white;
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndHorizontal();

        if (OutDropDownOpen)
        {
            List<GestureRequirementData> Requirements = TargetGesture.GetHandRequirements(Hand);

            SerializedProperty HandReqProp;

            if(Hand == EHand.eLeftHand)
            {
                HandReqProp = serializedObject.FindProperty("LeftHandRequirements");
            }
            else
            {
                HandReqProp = serializedObject.FindProperty("RightHandRequirements");
            }

            DrawCustomGestureDropdown(Requirements, Hand, HandReqProp);
        }
    }

    void AddItemRightHand(object requirement)
    {
        AddItem(requirement, EHand.eRightHand);
    }

    void AddItemLeftHand(object requirement)
    {
        AddItem(requirement, EHand.eLeftHand);
    }

    void AddItem(object requirement, EHand Hand)
    {
        GestureRequirement[] AllRequirements = TargetGesture.GetListOfAllRequirements();
        foreach (GestureRequirement Gesture in AllRequirements)
        {
            if (Gesture.GetType().ToString() == requirement.GetType().ToString())
            {
                TargetGesture.AddRequirement(Hand, Gesture);

                break;
            }
        }
    }

    void RemoveItem(GestureRequirement Gesture, EHand Hand)
    {
        TargetGesture.RemoveRequirement(Hand, Gesture);
    }

    void DrawCustomGestureDropdown(List<GestureRequirementData> Requirements, EHand Hand, SerializedProperty InHandProp)
    {
        GUIStyle BorderStyle = new GUIStyle(EditorStyles.helpBox);

        GUI.backgroundColor = Color.gray;
        EditorGUILayout.BeginVertical(BorderStyle);

        int index = 0;
        foreach (GestureRequirementData requirement in Requirements.ToArray())
        {
            SerializedProperty ReqArrayElem = InHandProp.GetArrayElementAtIndex(index);

            DrawCustomGestureWidget(requirement, Hand, ReqArrayElem);

            ++index;
        }

        EditorGUILayout.EndVertical();
    }
    
    void DrawCustomGestureWidget(GestureRequirementData requirement, EHand Hand, SerializedProperty InReqArrayElem)
    {
        GUIStyle RemoveButton = new GUIStyle(GUI.skin.button)
        {
            stretchWidth = false
        };

        GUIStyle ExtrasButton = new GUIStyle(GUI.skin.button)
        {
            stretchWidth = false
        };

        GUIStyle LabelText = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleLeft,
            fontSize = 15,
            fontStyle = FontStyle.Bold
        };

        GUIStyle BorderStyle = new GUIStyle(EditorStyles.helpBox);

        GUI.backgroundColor = Color.white;
        EditorGUILayout.BeginHorizontal(BorderStyle);
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button(CustomGestureIcons.LoadRemoveIcon(), RemoveButton, GUILayout.Width(25), GUILayout.Height(25)))
        {
            RemoveItem(requirement.m_Requirement, Hand);
        }
        GUI.contentColor = Color.white;
        GUI.backgroundColor = Color.white;
        GUILayout.Label(requirement.m_Requirement.GetName(), LabelText);

        SerializedProperty FloatProp = InReqArrayElem.FindPropertyRelative("m_FloatValue");
        SerializedProperty BoolProp = InReqArrayElem.FindPropertyRelative("m_BoolValue");
        SerializedProperty FingerProp = InReqArrayElem.FindPropertyRelative("m_FingerValue");
        SerializedProperty DirectionProp = InReqArrayElem.FindPropertyRelative("m_DirectionValue");
        SerializedProperty HandAxisProp = InReqArrayElem.FindPropertyRelative("m_HandAxisValue");

        EditorGUI.BeginChangeCheck();

        if(requirement.m_Requirement.m_DataRequirements.GetRequiresFinger())
        {
            EditorGUILayout.PropertyField(FingerProp, GUIContent.none);
        }


        if (requirement.m_Requirement.m_DataRequirements.GetRequiresHandAxis())
        {
            EditorGUILayout.PropertyField(HandAxisProp, GUIContent.none);
        }

        if (requirement.m_Requirement.m_DataRequirements.GetRequiresDirection())
        {
            EditorGUILayout.PropertyField(DirectionProp, GUIContent.none);
        }

        if (requirement.m_Requirement.m_DataRequirements.GetRequiresBool())
        {
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField(requirement.m_Requirement.m_DataRequirements.GetLabel(), GUILayout.Width(100));
            BoolProp.boolValue = EditorGUILayout.Toggle(BoolProp.boolValue, GUILayout.Width(20));
        }

        if (requirement.m_Requirement.m_DataRequirements.GetRequiresFloat())
        {
            float FloatMin = requirement.m_Requirement.m_DataRequirements.GetFloatMin();
            float FloatMax = requirement.m_Requirement.m_DataRequirements.GetFloatMax();
            FloatProp.floatValue = EditorGUILayout.Slider(FloatProp.floatValue, FloatMin, FloatMax);
        }

        if (EditorGUI.EndChangeCheck())
        {
            InReqArrayElem.serializedObject.ApplyModifiedProperties();
        }

        EditorGUILayout.EndHorizontal();
    }
}
