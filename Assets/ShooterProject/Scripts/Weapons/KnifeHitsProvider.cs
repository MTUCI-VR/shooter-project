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

		#region Fields

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

		#endregion

		#region Properties

		public bool CanHit => _xrInteractable.isSelected && _coolDownOver && _target != null;

		#endregion

		#region LifeCycle

		private void Awake()
		{
			_xrInteractable = GetComponent<XRBaseInteractable>();
		}

		private void Start()
		{
			SetupPositionRotation();
		}

		private void OnEnable()
		{
			_coolDownOver = true;
		}
		private void OnDisable()
		{
			ResetTarget();
		}
		private void Update()
		{
			if (CanHit)
			{
				var velocity = GetVelocity();
				var angularVelocity = GetAngularVelocity();

				SetupPositionRotation();
				TryHit(velocity, angularVelocity);
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent<Health>(out var targetHealth)
				&& targetHealth.CurrentHealth > 0
				&& LayerUtils.IsLayerMatch(interactionLayer, other.gameObject.layer))
			{
				_target = targetHealth;
				_target.OnDied += OnTargetDied;
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.TryGetComponent<Health>(out var otherHealth)
			&& _target == otherHealth)
			{
				ResetTarget();
			}
		}

		#endregion

		#region Private Methods

		private float GetAngularVelocity()
		{
			var rotationDelta = transform.rotation * Quaternion.Inverse(_previousRotation);
			var deltaAngle = rotationDelta.eulerAngles;

			if (deltaAngle.x > 180) deltaAngle.x -= 360;
			if (deltaAngle.y > 180) deltaAngle.y -= 360;
			if (deltaAngle.z > 180) deltaAngle.z -= 360;

			var angularVelocity = (deltaAngle / Time.deltaTime).magnitude;
			Debug.Log($"{deltaAngle} - {angularVelocity}");
			return angularVelocity;
		}

		private float GetVelocity()
		{
			var positionDelta = transform.position - _previousPosition;
			var velocity = (positionDelta / Time.deltaTime).magnitude;
			return velocity;
		}

		private void SetupPositionRotation()
		{
			_previousPosition = transform.position;
			_previousRotation = transform.rotation;
		}

		private void ResetTarget()
		{
			if (_target == null)
				return;
			_target.OnDied -= OnTargetDied;
			_target = null;
		}

		private void OnTargetDied(Health targetHealth)
		{
			ResetTarget();
		}

		private void TryHit(float currentVelocity, float angularVelocity)
		{
			if (currentVelocity >= minHitVelocity || angularVelocity >= minHitAngularVelocity)
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

		#endregion

	}

}

