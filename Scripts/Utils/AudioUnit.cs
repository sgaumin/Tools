using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace Tools.Utils
{
	public class AudioUnit : MonoBehaviour
	{
		private AudioSource audioSource;

		public event Action OnPlay;

		public bool playOnAwake { get; set; }
		public bool loop { get; set; }
		public AudioLoopType loopType { get; set; }
		public MinMax timeBetweenLoop { get; set; }
		public AudioMixerGroup outputAudioMixerGroup { get; set; }
		public AudioClip clip { get; set; }
		public AudioClip[] clips { get; set; }
		public float pitch { get; set; }
		public bool isGoingToStop { get; set; }
		public float duration { get; set; }

		public void Play()
		{
			audioSource = GetComponent<AudioSource>();

			audioSource.playOnAwake = playOnAwake;
			audioSource.loop = loop;
			audioSource.outputAudioMixerGroup = outputAudioMixerGroup;
			audioSource.clip = clip;
			audioSource.pitch = pitch;

			OnPlay?.Invoke();

			if (loopType == AudioLoopType.Manuel)
			{
				StartCoroutine(PlayLoop());
			}
			else
			{
				audioSource.Play();
				if (!loop)
				{
					StartCoroutine(WaitBeforeReturningToPool());
				}
			}
		}

		public void StopAndReturnToPool()
		{
			audioSource.Stop();
			AudioPool.ReturnToPool(this);
		}

		public void FadIn(float duration = 1f)
		{
			audioSource.volume = 0f;
			audioSource.DOFade(1f, duration).Play();
		}

		public void FadOut(float duration = 1f)
		{
			audioSource.DOFade(0f, duration).Play();
		}

		private IEnumerator PlayLoop()
		{
			while (true)
			{
				yield return new WaitForSeconds(timeBetweenLoop.RandomValue);
				audioSource.Play();

				if (clips != null)
				{
					audioSource.clip = clips.Random();
				}
			}
		}

		private IEnumerator WaitBeforeReturningToPool()
		{
			yield return new WaitForSeconds(audioSource.clip.length);
			AudioPool.ReturnToPool(this);
		}

		private void OnDisable()
		{
			StopAllCoroutines();
		}
	}
}
