using UnityEngine;
using System.Collections;

public class CivieTouch : MonoBehaviour {

	public GameObject exclamation;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col) {
		Debug.Log (col.gameObject.name);
		if (col.gameObject.name == "Player") {
			exclamation.SetActive (true);
			StartCoroutine (waitandTurnOff ());
		}
	}
	IEnumerator waitandTurnOff() {
		yield return new WaitForSeconds (.5f);
		exclamation.SetActive (false);
	}
}
