using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadScreen : MonoBehaviour
{
	public static FadScreen Instance { get; private set; }

	private Image image;

	private void Awake()
	{
		Instance = this;
		image = GetComponentInChildren<Image>();
	}

	public void FadOut(Color colorTarget, float fadDuration = 1f)
	{
		image.color = new Color(colorTarget.r, colorTarget.g, colorTarget.b, 0f);
		image.DOFade(1f, fadDuration).Play();
	}

	public void FadIn(Color colorTarget, float fadDuration = 1f)
	{
		image.color = colorTarget;
		image.DOFade(0f, fadDuration).Play();
	}
}
