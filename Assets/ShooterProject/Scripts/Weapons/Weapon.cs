using ShooterProject.Scripts.Items.Weapons.Reloading;
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

		private AmmoMagazine _includedMagazine;
		private bool _isSelected = false;
		private bool _coolDownOver = true;
		private Coroutine _workingShootingCoroutine;

		#endregion

		#region Properties
		public bool IsSelected => _isSelected;
		private bool _canShoot => _isSelected && _includedMagazine != null;
		private bool _canPlayNoAmooSound => _weaponParts.WeaponAudioSource != null && _weaponShootingEffects.NoAmmoSound != null;
		#endregion

		#region Events
		public event System.Action<AmmoMagazine> OnMagazineChanged;
		public event System.Action OnMagazineDetached;
		#endregion

		#region LifeCycle

		private void OnEnable()
		{
			AddEventsListeners();
		}

		private void OnDisable()
		{
			RemoveEventsListeners();
		}

		#endregion

		#region Public Methods
		/// <summary>
		/// Меняет текущий магазин с патронами и вызывает события OnMagazineChanged или OnMagazineDetached
		/// </summary>
		/// <param name="magazine">
		/// Магазин с патронами
		/// </param>
		public void ChangeAmmoMagazine(AmmoMagazine magazine)
		{
			_includedMagazine = magazine;
			if (magazine == null)
				OnMagazineDetached?.Invoke();
			else
				OnMagazineChanged?.Invoke(magazine);
		}
		#endregion

		#region Private Methods

		private IEnumerator ShootingCoroutine()
		{
			while (_canShoot)
			{
				if (!_includedMagazine.HasAmmo)
				{
					if (_canPlayNoAmooSound)
						PlaySound(_weaponShootingEffects.NoAmmoSound);
					yield break;
				}

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
			_includedMagazine.DecreaseAmmoCount();
			var bulletObject = Instantiate(_weaponParams.BulletPrefab, _weaponParts.BulletSpawner.position, _weaponParts.BulletSpawner.rotation);
			if (bulletObject.TryGetComponent<Bullet>(out Bullet bulletComponent))
			{
				bulletComponent.SetDamage(_weaponParams.Damage);
			}
		}

		private void PlaySound(AudioClip clip)
		{
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
