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
			get
			{
				return _progress;
			}
			private set
			{
				if (_progress != value)
				{
					_progress = value;
					OnProgressChanged?.Invoke();
				}
			}
		}

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

			Progress = 0;

			do
			{
				Progress = sceneAsyncOperation.progress * (10f / 9f);

				yield return new WaitForEndOfFrame();

			} while (!sceneAsyncOperation.isDone);
		}

		#endregion
	}
}
