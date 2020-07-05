using System;
using UnityEngine;
using UnityEngine.Audio;

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
		[SerializeField] private bool loop;
		[SerializeField] private bool isPitchModified;
		[SerializeField, MinMaxSlider(-1f, 1f)] private MinMax pitchMaxVariation = new MinMax(-0.2f, 0.2f);

		[Header("Component Behavior")]
		[SerializeField] private AutoDestroyTypes autoDestroy = AutoDestroyTypes.No;
		[SerializeField, Range(0f, 10f)] private float multiplier = 5f;

		public void Play()
		{
			// Initialization
			AudioUnit audioSource = AudioPool.GetFromPool();

			// Setup Paramaters
			audioSource.playOnAwake = false;
			audioSource.loop = loop;
			audioSource.outputAudioMixerGroup = mixerGroup;

			audioSource.clip = isUsingClips ? clips.Random() : clip;
			audioSource.pitch = isPitchModified ? 1f - pitchMaxVariation.RandomValue : audioSource.pitch;

			if (audioSource.clip == null)
			{
				Debug.LogWarning($"An audio unit is created without a clip.");

				AudioPool.ReturnToPool(audioSource);
				return;
			}

			audioSource.gameObject.name += audioSource.clip.name;

			// Auto Destroy
			switch (autoDestroy)
			{
				case AutoDestroyTypes.AutoDestroyAfterDuration:
					audioSource.duration = multiplier;
					break;
				case AutoDestroyTypes.AutoDestroyAfterPlays:
					audioSource.duration = audioSource.clip.length * (multiplier - 1);
					break;
			}

			// Play Sound
			audioSource?.Play();
		}
	}
}
