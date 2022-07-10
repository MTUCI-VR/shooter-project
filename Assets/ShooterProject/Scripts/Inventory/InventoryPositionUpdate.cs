using UnityEngine;
using UnityEngine.InputSystem;
using ShooterProject.Scripts.PlayerScripts;

namespace ShooterProject.Scripts.Inventory
{
	public class InventoryPositionUpdate : MonoBehaviour
	{
		#region Fields

		[SerializeField]
        private InputActionProperty cameraPositionAcitonProperty;

		[SerializeField]
		private Vector3 positionOffset;

		[SerializeField]
		private int speed;

		private CharacterController _playerController;

		#endregion

		#region LifeCycle

		private void Awake()
		{
			_playerController = Player.Instance.GetComponent<CharacterController>();
		}

		private void OnEnable()
		{
			cameraPositionAcitonProperty.action.performed += OnActivateActionPerfomed;
		}
		private void OnDisable()
		{
			cameraPositionAcitonProperty.action.performed -= OnActivateActionPerfomed;
		}

		#endregion

		#region Private Methods

        private void OnActivateActionPerfomed(InputAction.CallbackContext callbackContext)
		{
			var target = Player.Instance.transform.position + _playerController.center + positionOffset;

			transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * speed);
		}

        #endregion
	}
}

