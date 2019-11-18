using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class GameSystem : MonoBehaviour
{
	protected virtual void Awake()
	{
		DOTween.Init();
		DOTween.defaultAutoPlay = AutoPlay.None;
		DOTween.defaultAutoKill = false;
	}

	protected virtual void Update()
	{
#if UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX
		if (Input.GetButtonDown("Quit"))
		{
			LevelLoader.QuitGame();
		}
#endif
	}
}