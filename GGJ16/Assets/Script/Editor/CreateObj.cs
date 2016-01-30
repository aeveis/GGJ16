using UnityEngine;
using UnityEditor;

public class CreateObj
{
	[MenuItem("Assets/Create/ChantObj")]
	public static void CreateChantObj ()
	{
		ScriptableObjectUtility.CreateAsset<ChantObj> ();
	}
}