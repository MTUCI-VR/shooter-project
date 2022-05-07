using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShooterProject.Scripts.GameManager
{
	public class SceneLoader : MonoBehaviour
	{
		#region Fields

		public static SceneLoader instance;

		private float _progress;

		#endregion

		#region Properties

		public float Progress
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

		public bool SceneIsLoaded { get; private set; }

		#endregion

		#region Events

		public event Action OnProgressChanged;

		#endregion

		#region Life Cycle

		private void Awake()
		{
			if (instance == null)
			{
				instance = this;
				DontDestroyOnLoad(gameObject);
			}
			else
			{
				Destroy(gameObject);
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Загружает указанную сцену
		/// </summary>
		/// <param name="sceneName">Название сцены для перехода</param>
		public IEnumerator LoadScene(string sceneName)
		{
			AsyncOperation sceneAsyncOperation = SceneManager.LoadSceneAsync(sceneName);

			do
			{
				Progress = sceneAsyncOperation.progress * (10f / 9f);

				SceneIsLoaded = sceneAsyncOperation.isDone;

				yield return null;

			} while (!sceneAsyncOperation.isDone);
		}

		#endregion
	}
}
