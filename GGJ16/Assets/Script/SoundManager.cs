using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	
	public AudioSource efxSource;
	public static SoundManager instance = null;

	void Awake ()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
	}

	public void PlaySingle(AudioClip clip)
	{
		efxSource.clip = clip;

		//Play the clip.
		efxSource.volume = 1;
		efxSource.Play ();
	}
	public void FadeoutThenStop(int steps=5)
	{

		efxSource.Stop ();//StartCoroutine (Fadeout (steps));
	}
	public IEnumerator Fadeout(int steps) {
		float step = efxSource.volume / steps;
		while (efxSource.volume > 0) {
			efxSource.volume -= step;
			yield return null;
		}
		efxSource.Stop ();
	}
}
