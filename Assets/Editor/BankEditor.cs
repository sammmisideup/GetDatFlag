using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;




[CustomEditor(typeof(BGMBanks))]
[CanEditMultipleObjects]
public class BGMBanks : Editor
{
    private SerializedProperty bgmBanks;

    private void OnEnable()
    {
        bgmBanks = serializedObject.FindProperty("BGMBanks");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(bgmBanks, true);

        serializedObject.ApplyModifiedProperties();
    }
}

//288148