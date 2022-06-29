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
		private float minHitAngularSpeed;

		private XRBaseInteractable _xrInteractable;
		private Health _target;
		private bool _coolDownOver;
		private Vector3 _lastPosition;

		public bool CanHit => _xrInteractable.isSelected && _coolDownOver && _target != null;

		private void Awake()
		{
			_xrInteractable = GetComponent<XRBaseInteractable>();
		}
		private void Start()
		{
			_lastPosition = transform.position;
		}

		private void OnEnable()
		{
			_coolDownOver = true;
		}


		private void Update()
		{
			var velocity = ((transform.position - _lastPosition) / Time.deltaTime).magnitude;
			_lastPosition = transform.position;
			TryHit(velocity);
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
		private void TryHit(float currentVelocity)
		{
			if (!CanHit)
				return;
			if(currentVelocity >= minHitVelocity)
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

