using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tools
{
	public static class LevelLoader
	{
		/// <summary>
		/// Load the corresponding scene.
		/// </summary>
		/// <param name="levelLoading">Level to load.</param>
		public static void OnLoadLevel(LevelLoading levelLoading)
		{
			switch (levelLoading)
			{
				case LevelLoading.PreviousLevel:
					LevelLoader.LoadPreviousLevel();
					break;
				case LevelLoading.ReloadLevel:
					LevelLoader.ReloadLevel();
					break;
				case LevelLoading.NextLevel:
					LevelLoader.LoadNextLevel();
					break;
				case LevelLoading.FirstLevel:
					LevelLoader.LoadLevelByIndex(0);
					break;
			}
		}

		/// <summary>
		/// Reload the current level.
		/// </summary>
		public static void ReloadLevel()
		{
			LevelClear();
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		/// <summary>
		/// Load next level present in the build settings window.
		/// </summary>
		public static void LoadNextLevel()
		{
			LevelClear();
			int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
			int maxBuildIndex = SceneManager.sceneCountInBuildSettings;

			// We check if the current scene is not the last one.
			SceneManager.LoadScene(currentBuildIndex + (currentBuildIndex == maxBuildIndex ? 0 : 1));
		}

		/// <summary>
		/// Load next level present in the build settings window.
		/// </summary>
		public static void LoadPreviousLevel()
		{
			LevelClear();
			int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;

			// We check if the current scene is not the first one.
			SceneManager.LoadScene(currentBuildIndex + (currentBuildIndex != 0 ? -1 : 0));
		}

		/// <summary>
		/// Load a level by its name.
		/// </summary>
		/// <param name="name"></param>
		public static void LoadLevelByName(string name)
		{
			LevelClear();
			SceneManager.LoadScene(name);
		}

		/// <summary>
		/// Load a level by its build index.
		/// </summary>
		/// <param name="index"></param>
		public static void LoadLevelByIndex(int index)
		{
			LevelClear();
			SceneManager.LoadScene(index);
		}

		/// <summary>
		/// Quit the game or the editor mode.
		/// </summary>
		public static void QuitGame()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
		 Application.Quit();
#endif
		}

		private static void LevelClear() => DOTween.Clear(false);
	}
}