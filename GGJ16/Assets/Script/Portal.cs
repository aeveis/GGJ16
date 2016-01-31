using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            int nextLevel = Application.loadedLevel + 1;
            if (nextLevel >= Application.levelCount)
                nextLevel = 0;

            Application.LoadLevel(nextLevel);
        }
    }
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel (0);
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			Application.LoadLevel (Application.loadedLevel);
		}
		if (Input.GetKeyDown (KeyCode.RightBracket)) {
			Application.LoadLevel (Application.loadedLevel+1);
		}
		if (Input.GetKeyDown (KeyCode.LeftBracket)) {
			Application.LoadLevel (Application.loadedLevel-1<0?0:(Application.loadedLevel-1));
		}
	}
}
