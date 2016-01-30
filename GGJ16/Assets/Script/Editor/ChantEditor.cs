using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Chant))]
public class ChantObjEditor : Editor {

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		GUI.enabled = false;
		SerializedProperty prop = serializedObject.FindProperty("m_Script");
		EditorGUILayout.PropertyField(prop, false);
		GUI.enabled = true;
		SerializedProperty code = serializedObject.FindProperty ("chantPattern");
		string chantPattern = code.stringValue;

		EditorGUILayout.LabelField ("Use only \"-\", \".\", and \" \" characters");
		char chr = Event.current.character;
		if ( chr !=' ' && chr !='.' && chr!='-') {
			Event.current.character = '\0';
		}
		chantPattern = EditorGUILayout.TextField (chantPattern);
		code.stringValue = chantPattern;

		EditorGUILayout.PropertyField (serializedObject.FindProperty ("startBufferTime"));
		EditorGUILayout.PropertyField (serializedObject.FindProperty ("chantFrequencyLength"));
		EditorGUILayout.PropertyField (serializedObject.FindProperty ("leeway"));
		//EditorGUILayout.PropertyField (code);
		serializedObject.ApplyModifiedProperties ();
	}
}
