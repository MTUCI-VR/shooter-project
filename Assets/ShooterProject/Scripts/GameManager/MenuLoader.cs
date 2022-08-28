using UnityEngine;
using UnityEngine.SceneManagement;

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
