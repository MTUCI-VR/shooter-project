using System.Collections.Generic;
using UnityEngine;


namespace ShooterProject.Scripts.GameManager.Menus
{
	public class Menu : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		protected MenuLoadingBar loadingBarObjects;

		[SerializeField]
		protected List<MenuButtons> menuButtons;

		#endregion

		#region Events

		protected event System.Action OnMenuButtonsClick;

		#endregion

		#region Life Cycle

		protected virtual void OnEnable()
		{
			SceneLoader.OnProgressChanged += OnProgressChanged;
			
			foreach (var menuButton in menuButtons)
			{
				menuButton.button.onClick.AddListener(delegate { OnMenuButtonClick(menuButton); });
			}
		}
		protected virtual void OnDisable()
		{
			SceneLoader.OnProgressChanged -= OnProgressChanged;

			foreach (var menuButton in menuButtons)
			{
				menuButton.button.onClick.RemoveListener(delegate { OnMenuButtonClick(menuButton); });
			}
		}

		#endregion

		#region Private Methods

		private void OnMenuButtonClick(MenuButtons buttonForMenu)
		{
			StartCoroutine(SceneLoader.LoadScene(buttonForMenu.sceneForLoadName));

			OnMenuButtonsClick?.Invoke();

			menuButtons.ForEach(menuButton => menuButton.button.gameObject.SetActive(false));
			loadingBarObjects.loadingBar.SetActive(true);
		}

		private void OnProgressChanged()
		{
			loadingBarObjects._progress.fillAmount = SceneLoader.Progress;
			loadingBarObjects._progressText.text = $"{(int)(SceneLoader.Progress * 100)}%";
		}

		#endregion
	}
}