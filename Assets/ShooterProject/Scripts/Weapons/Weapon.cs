using ShooterProject.Scripts.General;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace ShooterProject.Scripts.Weapons
{
	[RequireComponent(typeof(XRGrabInteractable))]
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
		private GameObjectsPool _impactsPool;
		private XRGrabInteractable _grabInteractable;

		#endregion

		#region Properties

		#endregion

		#region LifeCycle Methods

		private void Awake()
		{
			_grabInteractable = GetComponent<XRGrabInteractable>();
			_impactsPool = new GameObjectsPool(_weaponShootingEffects.MaxImpacts,
				false,
				false,
				_weaponShootingEffects.ImpactPrefab,
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
			while (_isSelected)
			{
				if (!_coolDownOver)
				{
					yield return new WaitForEndOfFrame();
					continue;
				}

				SingleShot();

				PlaySound(_weaponShootingEffects.Sound);

				StartCoroutine(ShootingCoolDownCoroutine());

				if (!_weaponParams.CanFireBursts)
					yield break;
			}
		}

		private void SingleShot()
		{
			_weaponShootingEffects.Particles?.Play();

			Vector3 weaponForward = _weaponParts.BulletSpawnPoint.forward;
			if (Physics.Raycast(_weaponParts.BulletSpawnPoint.position, weaponForward, out RaycastHit hitInfo, _weaponParams.ShootingDistance))
			{
				ShowImpact(hitInfo);
			}
		}

		private void ShowImpact(RaycastHit hitInfo)
		{
			if (!_weaponShootingEffects.ImpactIgnoreTags.Contains(hitInfo.collider.gameObject.tag) &&
				_impactsPool.TryGetFreeElement(out GameObject freeImpact, true))
			{
				freeImpact.transform.position = hitInfo.point;
				freeImpact.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
			}
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
		#region EventsListeners

		private void OnActivateActionPerformed(ActivateEventArgs arg0)
		{
			_workingShootingCoroutine = StartCoroutine(ShootingCoroutine());
		}

		private void OnActivateActionCanceled(DeactivateEventArgs arg0)
		{
			if (_workingShootingCoroutine != null)
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
			
			_grabInteractable.activated.AddListener(OnActivateActionPerformed);
			_grabInteractable.deactivated.AddListener(OnActivateActionCanceled);

			_grabInteractable.selectEntered.AddListener(OnSelectEntered);
			_grabInteractable.selectExited.AddListener(OnSelectExited);
		}

		private void RemoveEventsListeners()
		{
			_grabInteractable.activated.RemoveListener(OnActivateActionPerformed);
			_grabInteractable.deactivated.RemoveListener(OnActivateActionCanceled);

			_grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
			_grabInteractable.selectExited.RemoveListener(OnSelectExited);
		}

		#endregion

		#endregion
	}
}
