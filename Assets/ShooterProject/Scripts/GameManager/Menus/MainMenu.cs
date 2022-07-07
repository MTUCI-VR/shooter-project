using System;
using UnityEngine;

namespace ShooterProject.Scripts.GameManager.Menus
{
	public class MainMenu : Menu
	{
		#region Protected Methods

		protected override void OnMenuButtonClick(string sceneForLoadName)
		{
			if (!String.IsNullOrEmpty(sceneForLoadName))
			{
				base.OnMenuButtonClick(sceneForLoadName);
				return;
			}

			Application.Quit();
		}

		#endregion
	}
}
