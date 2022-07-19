using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace ShooterProject.Scripts.Items
{
	[RequireComponent(
	typeof(ShooterProject.Scripts.Spawner.SpawnObjectParams),
	typeof(ShooterProject.Scripts.InteractableThings.ObjectHighlight))]
	public class Item : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private AudioSource audioSource;

		private XRGrabInteractable _grabInteractable;

		#endregion

		#region LifeCycle

		private void Awake()
		{
			_grabInteractable = GetComponent<XRGrabInteractable>();
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
			audioSource.Play();
		}

		#endregion
	}
}
