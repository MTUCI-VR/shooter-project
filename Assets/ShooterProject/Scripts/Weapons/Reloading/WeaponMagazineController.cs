using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace ShooterProject.Scripts.Weapons.Reloading
{
	[RequireComponent(typeof(XRSocketInteractor))]
	public class WeaponMagazineController : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private AudioSource _magazineAttachedAudioSource;

		private AmmoMagazine _attachedMagazine;
		private XRSocketInteractor _socketInteractor;
		#endregion

		#region Properties

		public AmmoMagazine AttachedMagazine
		{
			get => _attachedMagazine;

			private set
			{
				_attachedMagazine = value;
				OnAttachedMagazine?.Invoke();
			}
		}

		public bool MagazineAttached => _attachedMagazine != null;

		public bool HasAmmo => MagazineAttached && _attachedMagazine.HasAmmo;

		#endregion

		#region Events

		public event Action OnAttachedMagazine;

		#endregion

		#region LifeCycle Methods

		private void Awake()
		{
			_socketInteractor = GetComponent<XRSocketInteractor>();
		}

		private void OnEnable()
		{
			_socketInteractor.selectEntered.AddListener(OnObjectAttached);
			_socketInteractor.selectExited.AddListener(OnObjectDetached);
		}

		private void OnDisable()
		{
			_socketInteractor.selectEntered.RemoveListener(OnObjectAttached);
			_socketInteractor.selectExited.RemoveListener(OnObjectDetached);
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Уменьшает кол-во патронов в прикрепленном магазине.
		/// </summary>
		public void DecreaseAmmoCount()
		{
			if (MagazineAttached)
				AttachedMagazine.DecreaseAmmoCount();
		}

		#endregion

		#region Private Methods

		private void OnObjectAttached(SelectEnterEventArgs selectEnterEventArg)
		{
			var interactable = selectEnterEventArg.interactableObject.transform.gameObject;
			if (interactable.TryGetComponent<AmmoMagazine>(out AmmoMagazine magazine))
			{
				AttachedMagazine = magazine;

				_magazineAttachedAudioSource.Play();
			}
		}

		private void OnObjectDetached(SelectExitEventArgs selectExitEventArg)
		{
			if (AttachedMagazine != null)
			{
				AttachedMagazine = null;
			}
		}

		#endregion

	}
}
