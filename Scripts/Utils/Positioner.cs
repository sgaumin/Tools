using DG.Tweening;
using UnityEngine;

namespace Tools.Utils
{
	[System.Serializable]
	public class Positioner
	{
		[SerializeField] private Vector3 positiontarget;
		[SerializeField, Range(0f, 10f)] private float duration;
		[SerializeField] private Ease ease = Ease.InOutSine;
		[SerializeField] private LoopType loopType = LoopType.Yoyo;
		[SerializeField] private int loopCount = -1;
		[SerializeField] private bool playOnStart = false;

		public Vector3 Target => positiontarget;
		public float Duration => duration;
		public Ease Ease => ease;
		public LoopType LoopType => loopType;
		public int LoopCount => loopCount;
		public bool PlayOnStart => playOnStart;
	}
}
