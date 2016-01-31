using UnityEngine;
using System.Collections;

public class Shadow : MonoBehaviour {

	public float y = 0;
	private Vector3 pos;
	private bool moveShadow = false;
	void Awake () {
		pos = new Vector3 ();
		StartCoroutine (WaitforY ());
	}

	IEnumerator WaitforY(){
		yield return new WaitForSeconds (.05f);
		y = transform.position.y+.05f;
		moveShadow = true;
	}
	// Update is called once per frame
	void Update () {
		if (moveShadow) {
			pos = transform.parent.position;
			pos.y = y;
			transform.position = pos;
		}
	
	}
}
