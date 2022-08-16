using System;
using UnityEngine;

namespace ShooterProject.Scripts.GameManager.Menus
{
	public class MenuButton : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private string sceneForLoadName;

		#endregion

		#region Properties

		public string SceneForLoadName => sceneForLoadName;

		#endregion

		#region Events

		public event Action<MenuButton> onClick;

		#endregion

		#region Private Methods

		private void OnTriggerEnter(Collider collider)
		{
			if (collider.TryGetComponent<ButtonClicker>(out var buttonClicker))
			{
				onClick?.Invoke(this);
			}
		}

		#endregion
	}
}
