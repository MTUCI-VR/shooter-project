using System;
using UnityEngine;
using UnityEngine.UI;

namespace ShooterProject.Scripts.GameManager.Menus
{
	[RequireComponent(typeof(Button))]
	public class MenuButton : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private SceneType sceneType;

		[SerializeField]
		private string sceneForLoadName;

		private Button _button;

		#endregion

		#region Events

		public event Action<string, SceneType> onClick;

		#endregion

		#region Life Cycle

		private void Awake()
		{
			_button = GetComponent<Button>();
		}

		private void OnEnable()
		{
			_button.onClick.AddListener(OnClick);
		}
		private void OnDisable()
		{
			_button.onClick.RemoveListener(OnClick);
		}

		#endregion

		#region Private Methods

		private void OnClick()
		{
			onClick?.Invoke(sceneForLoadName, sceneType);
		}

		#endregion
	}
}
