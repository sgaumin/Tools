using DG.Tweening;
using UnityEngine;

namespace Tools.Utils
{
	[System.Serializable]
	public class Rotater
	{
		[SerializeField] private float angleTarget = 360f;
		[SerializeField] private bool hasFullAmplitude;
		[SerializeField, Range(0f, 10f)] private float duration;
		[SerializeField] private Ease ease = Ease.InOutSine;
		[SerializeField] private LoopType loopType = LoopType.Yoyo;
		[SerializeField] private int loopCount = -1;
		[SerializeField] private bool playOnStart = false;
		[SerializeField] private bool isReverting = false;
		[SerializeField] private bool isIgnoringTime = false;

		public float AngleTarget => angleTarget;
		public bool HasFullAmplitude => hasFullAmplitude;
		public float Duration => duration;
		public Ease Ease => ease;
		public LoopType LoopType => loopType;
		public int LoopCount => loopCount;
		public bool PlayOnStart => playOnStart;
		public bool IsReverting => isReverting;
		public bool IsIgnoringTime => isIgnoringTime;
	}
}
