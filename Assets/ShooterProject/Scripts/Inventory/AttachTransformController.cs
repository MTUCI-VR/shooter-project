using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace ShooterProject.Scripts.Inventory
{
	[RequireComponent(typeof(XRSocketInteractor))]
	public class AttachTransformController : MonoBehaviour
	{
		#region Fields

		private XRSocketInteractor _socketInteractor;

		private Transform _initialObjectAttachTransform;

		#endregion

		#region Life Cycle

		private void Awake()
		{
			_socketInteractor = GetComponent<XRSocketInteractor>();
		}

		private void OnEnable()
		{
			_socketInteractor.selectEntered.AddListener(OnSelectEntered);
			_socketInteractor.selectExited.AddListener(OnSelectExited);
		}
		private void OnDisable()
		{
			_socketInteractor.selectEntered.RemoveListener(OnSelectEntered);
			_socketInteractor.selectExited.RemoveListener(OnSelectExited);
		}

		#endregion

		#region Private Methods

		private void OnSelectEntered(SelectEnterEventArgs selectEnterEventArgs)
		{
			var attachTransform = selectEnterEventArgs.interactableObject.transform.GetComponent<XRGrabInteractable>().attachTransform;

			_initialObjectAttachTransform = attachTransform;

			selectEnterEventArgs.interactableObject.transform.GetComponent<XRGrabInteractable>().attachTransform = null;
		}
		private void OnSelectExited(SelectExitEventArgs selectExitEventArgs)
		{
			selectExitEventArgs.interactableObject.transform.GetComponent<XRGrabInteractable>().attachTransform = _initialObjectAttachTransform;
		}

		#endregion
	}
}
