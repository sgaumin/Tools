using UnityEngine;

namespace Tools.Utils
{
	public class DontDestroyOnLoad : MonoBehaviour
	{
		void Start() => DontDestroyOnLoad(gameObject);
	}
}
