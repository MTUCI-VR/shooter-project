using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoMagazine : MonoBehaviour
{
	#region PrivateFields

	[SerializeField]
	private int _ammoSize;

	[SerializeField]
	private int _ammoCount;

	[SerializeField]
	private int _secondsToDestroyAfterUse;
	#endregion

	#region Properties
	public bool InWeapon { get; set; }

	#endregion

	#region LifeCycle

	private void Awake()
	{
		if (_ammoCount > _ammoSize)
			_ammoCount = _ammoSize;
	}

	private void Update()
	{
		if (!InWeapon && _ammoCount <= 0)
			StartCoroutine(DestoyAmmoMagazine());
	}

	#endregion

	#region Private methods

	private IEnumerator DestoyAmmoMagazine()
	{
		yield return new WaitForSeconds(_secondsToDestroyAfterUse);
		Destroy(gameObject);
	}

	#endregion

	#region Public methods

	/// <summary> 
	/// Уменьшает кол-во патронов в текущем магазине.
	/// </summary>
	public void DecreaseAmmoCount()
	{
		_ammoCount--;
	}

	/// <summary> 
	/// Возвращает кол-во патронов в текущем магазине.
	/// </summary>
	public int GetAmmoCount()
	{
		return _ammoCount;
	}

	#endregion

}
