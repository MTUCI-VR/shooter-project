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
		private TextMeshProUGUI _gameInterfaceText;

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
			Player.Instance.PlayerHealth.OnChanged -= Print;
			WavesProvider.Instance.OnWavePreparationStarted -= Print;
			WavesProvider.Instance.OnWaveStarted -= Print;
		}

		private void Print(Health playerHealth)
		{
			_gameInterfaceText.text = $"HP: {Player.Instance.PlayerHealth.CurrentHealth}\nВолна {WavesProvider.Instance.CurrentWave + 1}";
		}
		private void Print(float wavePreparationTime)
		{
			StartCoroutine(WavePreparationTimeIndication(wavePreparationTime));
		}
		private void Print(int currentWave)
		{
			_gameInterfaceText.text = $"HP: {Player.Instance.PlayerHealth.CurrentHealth}\nВолна {currentWave}";
		}

		private IEnumerator WavePreparationTimeIndication(float wavePreparationTime)
		{
			_wavePreparationTime = wavePreparationTime;

			while (_wavePreparationTime > 0)
			{
				_gameInterfaceText.text = $"HP: {Player.Instance.PlayerHealth.CurrentHealth}\n00:{(int)_wavePreparationTime}";

				_wavePreparationTime -= Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
		}

		#endregion
	}
}
