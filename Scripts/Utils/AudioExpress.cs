using System;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace Tools.Utils
{
	[Serializable]
	public class AudioExpress
	{
		public enum AutoDestroyTypes
		{
			No,
			AutoDestroyAfterDuration,
			AutoDestroyAfterPlays
		}

		[Header("References")]
		[SerializeField] private bool isUsingClips;
		[SerializeField] private AudioClip clip;
		[SerializeField] private AudioClip[] clips;
		[SerializeField] private AudioMixerGroup mixerGroup;

		[Header("Audio Parameters")]
		[SerializeField] private bool attached;
		[SerializeField] private bool loop;
		[SerializeField] private bool isPitchModified;
		[SerializeField, MinMaxSlider(-1f, 1f)] private MinMax pitchMaxVariation = new MinMax(-0.2f, 0.2f);

		[Header("Component Behavior")]
		[SerializeField] private bool isDontDestroyOnLoad;
		[SerializeField] private AutoDestroyTypes autoDestroy = AutoDestroyTypes.No;
		[SerializeField, Range(0f, 10f)] private float multiplier = 5f;

		private AudioSource audioSource;

		public void Play(GameObject gameObject)
		{
			// Initialization
			if (audioSource is null)
			{
				audioSource = attached ?
					gameObject.AddComponent<AudioSource>() :
					new GameObject("Audio", typeof(AudioSource)).GetComponent<AudioSource>();

				if (isDontDestroyOnLoad)
				{
					audioSource.gameObject.AddComponent<DontDestroyOnLoad>();
				}

				// Setup Paramaters
				audioSource.playOnAwake = false;
				audioSource.loop = loop;
				audioSource.outputAudioMixerGroup = mixerGroup;
			}

			audioSource.clip = isUsingClips ? clips[Random.Range(0, clips.Length)] : clip;
			audioSource.pitch = isPitchModified ? 1f - pitchMaxVariation.RandomValue : audioSource.pitch;

			// Auto Destroy
			if (!attached)
			{
				switch (autoDestroy)
				{
					case AutoDestroyTypes.AutoDestroyAfterDuration:
						audioSource.gameObject.AddComponent<DestroyAfterLoad>().Initialize(multiplier);
						break;
					case AutoDestroyTypes.AutoDestroyAfterPlays:
						audioSource.gameObject.AddComponent<DestroyAfterLoad>().Initialize(audioSource.clip.length * (multiplier - 1));
						break;
				}
			}

			// Play Sound
			audioSource?.Play();
		}
	}
}
