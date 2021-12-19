using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;

    [CustomEditor(typeof(CustomButton),true)]
    [CanEditMultipleObjects]

public class CustomButtonEditor : ButtonEditor
{ 

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("normal"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("selected"));
        serializedObject.ApplyModifiedProperties();
        base.OnInspectorGUI();
    }
}
