using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShooterProject.Scripts.Teleport
{
	public class TeleportationToggler : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private InputActionProperty activateAction;

		[SerializeField]
		private GameObject teleportInteractor;

		#endregion

		#region LifeCycle

		private void Awake()
		{
			StartCoroutine(DisableInteractor());
		}

		private void OnEnable()
		{
			activateAction.action.performed += OnActivateActionPerformed;
			activateAction.action.canceled += OnActivateActionCanceled;
		}

		private void OnDisable()
		{
			activateAction.action.performed -= OnActivateActionPerformed;
			activateAction.action.canceled -= OnActivateActionCanceled;
		}

		#endregion

		#region Private Methods

		private void EnableInteractor()
		{
			teleportInteractor.SetActive(true);
		}

		private IEnumerator DisableInteractor()
		{
			yield return new WaitForEndOfFrame();
			teleportInteractor.SetActive(false);
		}

		private void OnActivateActionPerformed(InputAction.CallbackContext callbackContext)
		{
			EnableInteractor();
		}

		private void OnActivateActionCanceled(InputAction.CallbackContext callbackContext)
		{
			StartCoroutine(DisableInteractor());
		}

		#endregion
	}
}
