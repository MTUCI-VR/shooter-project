using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShooterProject.Scripts.MainMenu
{
	[System.Serializable]
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
				_progress = value;
				OnProgressChanged?.Invoke();
			}
		}

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
		/// <param name="sceneForLoadName">Название сцены для перехода</param>
		public IEnumerator LoadScene(string sceneForLoadName)
		{
			AsyncOperation sceneAsyncOperation = SceneManager.LoadSceneAsync(sceneForLoadName);

			do
			{
				Progress = sceneAsyncOperation.progress * (10f / 9f);

				yield return null;

			} while (!sceneAsyncOperation.isDone);
		}

		#endregion
	}
}
