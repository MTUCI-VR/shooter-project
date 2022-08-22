using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using ShooterProject.Scripts.Effects;

namespace ShooterProject.Scripts.GameManager
{
	public static class SceneLoader
	{
		#region Fields

		private static float _progress;

		private static bool _isSceneLoading;

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

		#endregion

		#region Events

		public static event Action OnProgressChanged;

		public static event Action OnSceneSwitched;

		#endregion

		#region Public Methods

		/// <summary>
		/// Загружает указанную сцену
		/// </summary>
		/// <param name="sceneForLoadName">Название сцены для перехода</param>
		/// <param name="sceneForUnloadName">Название текущей сцены для выгрузки</param>
		public static IEnumerator LoadScene(string sceneForLoadName, string sceneForUnloadName)
		{
			if (_isSceneLoading)
				yield break;

			_isSceneLoading = true;

			FadeTransition.Instance.FadeTransitionStart();

			while (!FadeTransition.Instance.IsFadeBackroundDarkened)
				yield return new WaitForEndOfFrame();

			AsyncOperation sceneAsyncOperation = SceneManager.LoadSceneAsync(sceneForLoadName, LoadSceneMode.Additive);

			Progress = 0;

			do
			{
				Progress = sceneAsyncOperation.progress * (10f / 9f);

				yield return new WaitForEndOfFrame();

			} while (!sceneAsyncOperation.isDone);

			SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneForLoadName));

			if (!string.IsNullOrWhiteSpace(sceneForUnloadName))
				SceneManager.UnloadSceneAsync(sceneForUnloadName);

			FadeTransition.Instance.FadeTransitionEnd();

			_isSceneLoading = false;

			OnSceneSwitched?.Invoke();
		}

		#endregion
	}
}
