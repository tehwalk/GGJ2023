using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[SerializeField] List<AudioSource> audioSources;
	[SerializeField] List<AudioClip> rootClips;

	public static AudioManager instance { get; private set; }

	void Awake()
	{
		if (instance != null && instance != this) Destroy(instance.gameObject);
		instance = this;
		//DontDestroyOnLoad(this.gameObject);
	}

	public void SetVolume(AudioSource source, float endVolume) => source.volume = endVolume;
	public void PlayMainTheme() => audioSources[0].Play();
	public void PlaySFX(AudioClip audioclip, float volume = 1) => audioSources[1].PlayOneShot(audioclip, volume);
	public void PlayCutClip(float volume = 1)
	{
		var randomIndex = Random.Range(0, rootClips.Count);
		audioSources[1].PlayOneShot(rootClips[randomIndex], volume);
		Debug.Log("heere");
	}

	public void PlayInteractionSound(AudioClip audioclip, float volume = 1) => audioSources[2].PlayOneShot(audioclip, volume);
}