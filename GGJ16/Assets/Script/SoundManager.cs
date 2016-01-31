using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	
	public static SoundManager instance = null;
	public AudioSource bgMusicSource;
	public AudioClip bgMusic;

	void Awake ()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);

		bgMusicSource = gameObject.AddComponent<AudioSource>();
		bgMusicSource.volume = .5f;
		bgMusicSource.loop = true;
		bgMusicSource.clip = bgMusic;
		bgMusicSource.Play ();
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
