using UnityEngine;

namespace Tools.Utils
{
	public class HideCursor : MonoBehaviour
	{
		private void Start()
		{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
	}
}
