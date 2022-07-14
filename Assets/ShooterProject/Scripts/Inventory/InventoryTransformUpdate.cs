using UnityEngine;
using UnityEngine.InputSystem;
using ShooterProject.Scripts.PlayerScripts;

namespace ShooterProject.Scripts.Inventory
{
	public class InventoryTransformUpdate : MonoBehaviour
	{
		#region Fields

		[SerializeField]
        private InputActionProperty cameraPositionAcitonProperty;
        
        [SerializeField]
        private InputActionProperty cameraRotationAcitonProperty;

		[SerializeField]
		private Vector3 positionOffset;
    
    	[SerializeField]
		private Vector3 rotationLimit;

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
			cameraPositionAcitonProperty.action.performed += OnPositionActivateActionPerfomed;
			cameraRotationAcitonProperty.action.performed += OnRotationActivateActionPerfomed;
		}
		private void OnDisable()
		{
			cameraPositionAcitonProperty.action.performed -= OnPositionActivateActionPerfomed;
			cameraRotationAcitonProperty.action.performed -= OnRotationActivateActionPerfomed;
		}

		#endregion

		#region Private Methods

        private void OnPositionActivateActionPerfomed(InputAction.CallbackContext callbackContext)
		{
			var target = Player.Instance.transform.position + _playerController.center + positionOffset;

			transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * speed);
		}
        private void OnRotationActivateActionPerfomed(InputAction.CallbackContext callbackContext)
		{
			Vector3 eulerAngles = callbackContext.action.ReadValue<Quaternion>().eulerAngles;

            if (eulerAngles.x < rotationLimit.x)
                transform.rotation = Quaternion.AngleAxis(eulerAngles.y, Vector3.up);
		}

        #endregion
	}
}

