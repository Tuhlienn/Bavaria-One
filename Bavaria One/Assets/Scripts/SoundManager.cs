using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
	// Audio players components.
	public AudioSource EffectsSource;
	public AudioSource MusicSource;

	// Random pitch adjustment range.
	public float LowPitchRange = .95f;
	public float HighPitchRange = 1.05f;

	private float effectsVolume = 0.0f;
	public float EffectsVolume 
	{
		get { return effectsVolume; }
		set
		{
			effectsVolume = value;
			EffectsSource.volume = effectsVolume;
		} 
	}

	private float musicVolume = 0.0f;
	public float MusicVolume 
	{
		get { return musicVolume; }
		set
		{
			musicVolume = value;
			MusicSource.volume = musicVolume;
		} 
	}

	void Start()
	{
		EffectsSource.volume = EffectsVolume;
		MusicSource.volume = MusicVolume;
	}

	// Play a single clip through the sound effects source.
	public void Play(AudioClip clip)
	{
		EffectsSource.clip = clip;
		EffectsSource.Play();
	}

	// Play a single clip through the music source.
	public void PlayMusic(AudioClip clip)
	{
		MusicSource.clip = clip;
		MusicSource.Play();
	}

	// Play a random clip from an array, and randomize the pitch slightly.
	public void RandomSoundEffect(params AudioClip[] clips)
	{
		int randomIndex = Random.Range(0, clips.Length);
		float randomPitch = Random.Range(LowPitchRange, HighPitchRange);

		EffectsSource.pitch = randomPitch;
		EffectsSource.clip = clips[randomIndex];
		EffectsSource.Play();
	}
	
}