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
        mySound.transform.parent = transform;
        AudioSource source = mySound.AddComponent<AudioSource>();
        source.clip = clip;
        source.spatialBlend = 0.0f;
        //Play the clip.
        source.volume = 1;
        source.Play ();
	}
}
