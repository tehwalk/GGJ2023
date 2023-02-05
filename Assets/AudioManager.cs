using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	[SerializeField] List<AudioSource> audioSources;

	public static AudioManager instance { get; private set; }

	void Awake() { instance = this; }

	public void SetVolume(AudioSource source, float endVolume) => source.volume = endVolume;
	public void PlayMainTheme() => audioSources[0].Play();
	public void PlaySFX(AudioClip audioclip, float volume = 1) => audioSources[1].PlayOneShot(audioclip, volume);
	public void PlayInteractionSound(AudioClip audioclip, float volume = 1) => audioSources[2].PlayOneShot(audioclip, volume);
}