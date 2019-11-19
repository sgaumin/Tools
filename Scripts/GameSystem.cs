﻿using DG.Tweening;
using UnityEngine;

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