using UnityEngine;

public class DestroyAfterLoad : MonoBehaviour
{
	[SerializeField] private float durationBeforeDestroy;

	private void Start() => Initialize(durationBeforeDestroy);

	public void Initialize(float duration) => Destroy(gameObject, duration);
}
