using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using ShooterProject.Scripts.Items;
using ShooterProject.Scripts.General;

namespace ShooterProject.Scripts.InteractableThings
{
	[RequireComponent(typeof(XRGrabInteractable))]
	public class ObjectHighlight : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private LayerMask objectHighlightLayer;

		private int _objectHighlightLayerValue;

		private int _initialLayerValue;
		private XRGrabInteractable _grabInteractable;

		#endregion

		#region Life Cycle

		private void Awake()
		{
			_grabInteractable = GetComponent<XRGrabInteractable>();

			_objectHighlightLayerValue = LayerUtils.GetLayerMaskValue(objectHighlightLayer);

			_initialLayerValue = gameObject.layer;
		}

		private void OnEnable()
		{
			_grabInteractable.hoverEntered.AddListener(OnHoverEntered);
			_grabInteractable.hoverExited.AddListener(OnHoverExited);
			_grabInteractable.selectEntered.AddListener(OnSelectEntered);
		}

		private void OnDisable()
		{
			_grabInteractable.hoverEntered.RemoveListener(OnHoverEntered);
			_grabInteractable.hoverExited.RemoveListener(OnHoverExited);
			_grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
		}

		#endregion

		#region Private Methods 
		private void OnHoverEntered(HoverEnterEventArgs hoverEnterEventArgs)
		{
			ObjectHighlightEnable(hoverEnterEventArgs.interactableObject.transform.gameObject);
		}

		private void OnHoverExited(HoverExitEventArgs hoverExitEventArgs)
		{
			ObjectHighlightDisable(hoverExitEventArgs.interactableObject.transform.gameObject);
		}

		private void OnSelectEntered(SelectEnterEventArgs selectEnterEventArgs)
		{
			ObjectHighlightDisable(selectEnterEventArgs.interactableObject.transform.gameObject);
		}

		private void ObjectHighlightEnable(GameObject objectHighlight)
		{
			objectHighlight.layer = _objectHighlightLayerValue;
		}

		private void ObjectHighlightDisable(GameObject objectHighlight)
		{
			objectHighlight.layer = _initialLayerValue;
		}

		#endregion
	}
}
