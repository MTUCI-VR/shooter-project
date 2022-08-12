using System.Collections.Generic;
using UnityEngine;

namespace ShooterProject.Scripts.GameManager.Menus
{
	public class Menu : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private List<MenuButton> menuButtons;

		[SerializeField]
		private MenuLoadingBar loadingBar;

		#endregion

		#region Life Cycle

		private void OnEnable()
		{
			foreach (var menuButton in menuButtons)
			{
				menuButton.onClick += OnMenuButtonClick;
			}
		}
		private void OnDisable()
		{
			foreach (var menuButton in menuButtons)
			{
				menuButton.onClick -= OnMenuButtonClick;
			}
		}

		#endregion

		#region Private Methods

		private void OnMenuButtonClick(MenuButton menuButton)
		{
			if (menuButton.SceneType == SceneType.Quit)
			{
				Application.Quit();
				return;
			}

			StartCoroutine(SceneLoader.LoadScene(menuButton.SceneForLoadName, gameObject.scene.name, menuButton.SceneType));
			loadingBar.gameObject.SetActive(true);
		}

		#endregion
	}
}
