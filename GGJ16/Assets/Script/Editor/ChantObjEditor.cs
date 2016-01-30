using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ChantObj))]
public class ChantObjEditor : Editor {

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		GUI.enabled = false;
		SerializedProperty prop = serializedObject.FindProperty("m_Script");
		EditorGUILayout.PropertyField(prop, false);
		GUI.enabled = true;
		SerializedProperty code = serializedObject.FindProperty ("chantCode");
		string chantCode = code.stringValue;

		EditorGUILayout.LabelField ("Use only \"-\", \".\", and \" \" characters");
		char chr = Event.current.character;
		if ( chr !=' ' && chr !='.' && chr!='-') {
			Event.current.character = '\0';
		}
		chantCode = EditorGUILayout.TextField (chantCode);
		code.stringValue = chantCode;
		//EditorGUILayout.PropertyField (code);
		serializedObject.ApplyModifiedProperties ();
	}
}
