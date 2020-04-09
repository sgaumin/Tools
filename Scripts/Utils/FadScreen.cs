using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.Utils
{
    public class FadScreen : MonoBehaviour
    {
        public static FadScreen Instance { get; private set; }

        private Color defaultFadColorTarget = Color.black;
        private Ease defaultEase = Ease.Linear;
        private Image image;

        private void Awake()
        {
            Instance = this;
            image = GetComponentInChildren<Image>();
        }

        public void FadOut(Color? colorTarget = null, float fadDuration = 1f, Ease? ease = null)
        {
            image.color = colorTarget != null ? new Color((float)colorTarget?.r, (float)colorTarget?.g, (float)colorTarget?.b, 0f) : (Color)colorTarget;
            image.DOFade(1f, fadDuration).SetEase(ease ?? defaultEase).Play();
        }

        public IEnumerator FadOutCore(Color? colorTarget = null, float fadDuration = 1f, Ease? ease = null)
        {
            image.color = colorTarget != null ? new Color((float)colorTarget?.r, (float)colorTarget?.g, (float)colorTarget?.b, 0f) : (Color)colorTarget;
            Tweener fad = image.DOFade(1f, fadDuration).SetEase(ease ?? defaultEase).Play();
            yield return fad.WaitForCompletion();
        }

        public void FadIn(Color? colorTarget = null, float fadDuration = 1f, Ease? ease = null)
        {
            image.color = colorTarget ?? defaultFadColorTarget;
            image.DOFade(0f, fadDuration).SetEase(ease ?? defaultEase).Play();
        }

        public IEnumerator FadInCore(Color? colorTarget = null, float fadDuration = 1f, Ease? ease = null)
        {
            image.color = colorTarget ?? defaultFadColorTarget;
            Tweener fad = image.DOFade(0f, fadDuration).SetEase(ease ?? defaultEase).Play();
            yield return fad.WaitForCompletion();
        }
    }
}
