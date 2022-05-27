using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShooterProject.Scripts.GameManager.Menus
{
	public class MainMenu : Menu
	{
		#region Fields

		[SerializeField]
		private Button quitButton;

		#endregion

		#region Life Cycle

		protected override void OnEnable()
		{
			base.OnEnable();
			quitButton.onClick.AddListener(OnQuitButtonClick);
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			quitButton.onClick.RemoveListener(OnQuitButtonClick);
		}

		#endregion

		#region Private Methods

		private void OnQuitButtonClick()
		{
			Application.Quit();
		}

		#endregion
	}
}
