using UnityEngine;

namespace ShooterProject.Scripts.GameManager.Menus
{
	public class MenuLoadingBar : MonoBehaviour
	{
		#region Life Cycle
		
		private void OnEnable()
		{
			SceneLoader.onProgressChanged += OnProgressChanged;
		}
		private void OnDisable()
		{
			SceneLoader.onProgressChanged -= OnProgressChanged;
		}

		#endregion

		#region Private Methods

		private void OnProgressChanged()
		{
			transform.localScale = Vector3.Lerp(transform.localScale,
				new Vector3(transform.localScale.x, SceneLoader.Progress, transform.localScale.z),
				Time.deltaTime);
		}

		#endregion
	}
}
