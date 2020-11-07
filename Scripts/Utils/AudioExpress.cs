using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Tools.Utils
{
	[Serializable]
	public class AudioExpress
	{
		[SerializeField] private bool isUsingClips;
		[SerializeField] private AudioClip clip;
		[SerializeField] private AudioClip[] clips;
		[SerializeField] private AudioMixerGroup mixerGroup;
		[SerializeField] private AudioLoopType loopType = AudioLoopType.No;
		[SerializeField, MinMaxSlider(0f, 10f)] private MinMax timeBetweenLoop = new MinMax(1f, 3f);
		[SerializeField] private bool isPitchModified;
		[SerializeField, MinMaxSlider(-1f, 1f)] private MinMax pitchMaxVariation = new MinMax(-0.2f, 0.2f);
		[SerializeField] private AudioStopType autoDestroy = AudioStopType.No;
		[SerializeField, Range(0f, 10f)] private float multiplier = 5f;

		public AudioUnit Play(string audioUnitPrefixName = null)
		{
			// Initialization
			AudioUnit audioSource = AudioPool.GetFromPool();

			// Setup Paramaters
			audioSource.playOnAwake = false;
			audioSource.loop = loopType == AudioLoopType.Normal;
			audioSource.loopType = loopType;
			audioSource.timeBetweenLoop = timeBetweenLoop;
			audioSource.outputAudioMixerGroup = mixerGroup;

			audioSource.clip = isUsingClips ? clips.Random() : clip;
			audioSource.clips = isUsingClips ? clips : null;
			audioSource.pitch = isPitchModified ? 1f - pitchMaxVariation.RandomValue : 1f;
			audioSource.isGoingToStop = autoDestroy != AudioStopType.No;

			if (audioSource.clip == null)
			{
				Debug.LogWarning($"An audio unit is created without a clip.");

				AudioPool.ReturnToPool(audioSource);
			}
			else
			{
				if (audioUnitPrefixName != null)
				{
					audioSource.gameObject.name = audioUnitPrefixName;
				}

				audioSource.gameObject.name += audioSource.clip.name;

				// Auto Destroy
				switch (autoDestroy)
				{
					case AudioStopType.StopAfterDuration:
						audioSource.duration = multiplier;
						break;
					case AudioStopType.StopAfterPlays:
						audioSource.duration = audioSource.clip.length * (multiplier - 1);
						break;
				}

				// Play Sound
				audioSource.Play();
			}

			return audioSource;
		}
	}
}
