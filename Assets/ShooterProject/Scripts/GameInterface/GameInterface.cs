using TMPro;
using UnityEngine;
using ShooterProject.Scripts.Actors.Health;
using ShooterProject.Scripts.PlayerScripts;
using ShooterProject.Scripts.Waves;
using System.Collections;

namespace ShooterProject.Scripts.GameInterface
{
	public class GameInterface : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private TextMeshProUGUI healthText;
		
		[SerializeField]
		private TextMeshProUGUI waveText;

		private float _wavePreparationTime;

		#endregion

		#region Life Cycle

		private void OnEnable()
		{
			AddListeners();
		}

		private void OnDisable()
		{
			RemoveListeners();
		}

		#endregion

		#region Private Methods

		private void AddListeners()
		{
			Player.Instance.PlayerHealth.OnChanged += Print;
			WavesProvider.Instance.OnWavePreparationStarted += Print;
			WavesProvider.Instance.OnWaveStarted += Print;
		}

		private void RemoveListeners()
		{
			if (Player.Instance != null)
				Player.Instance.PlayerHealth.OnChanged -= Print;
			
			if (WavesProvider.Instance != null)
			{
				WavesProvider.Instance.OnWavePreparationStarted -= Print;
				WavesProvider.Instance.OnWaveStarted -= Print;
			}
		}

		private void Print(Health playerHealth)
		{
			healthText.text = $"{Player.Instance.PlayerHealth.CurrentHealth}";
			waveText.text = $"Wave {WavesProvider.Instance.CurrentWave + 1}";
		}
		private void Print(float wavePreparationTime)
		{
			StartCoroutine(WavePreparationTimeIndication(wavePreparationTime));
		}
		private void Print(int currentWave)
		{
			healthText.text = $"{Player.Instance.PlayerHealth.CurrentHealth}";
			waveText.text = $"Wave {currentWave}";
		}
	
		private IEnumerator WavePreparationTimeIndication(float wavePreparationTime)
		{
			_wavePreparationTime = wavePreparationTime;

			while (_wavePreparationTime > 0)
			{
				healthText.text = $"{Player.Instance.PlayerHealth.CurrentHealth}";
				waveText.text = $"{(int)_wavePreparationTime}";

				_wavePreparationTime -= Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
		}

		#endregion
	}
}