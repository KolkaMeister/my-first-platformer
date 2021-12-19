using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StartDialogComponent))]
public class DialoComponentEditor : UnityEditor.Editor
{
    private SerializedProperty modeProperty;

    private void OnEnable()
    {
        modeProperty = serializedObject.FindProperty("type");
    }
    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(modeProperty);
        DialogType type;
        if (modeProperty.GetEnum(out type))
        {
            switch (type)
            {
                case DialogType.Bound:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("dialog"));
                    break;
                case DialogType.External:
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("dialogDef"));
                    break;
                    
            }
        }
        serializedObject.ApplyModifiedProperties();
    }
}
