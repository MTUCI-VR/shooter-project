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

		private void Awake()
		{
			quitButton.onClick.AddListener(OnQuitButtonClick);
		}

		#endregion

		#region Private Methods

		private void OnQuitButtonClick()
		{
			Debug.Log("dsa");
			quitButton.onClick.RemoveListener(OnQuitButtonClick);

			Application.Quit();
		}

		#endregion
	}
}
