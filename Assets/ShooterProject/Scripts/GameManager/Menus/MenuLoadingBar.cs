using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShooterProject.Scripts.GameManager.Menus
{
	public class MenuLoadingBar : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private Image progress;

		[SerializeField]
		private TextMeshProUGUI progressText;

		#endregion

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
			progress.fillAmount = SceneLoader.Progress;
			progressText.text = $"{(int)(SceneLoader.Progress * 100)}%";
		}

		#endregion
	}
}
