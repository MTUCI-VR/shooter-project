using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShooterProject.Scripts.General;
using ShooterProject.Scripts.Actors.Health;
using UnityEngine.XR.Interaction.Toolkit;
namespace ShooterProject.Scripts.Weapons
{
	[RequireComponent(typeof(Collider), typeof(XRBaseInteractable))]
	public class KnifeHitsProvider : MonoBehaviour
	{

		[SerializeField]
		private LayerMask interactionLayer;

		[SerializeField]
		[Min(0)]
		private float damage;

		[SerializeField]
		private float hitCoolDownSeconds;

		[SerializeField]
		private float minHitVelocity;

		[SerializeField]
		private float minHitAngularVelocity;

		private XRBaseInteractable _xrInteractable;
		private Health _target;
		private bool _coolDownOver;
		private Vector3 _previousPosition;
		private Quaternion _previousRotation;

		public bool CanHit => _xrInteractable.isSelected && _coolDownOver && _target != null;

		private void Awake()
		{
			_xrInteractable = GetComponent<XRBaseInteractable>();
		}
		private void Start()
		{
			_previousPosition = transform.position;
			_previousRotation = transform.rotation;
		}

		private void OnEnable()
		{
			_coolDownOver = true;
		}


		private void Update()
		{
			var positionDelta = transform.position - _previousPosition;
			var velocity = (positionDelta / Time.deltaTime).magnitude;

			var rotationDelta = transform.rotation * Quaternion.Inverse(_previousRotation);
			var angularVelocity = (rotationDelta.eulerAngles / Time.deltaTime).magnitude;

			_previousPosition = transform.position;
			_previousRotation = transform.rotation;
			TryHit(velocity,angularVelocity);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent<Health>(out var targetHealth)
				&& targetHealth.CurrentHealth > 0
				&& LayerUtils.IsLayerMatch(interactionLayer, other.gameObject.layer))
			{
				_target = targetHealth;
				_target.OnDied += ResetTarget;
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.TryGetComponent(typeof(Health), out var otherHealth)
			&& _target == otherHealth)
			{
				ResetTarget();
			}
		}

		private void ResetTarget()
        {
			_target = null;
        }
		private void TryHit(float currentVelocity, float angularVelocity)
		{
			if (!CanHit)
				return;
			if(currentVelocity >= minHitVelocity || angularVelocity >= minHitAngularVelocity)
			{
				_target.TakeHit(damage);
				StartCoroutine(HitCoolDownCoroutine());
			}
		}
		private IEnumerator HitCoolDownCoroutine()
		{
			_coolDownOver = false;
			yield return new WaitForSeconds(hitCoolDownSeconds);
			_coolDownOver = true;
		}

	}
	
}

