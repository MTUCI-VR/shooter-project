using UnityEngine;
using UnityEngine.UI;

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

			SceneLoader.instance.OnProgressChanged += () => {progressBar.fillAmount = SceneLoader.instance.Progress;};
		}

		private void OnQuitButtonClick()
		{
			Debug.Log("Quit");
			Application.Quit();
		}

		#endregion
	}
}
