using System.Collections.Generic;
using UnityEngine;

namespace ShooterProject.Scripts.GameManager.Menus
{
	public class Menu : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		protected List<MenuButton> menuButtons;

		[SerializeField]
		protected MenuLoadingBar loadingBar;

		#endregion

		#region Life Cycle

		protected virtual void OnEnable()
		{
			foreach (var menuButton in menuButtons)
			{
				menuButton.onClick += OnMenuButtonClick;
			}
		}
		protected virtual void OnDisable()
		{
			foreach (var menuButton in menuButtons)
			{
				menuButton.onClick -= OnMenuButtonClick;
			}
		}

		#endregion

		#region Protected Methods

		protected virtual void OnMenuButtonClick(string sceneForLoadName)
		{
			StartCoroutine(SceneLoader.LoadScene(sceneForLoadName, gameObject.scene.name, SceneType.Game));

			menuButtons.ForEach(menuButton => menuButton.gameObject.SetActive(false));
			loadingBar.gameObject.SetActive(true);
		}

		#endregion
	}
}
