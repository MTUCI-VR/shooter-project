using ShooterProject.Scripts.Actors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DamageTriggerZone : MonoBehaviour
{
	#region Fields

	[SerializeField]
	private float damage;

	[SerializeField]
	private LayerMask iteractionLayer;

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

	#endregion

	#region Private Methods

	private bool IsLayerMatch(int layer)
	{
		return (iteractionLayer & (1 << layer)) != 0;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<Health>(out var enemyHealth) && enemyHealth.CurrentHealth > 0 && IsLayerMatch(other.gameObject.layer))
		{
			_activeHitCoroutinesDict.Add(other, StartCoroutine(HitCoroutine(enemyHealth)));
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (!_activeHitCoroutinesDict.ContainsKey(other))
			return;

		Coroutine thisCoroutine = _activeHitCoroutinesDict[other];

		if (thisCoroutine != null)
			StopCoroutine(thisCoroutine);

		_activeHitCoroutinesDict.Remove(other);
	}
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
