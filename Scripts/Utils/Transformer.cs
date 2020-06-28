using DG.Tweening;
using Tools;
using Tools.Utils;
using UnityEngine;

public class Transformer : MonoBehaviour
{
	[SerializeField] private Positioner positioner;
	[SerializeField] private Rotater rotater;
	[SerializeField] private Scaler scaler;

	private Tween currentPositioner;
	private Tween currentRotater;
	private Tween currentScaler;
	private Vector3 startPostion;
	private Vector4 startRotation;
	private Vector3 startScale;
	private int rotationAmplitudeFactor;

	protected void OnEnable()
	{
		startPostion = transform.localPosition;
		startRotation = transform.localRotation.eulerAngles;
		startScale = transform.localScale;

		rotationAmplitudeFactor = rotater.HasFullAmplitude ? 2 : 1;

		if (positioner.PlayOnStart)
		{
			PlayLoop(TransformerType.Position, positioner.LoopCount);
		}

		if (rotater.PlayOnStart)
		{
			PlayLoop(TransformerType.Rotation, rotater.LoopCount);
		}

		if (scaler.PlayOnStart)
		{
			PlayLoop(TransformerType.Scale, scaler.LoopCount);
		}
	}

	public void PlayOnce(TransformerType type)
	{
		switch (type)
		{
			case TransformerType.Position:
				currentPositioner?.Kill();
				transform.localPosition = startPostion;
				currentPositioner = transform
					.DOLocalMove(transform.localPosition + positioner.Target, positioner.Duration)
					.SetEase(positioner.Ease)
					.Play();
				break;

			case TransformerType.Rotation:
				transform.localRotation = Quaternion.Euler(startRotation);
				currentRotater?.Kill();

				if (rotater.HasFullAmplitude)
				{
					transform.localRotation = Quaternion.Euler(new Vector4(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z - rotater.AngleTarget, 1));
				}

				Vector3 target = new Vector3(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z + rotationAmplitudeFactor * rotater.AngleTarget);
				currentRotater = transform
					.DORotate(target, rotater.Duration, RotateMode.FastBeyond360)
					.SetEase(rotater.Ease)
					.SetRelative()
					.Play();
				break;

			case TransformerType.Scale:
				currentScaler?.Kill();
				transform.localScale = startScale;
				currentScaler = transform
					.DOScale(scaler.Factor.RandomValue, scaler.Duration)
					.SetEase(scaler.Ease)
					.Play();
				break;
		}
	}

	public void PlayLoop(TransformerType type, int loop = -1)
	{
		switch (type)
		{
			case TransformerType.Position:
				currentPositioner?.Kill();
				transform.localPosition = startPostion;
				currentPositioner = transform
					.DOLocalMove(transform.localPosition + positioner.Target, positioner.Duration)
					.SetEase(positioner.Ease)
					.SetLoops(loop, positioner.LoopType)
					.Play();
				break;

			case TransformerType.Rotation:
				transform.localRotation = Quaternion.Euler(startRotation);
				currentRotater?.Kill();

				if (rotater.HasFullAmplitude)
				{
					transform.localRotation = Quaternion.Euler(new Vector4(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z - rotater.AngleTarget, 1));
				}

				Vector3 target = new Vector3(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z + rotationAmplitudeFactor * rotater.AngleTarget);
				currentRotater = transform
					.DORotate(target, rotater.Duration, RotateMode.FastBeyond360)
					.SetEase(rotater.Ease)
					.SetLoops(loop, positioner.LoopType)
					.SetRelative()
					.Play();
				break;

			case TransformerType.Scale:
				currentScaler?.Kill();
				transform.localScale = startScale;
				currentScaler = transform
					.DOScale(scaler.Factor.RandomValue, scaler.Duration)
					.SetEase(scaler.Ease)
					.SetLoops(loop, positioner
					.LoopType)
					.Play();
				break;
		}
	}

	public void ResetState()
	{
		currentPositioner?.Kill();
		transform.localPosition = startPostion;

		transform.localRotation = Quaternion.Euler(startRotation);
		currentRotater?.Kill();

		currentScaler?.Kill();
		transform.localScale = startScale;
	}
}
