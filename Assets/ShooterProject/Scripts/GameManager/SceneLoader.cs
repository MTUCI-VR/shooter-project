using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using ShooterProject.Scripts.PlayerScripts;

namespace ShooterProject.Scripts.GameManager
{
	public static class SceneLoader
	{
		#region Fields

		private static float _progress;

		private static bool _sceneIsLoading;

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
					onProgressChanged?.Invoke();
				}
			}
		}

		#endregion

		#region Events

		public static event Action onProgressChanged;

		#endregion

		#region Public Methods

		/// <summary>
		/// Загружает указанную сцену
		/// </summary>
		/// <param name="sceneForLoadName">Название сцены для перехода</param>
		/// <param name="sceneForUnloadName">Название текущей сцены для выгрузки</param>
		public static IEnumerator LoadScene(string sceneForLoadName, string sceneForUnloadName)
		{
			if (_sceneIsLoading) yield break;

			_sceneIsLoading = true;

			FadeTransition.Instance.FadeTransitionStart();
			yield return new WaitForSeconds(FadeTransition.Instance.FadeBackgroundDuration);

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

			Player.Instance.transform.position = Vector3.zero;
			
			FadeTransition.Instance.FadeTransitionEnd();
			
			_sceneIsLoading = false;
		}

		#endregion
	}
}
