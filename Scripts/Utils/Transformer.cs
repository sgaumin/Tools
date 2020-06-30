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
	private Vector3 startPosition;
	private Vector3 startRotation;
	private Vector3 startScale;
	private int rotationAmplitudeFactor;

	protected void OnEnable()
	{
		startPosition = transform.localPosition;
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
				transform.localPosition = !positioner.IsReverting ? startPosition : transform.localPosition + positioner.Target;

				currentPositioner = transform
					.DOLocalMove(!positioner.IsReverting ? transform.localPosition + positioner.Target : startPosition, positioner.Duration)
					.SetEase(positioner.Ease)
					.Play();
				break;

			case TransformerType.Rotation:
				currentRotater?.Kill();
				Vector3 target = !rotater.IsReverting ? new Vector3(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z + rotationAmplitudeFactor * rotater.AngleTarget) : startRotation;

				if (rotater.HasFullAmplitude)
				{
					transform.localRotation = !rotater.IsReverting ? Quaternion.Euler(startRotation) : Quaternion.Euler(new Vector3(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z - rotater.AngleTarget));

					currentRotater = transform
							.DORotate(target, rotater.Duration, RotateMode.FastBeyond360)
							.SetEase(rotater.Ease)
							.SetRelative()
							.Play();
				}
				else
				{
					transform.localRotation = !rotater.IsReverting ? Quaternion.Euler(startRotation) : Quaternion.Euler(target);

					currentRotater = transform
						.DORotate(target, rotater.Duration, RotateMode.FastBeyond360)
						.SetEase(rotater.Ease)
						.SetRelative()
						.Play();
				}

				break;

			case TransformerType.Scale:
				currentScaler?.Kill();

				float currentFactor = scaler.Factor.RandomValue;
				transform.localScale = !scaler.IsReverting ? startScale : startScale * currentFactor;

				currentScaler = transform
					.DOScale(!scaler.IsReverting ? startScale * currentFactor : startScale, scaler.Duration)
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
				transform.localPosition = !positioner.IsReverting ? startPosition : transform.localPosition + positioner.Target;
				currentPositioner = transform
					.DOLocalMove(!positioner.IsReverting ? transform.localPosition + positioner.Target : startPosition, positioner.Duration)
					.SetEase(positioner.Ease)
					.SetLoops(loop, positioner.LoopType)
					.Play();
				break;

			case TransformerType.Rotation:
				currentRotater?.Kill();
				Vector3 target = !rotater.IsReverting ? new Vector3(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z + rotationAmplitudeFactor * rotater.AngleTarget) : startRotation;

				if (rotater.HasFullAmplitude)
				{
					transform.localRotation = !rotater.IsReverting ? Quaternion.Euler(startRotation) : Quaternion.Euler(new Vector3(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z - rotater.AngleTarget));

					currentRotater = transform
						.DORotate(target, rotater.Duration, RotateMode.FastBeyond360)
						.SetEase(rotater.Ease)
						.SetLoops(loop, positioner.LoopType)
						.SetRelative()
						.Play();
				}
				else
				{
					transform.localRotation = !rotater.IsReverting ? Quaternion.Euler(startRotation) : Quaternion.Euler(startRotation);

					currentRotater = transform
						.DORotate(target, rotater.Duration, RotateMode.FastBeyond360)
						.SetEase(rotater.Ease)
						.SetLoops(loop, positioner.LoopType)
						.SetRelative()
						.Play();
				}

				break;

			case TransformerType.Scale:
				currentScaler?.Kill();

				float currentFactor = scaler.Factor.RandomValue;
				transform.localScale = !scaler.IsReverting ? startScale : startScale * currentFactor;

				currentScaler = transform
					.DOScale(!scaler.IsReverting ? startScale * currentFactor : startScale, scaler.Duration)
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
		transform.localPosition = startPosition;

		transform.localRotation = Quaternion.Euler(startRotation);
		currentRotater?.Kill();

		currentScaler?.Kill();
		transform.localScale = startScale;
	}
}
