using System.Collections.Generic;
using UnityEngine;

namespace ShooterProject.Scripts.GameManager.Menus
{
	public class Menu : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private List<MenuButton> menuButtons;

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
			if (string.IsNullOrWhiteSpace(menuButton.SceneForLoadName))
			{
				Application.Quit();
				return;
			}

			StartCoroutine(SceneLoader.LoadScene(menuButton.SceneForLoadName, gameObject.scene.name));
		}

		#endregion
	}
}
