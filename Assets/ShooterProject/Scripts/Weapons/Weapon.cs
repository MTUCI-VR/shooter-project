using ShooterProject.Scripts.General;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using ShooterProject.Scripts.Actors;
using ShooterProject.Scripts.Weapons.Reloading;

namespace ShooterProject.Scripts.Weapons
{
	[RequireComponent(typeof(XRGrabInteractable))]
	public class Weapon : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private WeaponParts weaponParts;

		[SerializeField]
		private WeaponParams weaponParams;

		[SerializeField]
		private WeaponShootingEffects weaponShootingEffects;

		[SerializeField]
		private LayerMask layer;

		private bool _coolDownOver = true;

		private Coroutine _workingShootingCoroutine;
		private GameObjectsPool _impactsPool;
		private XRGrabInteractable _grabInteractable;

		#endregion

		#region Properties

		private WeaponMagazineController _magazineController => weaponParts.ReloadController;
		private bool _canPlayNoAmmoSound => weaponParts.WeaponAudioSource != null && weaponShootingEffects.NoAmmoSound != null;

		#endregion

		#region LifeCycle Methods

		private void Awake()
		{
			_grabInteractable = GetComponent<XRGrabInteractable>();
			_impactsPool = new GameObjectsPool(weaponShootingEffects.MaxImpacts,
				false,
				false,
				weaponShootingEffects.ImpactPrefab,
				null);
		}

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
			if (!_magazineController.HasAmmo)
			{
				if (_canPlayNoAmmoSound)
					PlaySound(weaponShootingEffects.NoAmmoSound);
				yield break;
			}

			if (!_coolDownOver)
			{
				yield break;
			}

			do
			{
				SingleShot();
				PlaySound(weaponShootingEffects.Sound);

				yield return StartCoroutine(ShootingCoolDownCoroutine());
			} while (_magazineController.HasAmmo && weaponParams.CanFireBursts);
		}

		private void SingleShot()
		{
			_magazineController.DecreaseAmmoCount();

			weaponShootingEffects.Particles?.Play();

			Vector3 weaponForward = weaponParts.BulletSpawnPoint.forward;
			if (Physics.Raycast(weaponParts.BulletSpawnPoint.position,
				weaponForward,
				out RaycastHit hitInfo,
				weaponParams.ShootingDistance,
				layer,
				QueryTriggerInteraction.Ignore
				))
			{
				ShowImpact(hitInfo);
				if (hitInfo.collider.TryGetComponent<Health>(out Health enemyHealth))
				{
					enemyHealth.TakeHit(weaponParams.Damage);
				}
			}
		}

		private void ShowImpact(RaycastHit hitInfo)
		{
			if (!weaponShootingEffects.ImpactIgnoreTags.Contains(hitInfo.collider.gameObject.tag) &&
				_impactsPool.TryGetFreeElement(out GameObject freeImpact, true))
			{
				freeImpact.transform.position = hitInfo.point;
				freeImpact.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
			}
		}

		private void PlaySound(AudioClip clip)
		{
			if (weaponParts.WeaponAudioSource == null && clip == null)
				return;
			weaponParts.WeaponAudioSource.clip = clip;
			weaponParts.WeaponAudioSource.Play();
		}

		private IEnumerator ShootingCoolDownCoroutine()
		{
			_coolDownOver = false;
			yield return new WaitForSeconds(weaponParams.ShootingDelaySeconds);
			_coolDownOver = true;
		}

		private void OnActivateActionPerformed(ActivateEventArgs arg0)
		{
			_workingShootingCoroutine = StartCoroutine(ShootingCoroutine());
		}

		private void OnActivateActionCanceled(DeactivateEventArgs arg0)
		{
			if (_workingShootingCoroutine != null)
				StopCoroutine(_workingShootingCoroutine);
		}

		private void AddEventsListeners()
		{
			_grabInteractable.activated.AddListener(OnActivateActionPerformed);
			_grabInteractable.deactivated.AddListener(OnActivateActionCanceled);
		}

		private void RemoveEventsListeners()
		{
			_grabInteractable.activated.RemoveListener(OnActivateActionPerformed);
			_grabInteractable.deactivated.RemoveListener(OnActivateActionCanceled);
		}

		#endregion
	}
}
