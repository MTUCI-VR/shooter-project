using System.Collections;
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

		#region Life Cycle

		protected virtual void OnEnable()
		{
			foreach (var menuButton in menuButtons)
			{
				menuButton.button.onClick.AddListener(delegate{OnMenuButtonClick(menuButton);});
			}
		}
		protected virtual void OnDisable()
		{
			foreach (var menuButton in menuButtons)
			{
				menuButton.button.onClick.RemoveListener(delegate{OnMenuButtonClick(menuButton);});
			}
		}

		#endregion

		#region Private Methods

		private void OnMenuButtonClick(MenuButtons buttonForMenus)
		{
			StartCoroutine(SceneLoader.instance.LoadScene(buttonForMenus.sceneForLoadName));

			buttonForMenus.button.gameObject.SetActive(false);
			loadingBarObjects.loadingBar.SetActive(true);

			SceneLoader.instance.OnProgressChanged += OnProgressChanged;
		}

		private void OnProgressChanged()
		{
			loadingBarObjects._progress.fillAmount = SceneLoader.instance.Progress;
			loadingBarObjects._progressText.text = $"{(int)(SceneLoader.instance.Progress * 100)}%";

			if (SceneLoader.instance.SceneIsLoaded)
				SceneLoader.instance.OnProgressChanged -= OnProgressChanged;
		}

		#endregion
	}
}
