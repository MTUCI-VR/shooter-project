using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
namespace ShooterProject.Scripts.Items.Weapons
{

	public class Weapon : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private WeaponParts _weaponParts;

		[SerializeField]
		private WeaponParams _weaponParams;

		[SerializeField]
		private WeaponShootingEffects _weaponShootingEffects;

		private bool _isSelected = false;
		private bool _coolDownOver = true;
		private Coroutine _workingShootingCoroutine;

		#endregion

		#region Properties
		public bool IsSelected => _isSelected;
		private bool _canShoot => _isSelected;
		#endregion

		#region LifeCycle Methods

		private void OnEnable()
		{
			AddEventsListeners();
		}

		private void OnDisable()
		{
			RemoveEventsListeners();
		}

		#endregion

		#region Private Methods

		private IEnumerator ShootingCoroutine()
		{
			while (_canShoot)
			{

				if (!_coolDownOver)
				{
					yield return new WaitForEndOfFrame();
					continue;
				}

				SingleShot();

				PlaySound(_weaponShootingEffects.ShootignSound);

				StartCoroutine(ShootingCoolDownCoroutine());

				if (!_weaponParams.CanFireBursts)
					yield break;
			}
		}

		private void SingleShot()
		{
			var bulletObject = Instantiate(_weaponParams.BulletPrefab, _weaponParts.BulletSpawner.position, _weaponParts.BulletSpawner.rotation);
            _weaponShootingEffects.Particles?.Play();
		}

		private void PlaySound(AudioClip clip)
		{
			if (_weaponParts.WeaponAudioSource == null && clip == null)
				return;
			_weaponParts.WeaponAudioSource.clip = clip;
			_weaponParts.WeaponAudioSource.Play();
		}

		private IEnumerator ShootingCoolDownCoroutine()
		{
			_coolDownOver = false;
			yield return new WaitForSeconds(_weaponParams.ShootingDelaySeconds);
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

		private void AddEventsListeners()
		{
			_weaponParams.ActivateAction.action.performed += OnActivateActionPerformed;
			_weaponParams.ActivateAction.action.canceled += OnActivateActionCanceled;

			_weaponParts.InteractableHandle.selectEntered.AddListener(OnSelectEntered);
			_weaponParts.InteractableHandle.selectExited.AddListener(OnSelectExited);
		}

		private void RemoveEventsListeners()
		{
			_weaponParams.ActivateAction.action.performed -= OnActivateActionPerformed;
			_weaponParams.ActivateAction.action.canceled -= OnActivateActionCanceled;

			_weaponParts.InteractableHandle.selectEntered.RemoveListener(OnSelectEntered);
			_weaponParts.InteractableHandle.selectExited.RemoveListener(OnSelectExited);
		}

		#endregion
	}
}
