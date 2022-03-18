using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShooterProject.Scripts.Items
{
	public class Weapon : MonoBehaviour
	{
		#region Private Fields
		[SerializeField]
		private int _shootingDelaySeconds;

		[SerializeField]
		private Transform _bulletSpawner;

		[SerializeField]
		private GameObject _bulletPrefab;

		private AmmoMagazine _includedMagazine;
		#endregion

		

	}
}
