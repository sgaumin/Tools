using UnityEngine;

namespace Tools.Utils
{
	public class OpenURL : MonoBehaviour
	{
		[SerializeField] private string link;

		public void Open()
		{
			if (!string.IsNullOrEmpty(link))
			{
				Application.OpenURL(link);
			}
		}
	}
}
