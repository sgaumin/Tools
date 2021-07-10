using UnityEngine;

namespace Tools.Utils
{
	public class DontDestroyOnLoad : MonoBehaviour
	{
		private void Start() => DontDestroyOnLoad(gameObject);
	}
}