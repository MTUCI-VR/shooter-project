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
		private Transform attachTransform;

		private Vector3 _initialPosition;

		private XRGrabInteractable _grabInteractable;

		#endregion

		#region Properties

		private Vector3 leftHandAttachPosition => new Vector3(_initialPosition.x,
                                                       _initialPosition.y,
                                                       -1f * _initialPosition.z);

		private Vector3 rightHandAttachPosition => _initialPosition;		

		#endregion

		#region LifeCycle

		private void Awake()
		{
			_grabInteractable = GetComponent<XRGrabInteractable>();

			_initialPosition = attachTransform.localPosition;
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

		private void OnSelectEntered(SelectEnterEventArgs selectEnterEventArgs)
		{
			if(!selectEnterEventArgs.interactorObject.transform.TryGetComponent<GameHand>(out var gameHand))
				return;

            switch(gameHand.Side)
			{
				case HandSide.Left:
					attachTransform.localPosition = leftHandAttachPosition;
					break;
				case HandSide.Right:
					attachTransform.localPosition = rightHandAttachPosition;
					break;
			}
		}

		#endregion
    }
}
