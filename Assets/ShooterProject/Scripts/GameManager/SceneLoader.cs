using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShooterProject.Scripts.GameManager
{
	public static class SceneLoader
	{
		#region Fields

		private static float _progress;

		#endregion

		#region Properties

		public static float Progress
		{
			get => _progress;

			private set
			{
				var tolerance = 0.000000001;

				if (!(Math.Abs(_progress - value) < tolerance))
				{
					_progress = value;
					OnProgressChanged?.Invoke();
				}
			}
		}

		private static bool sceneIsLoaded => Progress == 1;

		#endregion

		#region Events

		public static event Action OnProgressChanged;

		#endregion

		#region Public Methods

		/// <summary>
		/// Загружает указанную сцену
		/// </summary>
		/// <param name="sceneName">Название сцены для перехода</param>
		public static IEnumerator LoadScene(string sceneName)
		{
			AsyncOperation sceneAsyncOperation = SceneManager.LoadSceneAsync(sceneName);

			sceneAsyncOperation.allowSceneActivation = false;

			Progress = 0;

			do
			{
				Progress = sceneAsyncOperation.progress * (10f / 9f);

				yield return new WaitForEndOfFrame();

			} while (!sceneIsLoaded);

			sceneAsyncOperation.allowSceneActivation = true;
		}

		#endregion
	}
}
