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
					onProgressChanged?.Invoke();
				}
			}
		}

		#endregion

		#region Events

		public static event Action onProgressChanged;

		#endregion

		#region Private Methods

		private static void SwitchPlayerComponents(SceneType sceneType)
		{
			var playerComponentsSwither = Player.Instance.GetComponent<PlayerComponents>();

			switch (sceneType)
			{
				case SceneType.Menu:
					playerComponentsSwither.DisableComponents();
					break;
				case SceneType.Game:
					playerComponentsSwither.EnableComponents();
					break;
			}
		}

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
			var fadeTransition = Player.Instance.GetComponent<FadeTransition>();

			fadeTransition.FadeTransitionStart();
			yield return new WaitForSeconds(fadeTransition.FadeTransitionDuration);

			AsyncOperation sceneAsyncOperation = SceneManager.LoadSceneAsync(sceneForLoadName, LoadSceneMode.Additive);

			Progress = 0;

			do
			{
				Progress = sceneAsyncOperation.progress * (10f / 9f);

				yield return new WaitForEndOfFrame();

			} while (!sceneAsyncOperation.isDone);

			if (!string.IsNullOrWhiteSpace(sceneForUnloadName))
				SceneManager.UnloadSceneAsync(sceneForUnloadName);

			fadeTransition.FadeTransitionEnd();

			SwitchPlayerComponents(sceneType);
		}

		#endregion
	}
}
