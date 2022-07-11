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
		/// <param name="sceneForLoadName">Название сцены для перехода</param>
		/// <param name="sceneForUnloadName">Название текущей сцены для выгрузки</param>
		/// <param name="sceneType">Тип загружаемой сцены, для определения активности комнонентов игрока</param>
		public static IEnumerator LoadScene(string sceneForLoadName, string sceneForUnloadName, SceneType sceneType)
		{
			AsyncOperation sceneAsyncOperation = SceneManager.LoadSceneAsync(sceneForLoadName, LoadSceneMode.Additive);

			sceneAsyncOperation.allowSceneActivation = false;

			Progress = 0;

			do
			{
				Progress = sceneAsyncOperation.progress * (10f / 9f);

				yield return new WaitForEndOfFrame();

			} while (!sceneIsLoaded);

			SceneManager.UnloadSceneAsync(sceneForUnloadName);

			sceneAsyncOperation.allowSceneActivation = true;

			yield return new WaitForEndOfFrame();

			switch (sceneType)
			{
				case SceneType.Menu:
					Player.Instance.GetComponent<PlayerComponents>().DisableComponents();
					break;
				case SceneType.Game:
					Player.Instance.GetComponent<PlayerComponents>().EnableComponents();
					break;
			}
		}

		#endregion
	}
}
