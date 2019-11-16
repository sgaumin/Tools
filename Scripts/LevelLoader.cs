using DG.Tweening;
using UnityEngine.SceneManagement;

public static class LevelLoader
{
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
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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