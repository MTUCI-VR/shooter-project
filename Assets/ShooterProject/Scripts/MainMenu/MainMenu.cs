using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ShooterProject.Scripts.MainMenu
{
	public class MainMenu : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private string sceneForLoadName;

		[SerializeField]
		private Button startButton;

		[SerializeField]
		private Button quitButton;

		[SerializeField]
		private GameObject loadingBar;

		[SerializeField]
		private Image progressBar;

		[SerializeField]
		private TextMeshProUGUI progressText;

		#endregion

		#region Life Cycle

		private void OnEnable()
		{
			startButton.onClick.AddListener(OnStartButtonClick);
			quitButton.onClick.AddListener(OnQuitButtonClick);
		}

		private void OnDisable()
		{
			startButton.onClick.RemoveListener(OnStartButtonClick);
			quitButton.onClick.RemoveListener(OnQuitButtonClick);
		}

		#endregion

		#region Private Methods

		private void OnStartButtonClick()
		{
			StartCoroutine(SceneLoader.instance.LoadScene(sceneForLoadName));

			startButton.gameObject.SetActive(false);
			quitButton.gameObject.SetActive(false);
			loadingBar.SetActive(true);

			SceneLoader.instance.OnProgressChanged += OnProgressChanged;
		}

		private void OnQuitButtonClick()
		{
			Application.Quit();
		}

		private void OnProgressChanged()
		{
			progressBar.fillAmount = SceneLoader.instance.Progress;
			progressText.text = $"{(int)(SceneLoader.instance.Progress * 100)}%";

			if (SceneLoader.instance.Progress == 1)
				SceneLoader.instance.OnProgressChanged -= OnProgressChanged;
		}

		#endregion
	}
}
