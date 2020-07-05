using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace Tools.Utils
{
	public class AudioUnit : MonoBehaviour
	{
		private AudioSource audioSource;
		private Coroutine returnToPool;

		public event Action OnPlay;

		public bool playOnAwake { get; set; }
		public bool loop { get; set; }
		public AudioMixerGroup outputAudioMixerGroup { get; set; }
		public AudioClip clip { get; set; }
		public float pitch { get; set; }
		public float duration { get; set; }

		public void Play()
		{
			audioSource = GetComponent<AudioSource>();
			returnToPool = null;

			audioSource.playOnAwake = playOnAwake;
			audioSource.loop = loop;
			audioSource.outputAudioMixerGroup = outputAudioMixerGroup;
			audioSource.clip = clip;
			audioSource.pitch = pitch;

			OnPlay?.Invoke();
			audioSource.Play();

			if (!audioSource.loop)
			{
				returnToPool = StartCoroutine(WaitBeforeReturningToPool());
			}
		}

		private IEnumerator WaitBeforeReturningToPool()
		{
			yield return new WaitForSeconds(audioSource.clip.length);
			AudioPool.ReturnToPool(this);

			if (returnToPool != null)
			{
				StopCoroutine(returnToPool);
			}
		}
	}
}
