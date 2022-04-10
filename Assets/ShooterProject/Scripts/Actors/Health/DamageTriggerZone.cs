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
	private List<string> ignoreTags;

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

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<Health>(out Health enemyHealth) && !ignoreTags.Contains(other.tag))
		{
			_activeHitCoroutinesDict.Add(other, StartCoroutine(HitCoroutine(enemyHealth)));
		}
	}
	private void OnTriggerExit(Collider other)
	{
		StopCoroutine(_activeHitCoroutinesDict[other]);
		_activeHitCoroutinesDict.Remove(other);
	}
	private IEnumerator HitCoroutine(Health health)
	{
		while (true)
		{
			health.TakeHit(damage);
			yield return new WaitForSeconds(hitDelaySeconds);
		}
	}

	#endregion
}
