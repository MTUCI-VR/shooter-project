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

		private Quaternion _initialRotation;

		private Vector3 _mirroredPosition;

		private XRGrabInteractable _grabInteractable;

		#endregion

		#region LifeCycle

		private void Awake()
		{
			_grabInteractable = GetComponent<XRGrabInteractable>();

			_initialPosition = attachTransform.localPosition;

			_initialRotation = Quaternion.Euler(attachTransform.localRotation.eulerAngles.x,
												attachTransform.localRotation.eulerAngles.y,
												attachTransform.localRotation.eulerAngles.z);

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
				attachTransform.localPosition = Vector3.zero;
				attachTransform.localRotation = Quaternion.Euler(0,0,0);
				return;
			}

			if (gameHand.Side == initialHandSide)
			{
				attachTransform.localPosition = _initialPosition;
				attachTransform.localRotation = _initialRotation;
			}
			else
			{
				attachTransform.localPosition = _mirroredPosition;
				attachTransform.localRotation = _initialRotation;
			}
		}

		#endregion
    }
}
