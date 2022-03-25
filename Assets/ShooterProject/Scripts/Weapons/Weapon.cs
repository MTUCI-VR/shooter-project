using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace ShooterProject.Scripts.Items.Weapons
{
	
	public class Weapon : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private InputActionProperty activateAction;

		[SerializeField]
		private int _shootingDelaySeconds;

		[SerializeField]
		private Transform _bulletSpawner;

		[SerializeField]
		private GameObject _bulletPrefab;

		[SerializeField]
		private float _damage;
		[SerializeField]
		private XRGrabInteractable _interactableHandle;

		private AmmoMagazine _includedMagazine;
		private bool _isSelected = false;
		private bool _coolDownOver = true;
		private Coroutine _workingShootingCoroutine;

		#endregion

		#region Properties
		public bool IsSelected => _isSelected;
		private bool CanShoot => _isSelected && _includedMagazine != null && _includedMagazine.HasAmmo;
		#endregion

		#region LifeCycle

		private void OnEnable()
		{
			activateAction.action.performed += OnActivateActionPerformed;
			activateAction.action.canceled += OnActivateActionCanceled;

			_interactableHandle.selectEntered.AddListener(OnSelectEntered);
			_interactableHandle.selectExited.AddListener(OnSelectExited);
		}

		private void OnDisable()
		{
			activateAction.action.performed -= OnActivateActionPerformed;
			activateAction.action.canceled -= OnActivateActionCanceled;

			_interactableHandle.selectEntered.RemoveListener(OnSelectEntered);
			_interactableHandle.selectExited.RemoveListener(OnSelectExited);
		}

		#endregion

		#region Public Methods

		public void ChangeAmmoMagazine(AmmoMagazine magazine)
		{
			_includedMagazine = magazine;
		}
		#endregion

		#region Private Methods

		private IEnumerator ShootingCoroutine()
		{

 		    if (!CanShoot || !_coolDownOver)
				yield break;
			_includedMagazine.DecreaseAmmoCount();
			var bulletObject = Instantiate(_bulletPrefab, _bulletSpawner.position, _bulletSpawner.rotation);
			
			if (bulletObject.TryGetComponent<Bullet>(out Bullet bulletComponent))
			{
				bulletComponent.SetDamage(_damage);
			}
			StartCoroutine(ShootingCoolDownCoroutine());
		}
		private IEnumerator ShootingCoolDownCoroutine()
		{
			_coolDownOver = false;
			yield return new WaitForSeconds(_shootingDelaySeconds);
			_coolDownOver = true;

		}

		private void OnActivateActionPerformed(InputAction.CallbackContext callbackContext)
		{
			_workingShootingCoroutine = StartCoroutine(ShootingCoroutine());
		}

		private void OnActivateActionCanceled(InputAction.CallbackContext callbackContext)
		{
			if(_workingShootingCoroutine != null)
				StopCoroutine(_workingShootingCoroutine);
		}

		private void OnSelectEntered(SelectEnterEventArgs arg0)
		{
			_isSelected = true;
		}
		private void OnSelectExited(SelectExitEventArgs arg0)
		{
			_isSelected = false;
		}

		#endregion


	}
}
