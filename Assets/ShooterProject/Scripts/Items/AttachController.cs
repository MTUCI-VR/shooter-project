using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using ShooterProject.Scripts.Hand;

namespace ShooterProject.Scripts.Items
{
    [RequireComponent(typeof(XRGrabInteractable))]
    public class AttachController : MonoBehaviour
    {
        #region Fields

		[SerializeField]
		private HandSide initialHandSide;

		[SerializeField]
		private SnapAxis axis;

		[SerializeField]
		private Transform attachTransform;

		private Vector3 _initialPosition;

		private Vector3 _mirroredPosition;

		private XRGrabInteractable _grabInteractable;

		#endregion

		#region LifeCycle

		private void Awake()
		{
			_grabInteractable = GetComponent<XRGrabInteractable>();

			_initialPosition = attachTransform.localPosition;

			_mirroredPosition = CalculateMirroredAttachPosition();
		}

		private void OnEnable()
		{
			_grabInteractable.selectEntered.AddListener(OnSelectEntered);
		}
		private void OnDisable()
		{
			_grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
		}

		#endregion

		#region Private Methods

		private Vector3 CalculateMirroredAttachPosition()
		{
			var newPosition = new Vector3();

			switch(axis)
			{
				case SnapAxis.X:
					newPosition = new Vector3(_initialPosition.x * -1f,
                                            _initialPosition.y,
                                            _initialPosition.z);
					break;
				case SnapAxis.Y:
					newPosition = new Vector3(_initialPosition.x,
                                            _initialPosition.y * -1f,
                                            _initialPosition.z);
					break;
				case SnapAxis.Z:
					newPosition = new Vector3(_initialPosition.x,
                                            _initialPosition.y,
                                            _initialPosition.z * -1f);
					break;
				default:
					newPosition = _initialPosition;
					break;
			}

			return newPosition;
		}

		private void OnSelectEntered(SelectEnterEventArgs selectEnterEventArgs)
		{
			if (!selectEnterEventArgs.interactorObject.transform.TryGetComponent<GameHand>(out var gameHand))
			{
				attachTransform.position = Vector3.zero;
				return;
			}

			attachTransform.localPosition = gameHand.Side == initialHandSide ? _initialPosition : _mirroredPosition;
		}

		#endregion
    }
}
