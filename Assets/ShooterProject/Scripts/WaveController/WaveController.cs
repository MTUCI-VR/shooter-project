using System;
using System.Collections;
using UnityEngine;

namespace ShooterProject.Scripts.WaveControllers
{
	public class WaveController : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private int timeBetweenWavesInSeconds;

		#endregion

		#region Properties

		public int TimeBetweenWavesInSeconds
		{
			get
			{
				return timeBetweenWavesInSeconds;
			}
			private set
			{
				timeBetweenWavesInSeconds = value;
				OnTimeBetweenWavesChanged?.Invoke();
			}
		}

		#endregion

		#region Events

		public event Action OnTimeBetweenWavesChanged;

		#endregion

		#region Life Cycle

		private void Start()
		{
			StartCoroutine(TestWaveTimer());
		}

		#endregion

		#region Private Methods

		private IEnumerator TestWaveTimer()
		{
			while (TimeBetweenWavesInSeconds > 0)
			{
				yield return new WaitForSeconds(1);
				TimeBetweenWavesInSeconds--;
			}
		}

		#endregion
	}
}
