using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShooterProject.Scripts.GameManager.Menus
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

		private Image _progressBar;

		private TextMeshProUGUI _progressText;

		#endregion

		#region Life Cycle

		private void Awake()
		{
			_progressBar = loadingBar.GetComponentInChildren<Image>();
			_progressText = loadingBar.GetComponentInChildren<TextMeshProUGUI>();
		}

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
			_progressBar.fillAmount = SceneLoader.instance.Progress;
			_progressText.text = $"{(int)(SceneLoader.instance.Progress * 100)}%";

			if (SceneLoader.instance.SceneIsLoaded)
				SceneLoader.instance.OnProgressChanged -= OnProgressChanged;
		}

		#endregion
	}
}
