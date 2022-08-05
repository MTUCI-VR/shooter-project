using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using ShooterProject.Scripts.PlayerScripts;

namespace ShooterProject.Scripts.GameManager
{
	public class MenuLoader : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private string menuSceneName;

		#endregion

		#region LifeCycle

		private void Start()
		{
			StartCoroutine(MenuSceneLoad());
		}

		#endregion

		#region Private Methods

		private IEnumerator MenuSceneLoad()
		{
			AsyncOperation asyncOperation =  SceneManager.LoadSceneAsync(menuSceneName,LoadSceneMode.Additive);

			while (!asyncOperation.isDone)
			{
				yield return new WaitForEndOfFrame();
			}

			Player.Instance.GetComponent<FadeTransition>().FadeTransitionEnd();
		}

		#endregion
	}
}
