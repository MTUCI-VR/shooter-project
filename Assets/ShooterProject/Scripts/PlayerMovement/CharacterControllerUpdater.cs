using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace ShooterProject.Scripts.PlayerMovement
{
    [RequireComponent(typeof(FixedCharacterControllerDriver))]
    public class CharacterControllerUpdater : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private InputActionProperty cameraPositionAcitonProperty;

        private FixedCharacterControllerDriver _characterControllerDriver;

        private bool isMoving;

        #endregion

        #region Life Cycle

        private void Awake()
        {
            _characterControllerDriver = GetComponent<FixedCharacterControllerDriver>();
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
            _characterControllerDriver.UpdateCharacter();
		}

        #endregion
	}
}
