using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using ShooterProject.Scripts.GameManager;

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
			SceneManager.LoadSceneAsync(menuSceneName, LoadSceneMode.Additive);
		}

		#endregion
	}
}
