using UnityEngine;
using System.Collections;
using ShooterProject.Scripts.Items;

namespace ShooterProject.Scripts.Spawner
{
	public class ItemSpawner : GeneralSpawner
	{
		#region Fields

		[SerializeField]
		private int spawnDelayInSeconds;

		private int _objectsInColliderCount;

		private bool _canSpawn;

		#endregion

		#region Properties

		private int ObjectsInColliderCount
		{
			get => _objectsInColliderCount;

			set
			{
				_objectsInColliderCount = value;
				
				if (_objectsInColliderCount == 0)
					onColliderEmpty?.Invoke();
			}
		}

		#endregion

		#region Events

		private event System.Action onColliderEmpty;

		#endregion

		#region  LifeCycle

		private void OnEnable()
		{
			onColliderEmpty += OnColliderEmpty;
		}
		private void OnDisable()
		{
			onColliderEmpty -= OnColliderEmpty;
		}

		private void Start()
		{
			StartCoroutine(ItemSpawn());
		}

		#endregion

		#region Private Methods

		private IEnumerator ItemSpawn()
		{
			yield return new WaitForSeconds(spawnDelayInSeconds);

			Spawn();

			_canSpawn = true;
		}

		private void OnColliderEmpty()
		{
			if (_canSpawn)
				StartCoroutine(ItemSpawn());
			_canSpawn = false;
		}

		private void OnTriggerEnter(Collider collider)
		{
			if (collider.TryGetComponent<Item>(out Item item))
			{
				ObjectsInColliderCount++;
			}
		}
		private void OnTriggerExit(Collider collider)
		{
			if (collider.TryGetComponent<Item>(out Item item))
			{
				ObjectsInColliderCount--;
			}
		}

		#endregion
	}
}