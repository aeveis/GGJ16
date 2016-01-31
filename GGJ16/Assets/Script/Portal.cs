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
	}
}
