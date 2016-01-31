using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	
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
        GameObject mySound = new GameObject(clip.name);
        AudioSource source = mySound.AddComponent<AudioSource>();
        source.clip = clip;

        //Play the clip.
        source.volume = 1;
        source.Play ();
	}
}
