using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShooterProject.Scripts.General;
namespace ShooterProject.Scripts.Actors.Health
{
	[RequireComponent(typeof(BoxCollider))]
	public class DamageTriggerZone : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private float damage;

		[SerializeField]
		private LayerMask interactionLayer;

		[SerializeField]
		private float hitDelaySeconds;

		private Dictionary<Collider, Coroutine> _activeHitCoroutinesDict = new Dictionary<Collider, Coroutine>();

		#endregion

		#region LifeCycle Methods

		private void OnDisable()
		{
			StopAllCoroutines();
			_activeHitCoroutinesDict.Clear();
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent<Health>(out var targetHealth)
				&& targetHealth.CurrentHealth > 0
				&& LayerUtils.IsLayerMatch(interactionLayer, other.gameObject.layer))
			{
				_activeHitCoroutinesDict.Add(other, StartCoroutine(HitCoroutine(targetHealth)));
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (!_activeHitCoroutinesDict.ContainsKey(other))
				return;

			var thisCoroutine = _activeHitCoroutinesDict[other];

			if (thisCoroutine != null)
				StopCoroutine(thisCoroutine);

			_activeHitCoroutinesDict.Remove(other);
		}

		#endregion

		#region Private Methods
		
		private IEnumerator HitCoroutine(Health health)
		{
			while (health.CurrentHealth > 0)
			{
				health.TakeHit(damage);
				yield return new WaitForSeconds(hitDelaySeconds);
			}

		}

		#endregion
	}
}
namespace ShooterProject.Scripts.General
{
	public static class LayerUtils
	{
		public static bool IsLayerMatch(LayerMask sourceLayer, int testingLayer)
		{
			return (sourceLayer & (1 << testingLayer)) != 0;
		}
	}
}
